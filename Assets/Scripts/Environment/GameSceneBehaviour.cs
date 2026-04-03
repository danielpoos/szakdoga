using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneBehaviour : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject itemGameObject;
    [SerializeField] private Material backgroundMat;
    [SerializeField] private Canvas buyMenu;
    [SerializeField] private GameObject inventoryLine;
    [SerializeField] private InventoryUI inventoryExtended;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    [SerializeField] private Canvas roundCanvas;

    private Vector2 backgroundOffset;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer itemSpriteRenderer;
    private bool isBuyMenuShown = false;
    private bool isInventoryShown = false;
    private void Awake()
    {
        boxCollider = playerObject.GetComponent<BoxCollider2D>();
        rigidBody = playerObject.GetComponent<Rigidbody2D>();
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
        itemSpriteRenderer = itemGameObject.GetComponent<SpriteRenderer>();
        gameSetting.IsGamePaused = false;
        gameSetting.Spawner = new MonsterSpawner();
        // set player
        if (gameSetting.Player.Score == 0)
        {
            scoreText.enabled = false;
        }
        playerSpriteRenderer.sprite = gameSetting.Player.Hunter.Sprite;
        Item asd = new ItemBase(ItemType.Beer);
        //Debug.Log(asd.Sprite);
        gameSetting.Player.SetWeapon();
        //foreach(Item i in gameSetting.Player.Inventory.GetItems()) Debug.Log(i.Sprite);
        //itemSpriteRenderer.sprite = gameSetting.Player.Hunter.Weapon.GetSprite();
        //inventoryExtended.enabled = true;

        //inventoryExtended.SetInventory(gameSetting.Player.Inventory);
    }
    void Start()
    {
        TriggerNextRound();
        //gameSetting.Player.CurrentItem.Sprite;
        backgroundOffset = gameSetting.Player.Hunter.position;
    }
    void Update()
    {
        MovePlayer();
        MoveScene();
        if (Input.anyKeyDown)
        {
            OnKeyDown();
        }
        
    }
    void FixedUpdate()
    {
        gameSetting.Timer += Time.fixedDeltaTime;
        UpdateTexts();
    }
    private void MoveScene()
    {
        //rigidBody.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //transform.position = backgroundOffset + movement * parallax;
        backgroundMat.mainTextureOffset += (gameSetting.Player.Hunter.MovementSpeed/10) * Time.deltaTime * gameSetting.Player.Hunter.position.normalized;

        // directional movement
        // floors ???
        // rotate sprite
        // select sprite
        // use sprite animations
        // rigidBody.MovePosition(rigidBody.position + hunter.MovementSpeed * Time.unscaledDeltaTime * movementDirection);
    }
    private void MovePlayer()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 playerPos = gameSetting.Player.Hunter.MovementSpeed * Time.deltaTime * new Vector2(horizontal, vertical);
        // walk inside an invisible rectangle
        Debug.Log(Math.Abs(playerObject.transform.position.x) +" "+Math.Abs(playerObject.transform.position.y));
        gameSetting.Player.Hunter.position = playerPos;
        playerObject.transform.Translate(playerPos);
        if (Math.Abs(playerObject.transform.position.x) < 300 && Math.Abs(playerObject.transform.position.y) < 150)
        {
            gameSetting.PlayerPosition = playerObject.transform.position;
        }
        else
        {
            if (playerObject.transform.position.x >= 300)
            {
                if (playerObject.transform.position.y <= -150)
                {
                    gameSetting.PlayerPosition = new Vector2(300, -150);
                }
                else if (playerObject.transform.position.y >= 150)
                {
                    gameSetting.PlayerPosition = new Vector2(300, 150);
                }
                else gameSetting.PlayerPosition = new Vector2(300, playerObject.transform.position.y);
            }
            if (playerObject.transform.position.x <= -300)
            {
                if (playerObject.transform.position.y <= -150)
                {
                    gameSetting.PlayerPosition = new Vector2(-300, -150);
                }
                else if (playerObject.transform.position.y >= 150)
                {
                    gameSetting.PlayerPosition = new Vector2(-300, 150);
                }
                else gameSetting.PlayerPosition = new Vector2(-300, playerObject.transform.position.y);
            }
            if (playerObject.transform.position.y <= -150)
            {
                if (playerObject.transform.position.x >= 300)
                {
                    gameSetting.PlayerPosition = new Vector2(300, -150);
                }
                else if (playerObject.transform.position.x <= -300)
                {
                    gameSetting.PlayerPosition = new Vector2(-300, -150);
                }
                else gameSetting.PlayerPosition = new Vector2(playerObject.transform.position.x, -150);
            }
            if (playerObject.transform.position.y >= 150)
            {
                if (playerObject.transform.position.x >= 300)
                {
                    gameSetting.PlayerPosition = new Vector2(300, 150);
                }
                else if (playerObject.transform.position.x <= -300)
                {
                    gameSetting.PlayerPosition = new Vector2(-300, 150);
                }
                else gameSetting.PlayerPosition = new Vector2(playerObject.transform.position.x, 150);
            }
        }
        //pos of background
        playerObject.transform.position = gameSetting.PlayerPosition;
    }
    private void ChangePlayerDirection()
    {

    }
    private void TriggerNextRound()
    {
        // TODO
        gameSetting.RoundNum += 1;
        TMP_Text round = roundCanvas.transform.Find("RoundText").GetComponent<TMP_Text>();
        round.text = $"Round {gameSetting.RoundNum}";
        roundCanvas.enabled = true;
        StartCoroutine(InvokeDelay());
        roundCanvas.enabled = false;
    }
    private IEnumerator InvokeDelay()
    {
        yield return new WaitForSeconds(1.5f);
    }
    private void IncreaseStatsBasedOnDifficulty()
    {
        int multiplier = gameSetting.DiffInt+1;
    }
    public void Pause()
    {
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Single);
        Debug.Log("Game paused");
    }
    private void UpdateTexts()
    {
        moneyText.text = "$ " + gameSetting.Player.Money;
        if (gameSetting.Player.Score != 0)
        {
            scoreText.text = ""+gameSetting.Player.Score;
            scoreText.enabled = true;
        }
        int hour = (int)(gameSetting.Timer / 3600);
        int min = (int)(gameSetting.Timer / 60);
        int sec = (int)(gameSetting.Timer % 60);
        TimeSpan ts = new(hour,min,sec);
        timeText.text = $"{ts:c}";
    }
    private void OnKeyDown()
    {
        if (Input.GetKey(gameSetting.options.shortcut.Pause))
        {
            if (!gameSetting.IsGamePaused)
            {
                Pause();
                // megallitas kozpontositasa
            }
        }
        //if (Input.GetKey(gameSetting.options.shortcut.Options))
        //{
        //    //open options
        //    SceneManager.LoadScene("OptionMenu", LoadSceneMode.Single);
        //    //pause game
        //}
        if (Input.GetKey(gameSetting.options.shortcut.Save))
        {
            //save
        }
        if (Input.GetKey(gameSetting.options.shortcut.Load))
        {
            //load
        }
        if (Input.GetKey(gameSetting.options.shortcut.Prevweapon))
        {
            //prev weapon
            // cannot cycle one weapon
        }
        if (Input.GetKey(gameSetting.options.shortcut.Nextweapon))
        {
            //next weapon
            // cannot cycle one weapon
        }
        if (Input.GetKey(gameSetting.options.shortcut.Buymenu))
        {
            //buy menu
            isBuyMenuShown = !isBuyMenuShown;
            buyMenu.enabled = isBuyMenuShown;
        }
        if (Input.GetKey(gameSetting.options.shortcut.Inventory))
        {
            //inventory
            isInventoryShown = !isInventoryShown;
            inventoryExtended.enabled = isInventoryShown;
        }
        if (Input.GetKey(gameSetting.options.shortcut.Interact))
        {
            //interact
            //throw item
        }
    }
}
