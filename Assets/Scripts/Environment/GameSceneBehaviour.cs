using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject playgroundObject;
    [SerializeField] private Player playerObject;
    [SerializeField] private ItemOnGround droppedItemGameObject;
    [SerializeField] private ProjectileObject projectileGameObject;
    [SerializeField] private MonsterObject monsterGameObject;
    [SerializeField] private Material backgroundMat;

    [SerializeField] private InventoryExtended inventoryLine;
    [SerializeField] private InventoryExtended inventoryExtended;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    [SerializeField] private Canvas roundCanvas;
    [SerializeField] private Canvas buyMenuCanvas;

    private float spawnTime;
    private float elapsedMonsterSpawnTime;
    private float elapsedAdvanceToNextRoundTime;

    private Vector2 backgroundOffset;
    private GameObject heldItemGameObject;
    private SpriteRenderer heldItemSpriteRenderer;
    private BoxCollider2D droppedItemBoxCollider;
    private SpriteRenderer droppedItemSpriteRenderer;
    //private BoxCollider2D monsterBoxCollider;
    //private SpriteRenderer monsterSpriteRenderer;

    private List<MonsterObject> enemyObjects;
    private List<ItemOnGround> droppedItemObjects;
    private List<ProjectileObject> projectiles;

    private float maxHealthValue;
    private RectTransform remainingHealth;
    private TMP_Text remainingHealthText;

    private bool isRoundShown = false;
    private bool isBuyMenuShown = false;
    private bool isInventoryShown = false;
    private bool isSceneJustLoaded;
    private int[] coordinates = { 300, -300, 150, -150 };
    #endregion
    #region Init
    private void Awake()
    {
        //player
        Player p = Instantiate(playerObject, playgroundObject.transform);
        p.SetPlayer(gameSetting.Difficulty);
        p.transform.position = gameSetting.PlayerPosition;
        p.Hunter = gameSetting.Hunter;
        gameSetting.Player = p;
        p.gameObject.SetActive(true);
        playerObject.gameObject.SetActive(false);
        if (gameSetting.Player.Score == 0) scoreText.enabled = false;
        p.ChangeSprite(p.Hunter.Sprite);
        gameSetting.Player.SetWeapon();
        heldItemGameObject = p.transform.Find("Item").gameObject;
        //scene
        InitScene();

        if (gameSetting.IsNewGame)
        {
            gameSetting.IsNewGame = false;
            gameSetting.Timer = 0;
            gameSetting.RoundNum = 0;
            droppedItemObjects.Clear();
            enemyObjects.Clear();
            gameSetting.PlayerPosition = new(0, 0);
            gameSetting.Spawner.ClearMonsters();
            gameSetting.ItemsOnGround.Clear();
        }
        else if (gameSetting.IsLoadedGame)
        {
            gameSetting.IsLoadedGame = false;
            droppedItemObjects.Clear();
            enemyObjects.Clear();
            gameSetting.Spawner.ClearMonsters();
            gameSetting.ItemsOnGround.Clear();
        }
        else
        {
            foreach (MonsterBase nm in gameSetting.Spawner.Monsters)
            {
                MonsterObject m = MonsterObject.Instantiate(monsterGameObject, playgroundObject.transform);
                m.gameObject.SetActive(true);
                m.transform.position = nm.position;
                m.SetMonster(nm);
                m.ChangeSprite(nm.Sprite);
                //monsterGameObject.spriteRenderer.sprite = nm.Sprite;
                enemyObjects.Add(m);
            }
        }
        //playerObject.transform.position = gameSetting.PlayerPosition;
        gameSetting.Player.transform.position = gameSetting.PlayerPosition;
    }
    void Start()
    {
        if (gameSetting.IsNewGame || gameSetting.IsLoadedGame)
        {
            TriggerNextRound();
        }
        heldItemSpriteRenderer.sprite = gameSetting.Player.CurrentItem.Sprite;
        backgroundOffset = gameSetting.Player.Hunter.position;
        backgroundMat.mainTextureOffset = backgroundOffset;
    }
    void Update()
    {
        UpdateTexts();
        //if (elapsedAdvanceToNextRoundTime > 60 && gameSetting.Player.Hunter.Level)
        TriggerNextRound();
        if (!isBuyMenuShown || !isRoundShown)
        {
            gameSetting.Timer += Time.deltaTime;
            elapsedMonsterSpawnTime += Time.deltaTime;
            elapsedAdvanceToNextRoundTime += Time.deltaTime;
        }
        MovePlayer();
        MoveMonsters();
        if (Input.anyKeyDown) OnKeyDown();
        if (elapsedMonsterSpawnTime > spawnTime)
        {
            elapsedMonsterSpawnTime -= spawnTime;
            SpawnMonster();
        }
    }
    private void InitScene()
    {
        heldItemSpriteRenderer = heldItemGameObject.GetComponent<SpriteRenderer>();
        droppedItemBoxCollider = droppedItemGameObject.GetComponent<BoxCollider2D>();
        droppedItemSpriteRenderer = droppedItemGameObject.GetComponent<SpriteRenderer>();
        maxHealthValue = healthBar.transform.GetComponent<RectTransform>().sizeDelta.x;
        remainingHealth = healthBar.transform.Find("Remaining").GetComponent<RectTransform>();
        remainingHealthText = healthBar.transform.Find("RemainingText").GetComponent<TMP_Text>();

        isSceneJustLoaded = true;
        enemyObjects = new();
        droppedItemObjects = new();
        projectiles = new();
        elapsedMonsterSpawnTime = 0;
        elapsedAdvanceToNextRoundTime = 0;
        spawnTime = 1 / gameSetting.Player.Hunter.Level;
        heldItemSpriteRenderer.sprite = gameSetting.Player.Hunter.Weapon.GetSprite();
        inventoryLine.SetInventory(gameSetting.Player.Inventory);
        inventoryExtended.SetInventory(gameSetting.Player.Inventory);
    }
    public void Pause()
    {
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Single);
    }
    private void UpdateTexts()
    {
        moneyText.text = "$ " + gameSetting.Player.Money;
        if (gameSetting.Player.Score != 0)
        {
            scoreText.text = "" + gameSetting.Player.Score;
            scoreText.enabled = true;
        }
        int hour = (int)(gameSetting.Timer / 3600);
        int min = (int)(gameSetting.Timer / 60);
        int sec = (int)(gameSetting.Timer % 60);
        TimeSpan ts = new(hour, min, sec);
        timeText.text = $"{ts:c}";
        remainingHealthText.text = $"{gameSetting.Player.Hunter.HP}/{gameSetting.Player.Hunter.MaxHP}";
        remainingHealth.sizeDelta = new Vector2(gameSetting.Player.Hunter.MaxHP / gameSetting.Player.Hunter.HP * maxHealthValue, remainingHealth.sizeDelta.y);
    }
    private void OnKeyDown()
    {
        if (Input.GetKey(KeyCode.Mouse0))
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
            else Pause();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            //weapon -> item
            //item -> weapon
            //prev item
            // cannot cycle
        }
        if (Input.GetKey(KeyCode.E))
        {
            //weapon -> item
            //item -> weapon
            //next item
            // cannot cycle
        }
        if (Input.GetKey(KeyCode.T))
        {
            //inventory
            isInventoryShown = !isInventoryShown;
            inventoryExtended.enabled = isInventoryShown;
            inventoryExtended.gameObject.SetActive(isInventoryShown);
            inventoryLine.enabled = !isInventoryShown;
            inventoryLine.gameObject.SetActive(!isInventoryShown);
            inventoryLine.SetInventory(gameSetting.Player.Inventory);
            inventoryExtended.SetInventory(gameSetting.Player.Inventory);
        }
        if (Input.GetKey(KeyCode.F))
        {
            //interact
            //throw item
        }
    }
    #endregion
    #region Movement
    private void MoveScene()
    {
        //rigidBody.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //transform.position = backgroundOffset + movement * parallax;
        // use this as sample for item and enemy movement on new instance or transform
        backgroundMat.mainTextureOffset += (gameSetting.Player.Hunter.MovementSpeed/20) * Time.deltaTime * gameSetting.Player.Hunter.position.normalized;
        gameSetting.BackgroundPosition = backgroundMat.mainTextureOffset;
        //Debug.Log(gameSetting.BackgroundPosition+ " "+ playerObject.transform.position + " " + backgroundOffset + " "+ playerObject.transform.position.x/gameSetting.BackgroundPosition.x+" "+ playerObject.transform.position.y/gameSetting.BackgroundPosition.y);
        // select sprite
        // use sprite animations
        // rigidBody.MovePosition(rigidBody.position + hunter.MovementSpeed * Time.unscaledDeltaTime * movementDirection);
    }
    private void MovePlayer()
    {
        // an invisible rectangle
        int maxX = coordinates[0];
        int minX = coordinates[1];
        int maxY = coordinates[2];
        int minY = coordinates[3];
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 playerPos = gameSetting.Player.Hunter.MovementSpeed * Time.deltaTime * new Vector2(horizontal, vertical);
        gameSetting.Player.Hunter.position = playerPos;
        Vector2 negativPlayerPos = playerPos * new Vector2(-1, 1);
        // rotate player
        bool rotatePlayer;
        if (horizontal > 0)
        {
            rotatePlayer = false;
            gameSetting.Player.Hunter.rotation = rotatePlayer;
            //playerObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            //playerObject.transform.Translate(playerPos);
            gameSetting.Player.transform.rotation = new Quaternion(0, 0, 0, 0);
            gameSetting.Player.transform.Translate(playerPos);
            heldItemGameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            rotatePlayer = gameSetting.Player.Hunter.rotation;
            if (horizontal == 0 && rotatePlayer)
            {
                gameSetting.Player.transform.rotation = new Quaternion(0, 180, 0, 0);
                gameSetting.Player.transform.Translate(negativPlayerPos);
                heldItemGameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else if (horizontal == 0 && !rotatePlayer){
                gameSetting.Player.transform.rotation = new Quaternion(0, 0, 0, 0);
                gameSetting.Player.transform.Translate(playerPos);
                heldItemGameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                rotatePlayer = true;
                gameSetting.Player.Hunter.rotation = rotatePlayer;
                gameSetting.Player.transform.rotation = new Quaternion(0, 180, 0, 0);
                gameSetting.Player.transform.Translate(negativPlayerPos);
                heldItemGameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
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
            //refactor
            //if (gameSetting.Player.transform.position.x >= maxX)
            //{
            //    if (gameSetting.Player.transform.position.y <= minY) { gameSetting.PlayerPosition = new Vector2(maxX, minY);  }
            //    else if (gameSetting.Player.transform.position.y >= maxY) {gameSetting.PlayerPosition = new Vector2(maxX, maxY);  }
            //    else gameSetting.PlayerPosition = new Vector2(maxX, gameSetting.Player.transform.position.y);
            //}
            //if (gameSetting.Player.transform.position.x <= minX)
            //{
            //    if (gameSetting.Player.transform.position.y <= minY) {gameSetting.PlayerPosition = new Vector2(minX, minY);  }
            //    else if (gameSetting.Player.transform.position.y >= maxY) {gameSetting.PlayerPosition = new Vector2(minX, maxY);  }
            //    else gameSetting.PlayerPosition = new Vector2(minX, gameSetting.Player.transform.position.y);
            //}
            //if (gameSetting.Player.transform.position.y <= minY)
            //{
            //    if (gameSetting.Player.transform.position.x >= maxX){ gameSetting.PlayerPosition = new Vector2(maxX, minY);  }
            //    else if (gameSetting.Player.transform.position.x <= minX) {gameSetting.PlayerPosition = new Vector2(minX, minY);  }
            //    else gameSetting.PlayerPosition = new Vector2(gameSetting.Player.transform.position.x, minY);
            //}
            //if (gameSetting.Player.transform.position.y >= maxY)
            //{
            //    if (gameSetting.Player.transform.position.x >= maxX) {gameSetting.PlayerPosition = new Vector2(maxX, maxY);  }
            //    else if (gameSetting.Player.transform.position.x <= minX) {gameSetting.PlayerPosition = new Vector2(minX, maxY);  }
            //    else gameSetting.PlayerPosition = new Vector2(gameSetting.Player.transform.position.x, maxY);
            //}
        }
        //pos of background
        gameSetting.Player.transform.position = gameSetting.PlayerPosition;
    }
    private void MoveMonsters()
    {
        // background pos move event
        Vector2 newPosition;
        foreach (MonsterObject m in enemyObjects)
        {
            float xPos = gameSetting.PlayerPosition.x - m.Monster.position.x;
            float yPos = gameSetting.PlayerPosition.y - m.Monster.position.y;
            Vector2 toPlayer = new(xPos, yPos);
            newPosition = m.Monster.MovementSpeed/2 * Time.deltaTime * toPlayer.normalized;
            m.Monster.position = newPosition;
            m.transform.Translate(newPosition);
        }
        //foreach (MonsterBase m in gameSetting.Spawner.Monsters)
        //{
        //    newPosition = m.MovementSpeed * Time.deltaTime * m.position - backgroundMat.mainTextureOffset;
        //    m.position = newPosition;
        //    m.destination = gameSetting.PlayerPosition;
        //    foreach (Monster mo in enemyObjects)
        //    {
        //        mo.transform.position = newPosition;
        //    }
        //}
    }
    private void MoveProjectiles()
    {
        foreach (ProjectileObject proj in projectiles)
        {
            //proj.GetProjectile().Destination;
            //get there and disappear
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
        while (Math.Abs(gameSetting.PlayerPosition.x - startPosition.x) > 100 && Math.Abs(gameSetting.PlayerPosition.y - startPosition.y) > 100);

        //set monster object
        MonsterBase newMonster = gameSetting.Spawner.SpawnMonster(startPosition);
        MonsterObject m = MonsterObject.Instantiate(monsterGameObject, playgroundObject.transform);
        m.gameObject.SetActive(true);
        m.transform.position = startPosition;
        m.SetMonster(newMonster);
        //monsterGameObject.spriteRenderer.sprite = newMonster.Sprite;
        m.ChangeSprite(newMonster.Sprite);
        enemyObjects.Add(m);
        //m.spriteRenderer = monsterGameObject.spriteRenderer;
    }
    public void SpawnMonster()
    {
        SpawnMonsterOnLocation();
        // move with backgr
        gameSetting.Spawner.UpdateTargetLocation(gameSetting.PlayerPosition);
    }
    private void DestroyMonster()
    {
        //MonsterBase newMonster = gameSetting.Spawner.SpawnMonster(startPosition);
        //Monster m = Monster.Instantiate(monsterGameObject);
        //m.gameObject.SetActive(true);
        //m.transform.position = startPosition;
        //m.SetMonster(newMonster);
        //enemyObjects.Add(m);
    }
    private void DropItemOnGround(Item item)
    {
        gameSetting.ItemsOnGround.Add(item);
        //droppedItemObjects.Add(ScriptableObject.CreateInstance<ItemOnGround>(item));

    }
    #endregion
    #region Round
    private void TriggerNextRound()
    {
        //open buymenu before next round
        if (gameSetting.Player.CanGoNextRound)
        {
            gameSetting.Player.CanGoNextRound = false;
            gameSetting.RoundNum += 1;
            if (gameSetting.RoundNum > 10) SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
            //delete all entity from map
            gameSetting.Spawner.ClearMonsters();
            gameSetting.ItemsOnGround.Clear();
            roundCanvas.gameObject.SetActive(true);
            StartCoroutine(RoundDelay());
            if(!gameSetting.IsNewGame && !gameSetting.IsLoadedGame && !isSceneJustLoaded) ShowBuyMenu();
        }
        else
        {
            isRoundShown = false;
            roundCanvas.enabled = false;
        }
        isSceneJustLoaded = false;
    }
    public IEnumerator RoundDelay()
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
    public void ShowBuyMenu()
    {
        isBuyMenuShown = true;
        buyMenuCanvas.enabled = true;
        buyMenuCanvas.gameObject.SetActive(true);
        this.enabled = false;
    }
    public void HideBuyMenu()
    {
        isBuyMenuShown = false;
        this.enabled = true;
        buyMenuCanvas.gameObject.SetActive(false);
        buyMenuCanvas.enabled = false;
    }
    #endregion
    #region Collision
    private void Collision()
    {
        //player projectile in action
        //foreach (Monster m in enemyObjects)
        //{
        //    m.M.TakeDamage(gameSetting.Player.Hunter.Attack);
        //}
        //if mob.isdead => add points to player, dropitem
        //if isgameover => go to endgame
    }
    private void ShootProjectile(Vector2 clickPos)
    {
        if (gameSetting.Player.CurrentItem.GetType() == typeof(WeaponBase))
        {
            ProjectileBase bullet = gameSetting.Player.GetProjectile();
            //bullet.Position.normalized
            //bullet.Destination = clickPos;
            //bullet.Rotation = new Quaternion();
            ProjectileObject proj = Instantiate(projectileGameObject, playgroundObject.transform);
            proj.gameObject.SetActive(true);
            proj.transform.position = new Vector2(gameSetting.PlayerPosition.x, gameSetting.PlayerPosition.y - 35);
            proj.SetProjectile(bullet);
            proj.ChangeSprite(bullet.Sprite);
            projectiles.Add(proj);
        }
        else if (gameSetting.Player.CurrentItem.GetType() == typeof(Item))
        {
            // use and remove from inventory
        }
    }
    #endregion
}
