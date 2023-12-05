using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;

    [SerializeField]
    private Camera mainCamera, menuCamera;

    [SerializeField]
    private Animator menuAnim;

    [SerializeField]
    private MenuCameraController menuController;
    
    [SerializeField]
    NotepadBehavior notepad;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    EjectButtonBehavior ejectButton;

    [SerializeField]
    Sprite[] endingImages;

    [SerializeField]
    GameObject[] endingHolders;

    [SerializeField]
    Animator sceneLights;

    [SerializeField]
    GameObject normalPC;

    [SerializeField]
    GameObject breakablePC;

    [SerializeField]
    Transform breakablePCPos;

    [SerializeField]
    AudioSource explotion;

    [SerializeField]
    GameObject Act2Disket;

    [SerializeField]
    Material Act2DisketAv, Act2DisketUnav;

    public enum GameState
    {
        Intro = 0,
        LookingAround,
        ObjectSelected,
        PlayingDisket,
        Default
    }

    public enum GameEnds
    {
        WalkAway = 0,
        Rebel,
        Warrior,
        Creative,
        Mentor,
        Secret
    }

    public enum GameActs
    {
        Act1 = 0,
        Act2,
        Act3
    }

    private GameState state = GameState.Intro;

    private bool screenFocused = false;
    private bool shiftPressed = false;

    private bool movingCameraToMain = false;
    private bool movingCameraToMenu = false;

    private int currentAct = 0;

    private Dictionary<GameEnds, bool> endings = new Dictionary<GameEnds, bool>()
    {
        { GameEnds.WalkAway, false },
        { GameEnds.Warrior, false },
        { GameEnds.Creative, false },
        { GameEnds.Mentor, false },
        { GameEnds.Rebel, false },
        { GameEnds.Secret, false }
    };

    public void LoadData(Dictionary<MainGameManager.GameEnds, bool> data)
    {
        endings = data;
    }

    public void SaveData(ref Dictionary<MainGameManager.GameEnds, bool> data)
    {
        data = endings;
    }

    private void Awake()
    {
        if ((instance != null) && (instance != this))
        {
            Debug.LogError("Instance of StateMachine has already been created!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static MainGameManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        MenuUpdateEndings();
    }

    private void Update()
    {
        MainStateMachine();
        //TestEndings();
    }

    void MainStateMachine()
    {
        switch (state)
        {
            case GameState.Intro:
                StateIntro();
                break;
            case GameState.LookingAround:
                StateLookingAround();
                break;
            case GameState.ObjectSelected:
                StateObjectSelected();
                break;
            case GameState.PlayingDisket:
                StatePlayingDisket();
                break;
            case GameState.Default:
                StateDefault();
                break;
            default:
                break;
        }
    }

    public GameState GetState()
    {
        return state;
    }

    public void SetState(GameState newSatate)
    {
        state = newSatate;
    }

    private void StateIntro()
    {
        if (movingCameraToMain)
        {
            EnableMainCamera();
        }
        else if (movingCameraToMenu)
        {
            EnableMenuCamera();
        }
        else
        {
            CursorManager.GetInstance().SetCursorStatus(false);
            mainMenu.SetActive(true);
            menuCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }

    private void StateLookingAround()
    {
        UpdateScreenStatus();
    }

    private void StateObjectSelected()
    {

    }

    private void StatePlayingDisket()
    {
        UpdateScreenStatus();
        UpdateConversationLog();
    }

    private void StateDefault()
    {
        UpdateScreenStatus();
        UpdateConversationLog();
    }

    private void UpdateScreenStatus()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !shiftPressed)
        {
            shiftPressed = true;
            screenFocused = !screenFocused;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftPressed = false;
        }

        if (screenFocused)
        {
            ActivateScreen();
        }
        else
        {
            DeactivateScreen();
        }
    }

    private void ActivateScreen()
    {
        CursorManager.GetInstance().SetCursorStatus(true);

        mainCamera.GetComponent<CameraMovement>().CenterCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", true);
    }

    private void DeactivateScreen()
    {
        CursorManager.GetInstance().SetCursorStatus(false);

        mainCamera.GetComponent<CameraMovement>().MoveCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", false);
    }

    private void UpdateConversationLog()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            ConversationManager.GetInstance().ShowLog();
        }
        else
        {
            ConversationManager.GetInstance().HideLog();
        }
    }

    private void StartGame()
    {
        menuCamera.enabled = false;
        mainCamera.enabled = true;
        mainCamera.GetComponentInParent<CameraMovement>().CenterCamera();

        SetState(GameState.Default);
    }

    private void EnableMainCamera()
    {
        if (!movingCameraToMain)
        {
            movingCameraToMain = true;
            mainMenu.SetActive(false);
            menuAnim.SetTrigger("start");
        }

        if (menuController.IsAnimPlaying() == MenuCameraController.AnimState.Ended)
        {
            movingCameraToMain = false;
            mainCamera.GetComponentInParent<CameraMovement>().CenterCamera();
            notepad.ShowNotepad();
            StartGame();
        }
        else { /* Do nothing */ }
    }

    private void EnableMenuCamera()
    {
        if (!movingCameraToMenu)
        {
            movingCameraToMenu = true;
            mainCamera.GetComponentInParent<CameraMovement>().CenterCamera();
            menuCamera.enabled = true;
            mainCamera.enabled = false;
            menuAnim.SetTrigger("end");
        }

        if (menuController.IsAnimPlaying() == MenuCameraController.AnimState.Ended)
        {
            MenuUpdateEndings();
            movingCameraToMenu = false;
        }
    }

    private void MenuUpdateEndings()
    {
        int i = 0;
        foreach (var item in endings)
        {
            if (item.Value == false)
            {
                endingHolders[i].GetComponent<Image>().sprite = endingImages[0];
            }
            else
            {
                endingHolders[i].GetComponent<Image>().sprite = endingImages[i+1];
            }

            i++;
        }
    }

    private void MenuLoadEndings()
    {

    }

    public int GetCurrentAct()
    {
        return currentAct;
    }

    public void SetCurrentAct(int act)
    {
        currentAct = act;

        if ((int)GameActs.Act3 == currentAct)
        {
            Act2Disket.GetComponent<MeshRenderer>().material = Act2DisketAv;
        }
        else
        {
            Act2Disket.GetComponent<MeshRenderer>().material = Act2DisketUnav;
        }
    }

    public void EndGame(GameEnds end)
    {
        switch (end)
        {
            case GameEnds.WalkAway:
                PerformWalkAwayEnding();
                break;
            case GameEnds.Warrior:
                PerformWarriorEnding();
                break;
            case GameEnds.Creative:
                PerformCreativeEnding();
                break;
            case GameEnds.Mentor:
                PerformMentorEnding();
                break;
            case GameEnds.Rebel:
                PerformRebelEnding();
                break;
            default:
                break;
        }

        endings[end] = true;

        SetCurrentAct(0);
        SetState(GameState.Intro);
    }

    private void PerformWalkAwayEnding()
    {
        ejectButton.EjectDisket();
        shiftPressed = false;
        screenFocused = false;
        DeactivateScreen();
        EnableMenuCamera();
    }

    private void PerformWarriorEnding()
    {
        PerformWalkAwayEnding();
    }

    private void PerformCreativeEnding()
    {
        PerformWalkAwayEnding();
    }

    private void PerformMentorEnding()
    {
        sceneLights.SetTrigger("ending");
        PerformWalkAwayEnding();
    }

    private void PerformRebelEnding()
    {
        normalPC.SetActive(false);
        GameObject breakable = Instantiate(breakablePC, breakablePCPos);

        Breakable[] pieces = breakable.GetComponentsInChildren<Breakable>();

        explotion.Play();

        foreach (Breakable piece in pieces)
        {
            piece.CreateExplosion();
        }

        PerformWalkAwayEnding();
    }

    private void TestEndings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PerformRebelEnding();
        }
    }

    public void MenuStartGame()
    {
        EnableMainCamera();
    }
}
