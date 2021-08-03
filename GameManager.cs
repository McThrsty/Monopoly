using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public const int maxNumPlayers = 4;
    public sPlayer[] players;
    public int numPlayers;
    public int numHumanPlayers;
    public int curPlayer;
    int currentScene;


    [Space]
    [Header("Game Settings")]
    public float volSound;
    public float volMusic;
    public bool isPlayAnimation;
    public float brightness;
    public bool useHouseRules;
    [NamedArray(typeof(eHouseRules))] public bool[] houseRules;

    public bool isTutorialFinished;
    public bool areSubtitlesActive;

    [Space]
    [Header("Prefabs")]
    public GameObject pCanvasMainMenu;
    public GameObject pSetupScreen;
    public GameObject pOptionsScreen;
    public GameObject pInGame;
    public GameObject pPlayer;
    public GameObject pMarker;
    public GameObject pBoard;
    [NamedArray(typeof(ePiece))] public GameObject[] pPiece; //Represents the prefab for every piece
    
    

    [Space]
    [Header("Instantiated Objects")]
    private cMainMenu c_MainMenu;
    public cInGame c_InGame;

    [Space]
    [Header("Assets")]
    public Color[] playerColors;
    

    [Space]
    [Header("Lookups")]
    [NamedArray(typeof(ePos))] public soSpot[] monoSpots;
    [NamedArray(typeof(eChanceCards))] public cardChance[] cardChances;
    [NamedArray(typeof(eCommChestCards))] public cardCommChest[] cardCommChests;
    [NamedArray(typeof(eDiceRolls))] public Sprite[] diefaces;
    [NamedArray(typeof(eAudio))] public AudioClip[] playSound;
    
    public sBoard s_Board;
    [NamedArray(typeof(ePiece))] public Sprite[] sprPieces;
    [NamedArray(typeof(ePiece))] public string[] strPieces = { "Car", "Top Hat", "Dog", "Cat", "Battleship", "Thimble", "Shoe", "Wheelbarrow" };
    [NamedArray(typeof(ePlayerType))] public Sprite[] sprPlayerType;
    [NamedArray(typeof(ePlayerType))] public string[] strPlayerType = { "None", "Human", "AI" };

    [Space]
    [Header("Audio")]
    public AudioMixerGroup musicMixer;

    private void Awake()
    {
        if (gm == null)
        {
            DontDestroyOnLoad(gameObject);
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (currentScene == 0)
        {
            SetPlayerDefaults();
        }
        
        ChangeMusicVolume(-20f);

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void ChangeMusicVolume(float _newVolume)
    {
        musicMixer.audioMixer.SetFloat("MusicVol", _newVolume);
    }

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene has Loaded: " + scene.name);
        currentScene = scene.buildIndex;
            if (scene.buildIndex == 0) // Load Front End things
        {
            c_MainMenu = Instantiate(pCanvasMainMenu).GetComponent<cMainMenu>();
        }
        else if (scene.buildIndex == 1)
        {
            c_InGame = Instantiate(pInGame).GetComponent<cInGame>();
            c_InGame.InitUI();
            s_Board = Instantiate(pBoard).GetComponent<sBoard>();
            for (int i = 0; i < numPlayers; i++)
            {
                players[i].SpawnPlayerPiece();
            }
            
        }
    }

    public void LoadScene(int _idx)
    {
        SceneManager.LoadScene(_idx);
    }

    public void SetPlayerDefaults()
    {
        players = new sPlayer[maxNumPlayers];

        for (int i = 0; i < maxNumPlayers; i++)
        {
            players[i] = Instantiate(pPlayer, gameObject.transform).GetComponent<sPlayer>();
            players[i].cashOnHand = 1500;
            players[i].playerName = "Player" + (i + 1);
        }

        playerColors = new Color[maxNumPlayers];
        playerColors[0] = Color.blue;
        playerColors[1] = Color.yellow;
        playerColors[2] = Color.magenta;
        playerColors[3] = Color.red;
    }

    public void AdvanceToNextPlayer()
    {
        curPlayer++;
        if (curPlayer >= numPlayers)
        {
            curPlayer = 0;
        }
        c_InGame.DisplayRollDiceButton(true);
        c_InGame.SetHilightedPlayerHUD();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadScene(1);
        }
    }
}
