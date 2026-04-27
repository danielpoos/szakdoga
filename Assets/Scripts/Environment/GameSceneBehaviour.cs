using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas roundCanvas;
    [SerializeField] private BuyMenu buyMenuCanvas;
    [SerializeField] private GameObject playgroundObject;
    [SerializeField] private CanvasGroup uiCanvasGroup;

    [SerializeField] private Player playerObject;
    [SerializeField] private MonsterObject monsterGameObject;
    [SerializeField] private ItemOnGround droppedItemGameObject;
    [SerializeField] private ProjectileObject projectileGameObject;
    [SerializeField] private Material backgroundMat;

    [SerializeField] private InventoryExtended inventoryLine;
    [SerializeField] private InventoryExtended inventoryExtended;
    [SerializeField] private GameObject xpBar;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject statObject;

    private float maxXPValue;
    private RectTransform gainedXP;
    private float maxHealthValue;
    private RectTransform remainingHealth;
    private TMP_Text remainingHealthText;

    private float spawnTime;
    private float elapsedMonsterSpawnTime;
    private float elapsedAdvanceToNextRoundTime;

    private Vector2 absolutePlayerPosition;
    private GameObject heldItemGameObject;
    private SpriteRenderer heldItemSpriteRenderer;

    private List<MonsterObject> enemyObjects = new();
    private List<ItemOnGround> droppedItemObjects = new();
    private List<ProjectileObject> projectiles = new();
    private UnityAction<MonsterObject> MonsterDeadAction;
    private UnityAction PlayerDeadAction;
    private UnityAction<int> ButtonClickAction;

    private bool isStatShown = false;
    private bool isRoundShown = false;
    private bool isBuyMenuShown = false;
    private bool isInventoryShown = false;
    private int[] coordinates = { 300, -300, 150, -150 };
    #endregion
    #region Init
    private void Awake()
    {
        Player p = Instantiate(playerObject, playgroundObject.transform);
        p.Hunter = gameSetting.Hunter;
        gameSetting.Player = p;
        p.transform.position = gameSetting.PlayerPosition;
        p.gameObject.SetActive(true);
        playerObject.gameObject.SetActive(false);
        p.ChangeSprite(p.Hunter.Sprite);
        heldItemGameObject = p.transform.Find("Item").gameObject;
        absolutePlayerPosition = new(Screen.width - Screen.width / 2+gameSetting.PlayerPosition.x, Screen.height - Screen.height / 2 + 60+gameSetting.PlayerPosition.y);
        PlayerDeadAction += EndGame;
        Player.PlayerDead.AddListener(PlayerDeadAction);
        InitScene();

        if (gameSetting.IsNewGame)
        {
            gameSetting.Timer = 0;
            gameSetting.RoundNum = 0;
            gameSetting.PlayerPosition = new(0, 0);
            gameSetting.Spawner.ClearMonsters();
            gameSetting.ItemsOnGround.Clear();
            gameSetting.SaveFileName = "";
        }
        else if (gameSetting.IsLoadedGame)
        {
            gameSetting.Spawner.ClearMonsters();
            gameSetting.ItemsOnGround.Clear();
            UpdateTexts();
        }
        else
        {
            LoadScene();
        }
        if (gameSetting.Player.Hunter.Score == 0) scoreText.enabled = false;
        gameSetting.Player.transform.position = gameSetting.PlayerPosition;
    }
    void Start()
    {
        HandleSceneChange();
        gameSetting.Player.Hunter.SelectCurrentItem();
        heldItemSpriteRenderer.sprite = gameSetting.Player.Hunter.CurrentItem.Sprite;
        backgroundMat.mainTextureOffset = gameSetting.Player.Hunter.position;
    }
    void Update()
    {
        UpdateTexts();
        HandleSceneChange();
        if (!isBuyMenuShown && !isRoundShown)
        {
            gameSetting.Timer += Time.deltaTime;
            elapsedMonsterSpawnTime += Time.deltaTime;
            elapsedAdvanceToNextRoundTime += Time.deltaTime;
            MovePlayer();
            MoveMonsters();
            MoveProjectiles();
            if (elapsedMonsterSpawnTime > spawnTime)
            {
                elapsedMonsterSpawnTime -= spawnTime;
                SpawnMonster();
            }
        }
        if (Input.anyKeyDown) OnKeyDown();
        UpdateObjects();
    }
    private void InitScene()
    {
        heldItemSpriteRenderer = heldItemGameObject.GetComponent<SpriteRenderer>();
        maxXPValue = xpBar.transform.GetComponent<RectTransform>().sizeDelta.x;
        gainedXP = xpBar.transform.Find("Gained").GetComponent<RectTransform>();
        maxHealthValue = healthBar.transform.GetComponent<RectTransform>().sizeDelta.x;
        remainingHealth = healthBar.transform.Find("Remaining").GetComponent<RectTransform>();
        remainingHealthText = healthBar.transform.Find("RemainingText").GetComponent<TMP_Text>();
        
        MonsterDeadAction += Collision;
        MonsterObject.MonsterDead.AddListener(MonsterDeadAction);

        enemyObjects = new();
        droppedItemObjects = new();
        projectiles = new();
        elapsedMonsterSpawnTime = 0;
        elapsedAdvanceToNextRoundTime = 0;
        heldItemSpriteRenderer.sprite = gameSetting.Player.Hunter.Weapon.GetSprite();
        spawnTime = gameSetting.DiffInt switch
        {
            1=> 4,
            2=> 3,
            _=> 5,
        };
        inventoryLine.SetInventory(gameSetting.Player.Hunter.Inventory);
        inventoryExtended.SetInventory(gameSetting.Player.Hunter.Inventory);
    }
    #endregion
    #region Functions
    private void Pause()
    {
        SaveScene();
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Single);
    }
    private void UpdateTexts()
    {
        moneyText.text = "$ " + gameSetting.Player.Hunter.Money;
        if (gameSetting.Player.Hunter.Score != 0)
        {
            scoreText.text = "" + gameSetting.Player.Hunter.Score;
            scoreText.enabled = true;
        }
        int hour = (int)(gameSetting.Timer / 3600);
        int min = (int)(gameSetting.Timer / 60);
        int sec = (int)(gameSetting.Timer % 60);
        TimeSpan ts = new(hour, min, sec);
        timeText.text = $"{ts:c}";
        float xpWidth = maxXPValue * gameSetting.Player.Hunter.XP / gameSetting.Player.Hunter.ExperienceForNextLevel();
        gainedXP.sizeDelta = new Vector2(maxXPValue - xpWidth, gainedXP.sizeDelta.y);
        gainedXP.position = new Vector3(xpWidth / 2.0f, gainedXP.position.y, 0);
        remainingHealthText.text = $"{gameSetting.Player.Hunter.HP}/{gameSetting.Player.Hunter.MaxHP}";
        remainingHealth.sizeDelta = new Vector2(maxHealthValue * gameSetting.Player.Hunter.HP / gameSetting.Player.Hunter.MaxHP, remainingHealth.sizeDelta.y);
        statObject.GetComponentsInChildren<TMP_Text>()[0].text = $"LVL: {gameSetting.Player.Hunter.Level}";
        statObject.GetComponentsInChildren<TMP_Text>()[1].text = $"ATK: {gameSetting.Player.Hunter.Attack}";
        statObject.GetComponentsInChildren<TMP_Text>()[2].text = $"HP : {gameSetting.Player.Hunter.HP}";
        statObject.GetComponentsInChildren<TMP_Text>()[3].text = $"MAX: {gameSetting.Player.Hunter.MaxHP}";
        statObject.GetComponentsInChildren<TMP_Text>()[4].text = $"XP : {gameSetting.Player.Hunter.XP}";
        statObject.GetComponentsInChildren<TMP_Text>()[5].text = $"MAX: {gameSetting.Player.Hunter.ExperienceForNextLevel()}";
    }
    private void LoadScene()
    {
        gameSetting.Player.Hunter.SetWeapon();
        for (int m = 0; m < gameSetting.Spawner.Monsters.Count;m++)
        {
            MonsterObject mo = MonsterObject.Instantiate(monsterGameObject, playgroundObject.transform);
            mo.gameObject.SetActive(true);
            mo.transform.position = gameSetting.monsterPlace[m];
            mo.SetMonster(gameSetting.Spawner.Monsters[m]);
            mo.ChangeSprite(gameSetting.Spawner.Monsters[m].Sprite);
            enemyObjects.Add(mo);
        }
        for (int i= 0; i< gameSetting.ItemsOnGround.Count;i++)
        {
            ItemOnGround it = ItemOnGround.Instantiate(droppedItemGameObject, playgroundObject.transform);
            it.gameObject.SetActive(true);
            it.transform.position = gameSetting.itemPlace[i];
            it.SetItem(gameSetting.ItemsOnGround[i]);
            it.ChangeSprite(gameSetting.ItemsOnGround[i].Sprite);
            droppedItemObjects.Add(it);
        }
    }
    private void SaveScene()
    {
        gameSetting.Hunter = gameSetting.Player.Hunter;
        gameSetting.monsterPlace.Clear();
        gameSetting.itemPlace.Clear();
        foreach (MonsterObject mon in enemyObjects)
        {
            gameSetting.monsterPlace.Add(mon.transform.position);
        }
        foreach (ItemOnGround item in droppedItemObjects)
        {
            gameSetting.itemPlace.Add(item.transform.position);
        }
    }
    private void UpdateObjects()
    {
        absolutePlayerPosition = new(Screen.width - Screen.width / 2 + gameSetting.Player.transform.position.x, Screen.height - Screen.height / 2 + 60 + gameSetting.Player.transform.position.y);
        try
        {
            foreach (MonsterObject m in enemyObjects)
            {
                if (m.gameObject.activeSelf && m.Monster.IsDead)
                {
                    m.gameObject.SetActive(false);
                    gameSetting.Spawner.Monsters.Remove(m.Monster);
                    enemyObjects.Remove(m);
                }
            }
            foreach (ItemOnGround i in droppedItemObjects)
            {
                if (i.gameObject.activeSelf && i.IsPickedUp)
                {
                    i.gameObject.SetActive(false);
                    gameSetting.ItemsOnGround.Remove(i.Item);
                    droppedItemObjects.Remove(i);
                }
            }
            foreach (ProjectileObject p in projectiles)
            {
                if (p.gameObject.activeSelf && p.IsHit)
                {
                    p.gameObject.SetActive(false);
                    projectiles.Remove(p);
                }
            }
        }
        catch (Exception) { }
        inventoryLine.SetInventory(gameSetting.Player.Hunter.Inventory);
        inventoryExtended.SetInventory(gameSetting.Player.Hunter.Inventory);
        foreach (Transform child in playgroundObject.transform)
        {
            if (child.gameObject.activeSelf == false)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    private void OnKeyDown()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isBuyMenuShown)
        {
            ShootProjectile(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if (isBuyMenuShown)
            {
                isBuyMenuShown = false;
                HideBuyMenu();
            }
            else {
                SaveScene();
                Pause();
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            SwitchItem(true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            SwitchItem(false);
        }
        if (Input.GetKey(KeyCode.T))
        {
            isInventoryShown = !isInventoryShown;
            inventoryExtended.enabled = isInventoryShown;
            inventoryExtended.gameObject.SetActive(isInventoryShown);
            inventoryLine.enabled = !isInventoryShown;
            inventoryLine.gameObject.SetActive(!isInventoryShown);
            inventoryLine.SetInventory(gameSetting.Player.Hunter.Inventory);
            inventoryExtended.SetInventory(gameSetting.Player.Hunter.Inventory);
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (isStatShown) statObject.SetActive(false);
            else statObject.SetActive(true);
            isStatShown = !isStatShown;
        }
    }
    private void SwitchItem(bool backward)
    {
        if (backward) gameSetting.Player.Hunter.SwitchBackward();
        else gameSetting.Player.Hunter.SwitchForward();
        heldItemSpriteRenderer.sprite = gameSetting.Player.Hunter.CurrentItem.Sprite;
    }
    #endregion
    #region Movement
    private void MoveScene()
    {
        gameSetting.BackgroundPosition = backgroundMat.mainTextureOffset;
        backgroundMat.mainTextureOffset += (gameSetting.Player.Hunter.MovementSpeed/20) * Time.deltaTime * gameSetting.Player.Hunter.position.normalized;
        Vector2 movebg = -gameSetting.Player.Hunter.MovementSpeed * (3 / 4.0f) * Time.deltaTime * gameSetting.Player.Hunter.position.normalized;
        foreach (ItemOnGround item in droppedItemObjects)
        {
            item.transform.Translate(movebg);
        }
        foreach (MonsterObject m in enemyObjects)
        {
            m.transform.Translate(movebg);
        }
        // future update: use sprite animations
    }
    private void MovePlayer()
    {
        int maxX = coordinates[0];
        int minX = coordinates[1];
        int maxY = coordinates[2];
        int minY = coordinates[3];
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 playerPos = gameSetting.Player.Hunter.MovementSpeed * Time.deltaTime * new Vector2(horizontal, vertical);
        gameSetting.Player.Hunter.position = playerPos;
        gameSetting.Player.transform.Translate(playerPos);
        // rotate player
        FlipPlayer(horizontal);
        // move player inside an invisible rectangle
        if (Math.Abs(gameSetting.Player.transform.position.x) < maxX && Math.Abs(gameSetting.Player.transform.position.y) < maxY)
        {
            gameSetting.PlayerPosition = gameSetting.Player.transform.position;
        }
        else
        {
            if (Math.Abs(gameSetting.Player.transform.position.x) >= maxX || Math.Abs(gameSetting.Player.transform.position.y) >= maxY)
            {
                if (Math.Abs(gameSetting.Player.transform.position.x) >= maxX && Math.Abs(gameSetting.Player.transform.position.y) >= maxY)
                {
                    MoveScene();
                    if (gameSetting.Player.transform.position.x >= maxX)
                    {
                        if (gameSetting.Player.transform.position.y <= minY) { gameSetting.PlayerPosition = new Vector2(maxX, minY); }
                        else if (gameSetting.Player.transform.position.y >= maxY) { gameSetting.PlayerPosition = new Vector2(maxX, maxY); }
                    }
                    if (gameSetting.Player.transform.position.x <= minX)
                    {
                        if (gameSetting.Player.transform.position.y <= minY) { gameSetting.PlayerPosition = new Vector2(minX, minY); }
                        else if (gameSetting.Player.transform.position.y >= maxY) { gameSetting.PlayerPosition = new Vector2(minX, maxY); }
                    }
                    if (gameSetting.Player.transform.position.y <= minY)
                    {
                        if (gameSetting.Player.transform.position.x >= maxX) { gameSetting.PlayerPosition = new Vector2(maxX, minY); }
                        else if (gameSetting.Player.transform.position.x <= minX) { gameSetting.PlayerPosition = new Vector2(minX, minY); }
                    }
                    if (gameSetting.Player.transform.position.y >= maxY)
                    {
                        if (gameSetting.Player.transform.position.x >= maxX) { gameSetting.PlayerPosition = new Vector2(maxX, maxY); }
                        else if (gameSetting.Player.transform.position.x <= minX) { gameSetting.PlayerPosition = new Vector2(minX, maxY); }
                    }
                }
                else
                {
                    if (gameSetting.Player.transform.position.x >= maxX)
                    {
                    if (Math.Abs(horizontal) == 1 && Math.Abs(vertical) != 1) { MoveScene(); }
                        gameSetting.PlayerPosition = new Vector2(maxX, gameSetting.Player.transform.position.y);
                    }
                    if (gameSetting.Player.transform.position.x <= minX)
                    {
                    if (Math.Abs(horizontal) == 1 && Math.Abs(vertical) != 1) { MoveScene(); }
                        gameSetting.PlayerPosition = new Vector2(minX, gameSetting.Player.transform.position.y);
                    }
                    if (gameSetting.Player.transform.position.y <= minY)
                    {
                    if (Math.Abs(vertical) == 1 && Math.Abs(horizontal) != 1) { MoveScene(); }
                        gameSetting.PlayerPosition = new Vector2(gameSetting.Player.transform.position.x, minY);
                    }
                    if (gameSetting.Player.transform.position.y >= maxY)
                    {
                    if (Math.Abs(vertical) == 1 && Math.Abs(horizontal) != 1) { MoveScene(); }
                        gameSetting.PlayerPosition = new Vector2(gameSetting.Player.transform.position.x, maxY);
                    }
                }
            }
        }
        //pos of background
        gameSetting.Player.transform.position = gameSetting.PlayerPosition;
    }
    private void FlipPlayer(float horizontal)
    {
        bool flipPlayer;
        if (horizontal > 0)
        {
            flipPlayer = false;
            gameSetting.Player.Hunter.rotation = flipPlayer;
        }
        else
        {
            if (horizontal == 0)
            {
                flipPlayer = gameSetting.Player.Hunter.rotation;
            }
            else
            {
                flipPlayer = true;
                gameSetting.Player.Hunter.rotation = flipPlayer;
            }
        }
        gameSetting.Player.Flip(flipPlayer);
        heldItemSpriteRenderer.flipX = flipPlayer;
    }
    private void MoveMonsters()
    {
        Vector2 newPosition;
        foreach (MonsterObject m in enemyObjects)
        {
            float xPos = gameSetting.PlayerPosition.x - m.transform.position.x;
            float yPos = gameSetting.PlayerPosition.y - m.transform.position.y;
            Vector2 toPlayer = new(xPos, yPos);
            bool flipMonster;
            if (xPos > 0)
            {
                flipMonster = false;
                m.Monster.rotation = flipMonster;
            }
            else
            {
                if (xPos == 0) flipMonster = m.Monster.rotation;
                else
                {
                    flipMonster = true;
                    m.Monster.rotation = flipMonster;
                }
            }
            m.Flip(flipMonster);
            newPosition = gameCanvas.scaleFactor * m.Monster.MovementSpeed/2 * Time.deltaTime * toPlayer.normalized;
            m.Monster.position = newPosition;
            m.transform.Translate(newPosition);
        }
    }
    private void MoveProjectiles()
    {
        foreach (ProjectileObject proj in projectiles)
        {
            if (proj == null || proj.Projectile == null) continue;
            Vector3 nextPos = gameCanvas.scaleFactor * proj.Projectile.MovementSpeed * Time.deltaTime * proj.Projectile.Destination.normalized;
            if (Math.Sqrt(Math.Pow(proj.Projectile.Destination.x - nextPos.x, 2) + Math.Pow(proj.Projectile.Destination.y - nextPos.y, 2)) < 0)
            {
                proj.gameObject.SetActive(false);
                projectiles.Remove(proj);
            }
            else
            {
                proj.transform.Translate(nextPos);
            }
        }
    }
    #endregion
    #region MonsterSpawn
    private void SpawnMonsterOnLocation()
    {
        int maxX = 450;
        int maxY = 250;
        float startX;
        float startY;
        Vector2 startPosition;
        do
        {
            startX = UnityEngine.Random.Range(-maxX, maxX);
            startY = UnityEngine.Random.Range(-maxY, maxY);
            startPosition = new(startX, startY);
        }
        //while (Math.Pow(startPosition.x - gameSetting.PlayerPosition.x, 2) + Math.Pow(startPosition.y - gameSetting.PlayerPosition.y, 2) > Math.Pow(50, 2));
        while (Math.Abs(gameSetting.PlayerPosition.x - startPosition.x) < 100 && Math.Abs(gameSetting.PlayerPosition.y - startPosition.y) < 100);

        //set monster object
        MonsterBase newMonster = gameSetting.Spawner.SpawnMonster(startPosition);
        MonsterObject m = MonsterObject.Instantiate(monsterGameObject, playgroundObject.transform);
        m.gameObject.SetActive(true);
        m.transform.position = startPosition;
        m.SetMonster(newMonster);
        m.Monster.LevelUpMonster(gameSetting.Player.Hunter.Level, gameSetting.Difficulty);
        m.ChangeSprite(newMonster.Sprite);
        enemyObjects.Add(m);
    }
    private void SpawnMonster()
    {
        SpawnMonsterOnLocation();
        gameSetting.Spawner.UpdateTargetLocation(gameSetting.PlayerPosition);
    }
    private void DropItemOnGround(Item item, Vector2 position)
    {
        gameSetting.ItemsOnGround.Add(item);
        ItemOnGround droppedItem = Instantiate(droppedItemGameObject, playgroundObject.transform);
        droppedItem.gameObject.SetActive(true);
        droppedItem.transform.position = item.position = position;
        droppedItem.SetItem(item);
        droppedItem.ChangeSprite(item.GetSprite());
        droppedItemObjects.Add(droppedItem);
    }
    #endregion
    #region Round
    private void TriggerNextRound()
    {
        elapsedAdvanceToNextRoundTime = 0;
        gameSetting.Player.Hunter.CanGoNextRound = false;
        gameSetting.RoundNum += 1;
        if (gameSetting.RoundNum > 10) {
            SaveScene();
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
            return;
        }
        roundCanvas.gameObject.SetActive(true);
        StartCoroutine(RoundDelay());
    }
    private IEnumerator RoundDelay()
    {
        TMP_Text round = roundCanvas.transform.Find("RoundText").GetComponent<TMP_Text>();
        round.GetOrAddComponent<CanvasRenderer>();
        round.text = $"Round {gameSetting.RoundNum}";
        round.gameObject.SetActive(true);
        round.enabled = true;
        roundCanvas.enabled = true;
        this.enabled = false;
        isRoundShown = true;
        yield return new WaitForSecondsRealtime(1.5f);
        this.enabled = true;
        isRoundShown = false;
        roundCanvas.enabled = false;
        round.gameObject.SetActive(false);
        round.enabled = false;
    }
    private void HandleSceneChange()
    {
        gameSetting.Player.Hunter.CanGoNextRound = elapsedAdvanceToNextRoundTime > 30 || gameSetting.Player.Hunter.CanGoNextRound;
        if (gameSetting.IsNewGame || gameSetting.IsLoadedGame)
        {
            uiCanvasGroup.interactable = false;
            TriggerNextRound();
            gameSetting.IsNewGame = false;
            gameSetting.IsLoadedGame = false;
            uiCanvasGroup.interactable = true;
        }
        else if (gameSetting.Player.Hunter.CanGoNextRound)
        {
            uiCanvasGroup.interactable = false;
            TriggerNextRound();
            gameSetting.Player.Hunter.CanGoNextRound = false;
            elapsedAdvanceToNextRoundTime = 0;
            uiCanvasGroup.interactable = true;
            ShowBuyMenu();
        }
        else if (gameSetting.IsPaused)
        {
            gameSetting.IsPaused = false;
            elapsedAdvanceToNextRoundTime = 0;
        }
    }
    private void ShowBuyMenu()
    {
        isBuyMenuShown = true;
        buyMenuCanvas.enabled = true;
        buyMenuCanvas.gameObject.SetActive(true);
        buyMenuCanvas.GenerateItems(gameSetting.Player.Hunter.Level, gameSetting.DiffInt+1);
        ButtonClickAction += AddItemToHunter;
        BuyMenu.ButtonClick.AddListener(ButtonClickAction);
        playgroundObject.SetActive(false);
        uiCanvasGroup.interactable = false;
    }
    private void HideBuyMenu()
    {
        isBuyMenuShown = false;
        buyMenuCanvas.enabled = false;
        buyMenuCanvas.gameObject.SetActive(false);
        playgroundObject.SetActive(true);
        uiCanvasGroup.interactable = true;
        BuyMenu.ButtonClick.RemoveAllListeners();
    }
    public void AddItemToHunter(int i)
    {
        if(gameSetting.Player.Hunter.HasEnoughMoney(buyMenuCanvas.price[i])){
            gameSetting.Player.AddToInventory(buyMenuCanvas.items[i]);
            gameSetting.Player.Hunter.AddMoney(-buyMenuCanvas.price[i]);
            buyMenuCanvas.Disable();
            ButtonClickAction -= AddItemToHunter;
            try { BuyMenu.ButtonClick.RemoveListener(ButtonClickAction); }
            catch (Exception) { }
        }
    }
    #endregion
    #region Collision
    private void Collision(MonsterObject monster)
    {
        gameSetting.Spawner.Monsters.Remove(monster.Monster);
        enemyObjects.Remove(monster);
        switch (gameSetting.Difficulty ) {
            case Difficulty.Normal:
                gameSetting.Player.Hunter.IncreaseScore(100);
                gameSetting.Player.Hunter.AddMoney(75);
                break;
            case Difficulty.Hard:
                gameSetting.Player.Hunter.IncreaseScore(200);
                gameSetting.Player.Hunter.AddMoney(50);
                break;
            default:
                gameSetting.Player.Hunter.IncreaseScore(50);
                gameSetting.Player.Hunter.AddMoney(100);
                break;
        }
        gameSetting.Player.Hunter.AddXP(monster.Monster.XP);
        DropItemOnGround(monster.Monster.ItemDrop, monster.transform.position);
        Destroy(monster);
    }
    private void EndGame()
    {
        float waitASecond = 0f;
        while (waitASecond < .5f) waitASecond += Time.deltaTime;
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
    private void ShootProjectile(Vector2 clickPos)
    {
        //bool tempRotate = gameSetting.Player.Hunter.rotation;
        if (gameSetting.Player.Hunter.CurrentItem.GetType() == typeof(WeaponBase))
        {
            ProjectileBase bullet = gameSetting.Player.GetProjectile();
            Vector2 clickAbsPos = gameSetting.PlayerPosition - absolutePlayerPosition;
            bullet.Destination = new Vector2(clickPos.x + clickAbsPos.x, clickPos.y + clickAbsPos.y);
            Vector2 aha = -(gameSetting.PlayerPosition - bullet.Destination);
            bullet.Destination = aha;
            //FlipPlayer(bullet.Destination.x);
            ProjectileObject proj = Instantiate(projectileGameObject, playgroundObject.transform);
            proj.gameObject.SetActive(true);
            proj.transform.Find("GameObject").eulerAngles = new Vector3(0, 0, Mathf.Atan2(aha.y, aha.x) * Mathf.Rad2Deg);
            proj.transform.Find("GameObject").GetComponent<SpriteRenderer>().sprite = bullet.Sprite;
            proj.transform.position = new Vector2(gameSetting.PlayerPosition.x, gameSetting.PlayerPosition.y + 60);
            proj.Projectile = bullet;
            //proj.ChangeSprite(bullet.Sprite);
            proj.ChangeSprite(null);
            projectiles.Add(proj);
            //gameSetting.Player.Hunter.rotation = tempRotate;
        }
        else if (gameSetting.Player.Hunter.CurrentItem.GetType() == typeof(Item))
        {
            // use and remove from inventory
            gameSetting.Player.Hunter.UseItem();
            gameSetting.Player.Hunter.Inventory.RemoveItem(gameSetting.Player.Hunter.CurrentItem);
            SwitchItem(false);
        }
    }
    #endregion
}
