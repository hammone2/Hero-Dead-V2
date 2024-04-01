using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomExtensions;


using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameBehaviour : MonoBehaviour, IManager
{
    private string _state;
    // 3
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public Text ammoText;
    public Text healthText;
    public Text itemText;

    public bool showWinScreen = false;

    public bool showLossScreen = false;

    public string labelText = "OBJECTIVE: Find the keycard and escape!";
    public int maxItems = 4;
    public int maxHealth = 100;
    public int gunAmmo = 0;
    public int flameAmmo = 0;

    private int _itemsCollected = 0;

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    public Stack<string> lootStack = new Stack<string>();


    // 1
    public int Items
    {
        // 2
        get {
            return _itemsCollected;
        }
        // 3
        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                //Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }
    public int _playerHP = 10;
    // 4
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);

            if (_playerHP <= 0)
            {
                labelText = "You died!";
                showLossScreen = true;
                //Time.timeScale = 0;
            }
            else
            {
                labelText = "You've been damaged!";
            }
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        Initialize();

        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);

    }
    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();

        debug(_state);
        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");
        // 2
        PlayerController playerBehavior = player.GetComponent<PlayerController>();
        // 3
        playerBehavior.playerJump += HandlePlayerJump;

        lootStack.Push("Plasma SMG");
        lootStack.Push("HP+");
        lootStack.Push("Golden Keycard");
        lootStack.Push("Plasma Ammo");
        lootStack.Push("Flamethrower");
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    void OnGUI()
    {
        ammoText.text = gunAmmo.ToString();
        healthText.text = _playerHP.ToString();
        itemText.text = _itemsCollected.ToString();


        if (showWinScreen)
        {
            SceneManager.LoadScene(2);
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                // 2
                catch (System.ArgumentException e)
                {
                    // 3
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());

                }
                // 4
                finally
                {
                    debug("Restart handled...");

                }
            }
        }

    }
    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        // 2
        var nextItem = lootStack.Peek();
        // 3
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }
}
