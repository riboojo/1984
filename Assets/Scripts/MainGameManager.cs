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
    Light menuLight;

    [SerializeField]
    NotepadBehavior notepad;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    EjectButtonBehavior ejectButton;

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
        Empty
    }

    private GameState state = GameState.Intro;

    private bool screenFocused = false;
    private bool shiftPressed = false;
    
    private bool movingCameraToMain = false;
    private bool movingCameraToMenu = false;

    private int currentAct = 0;

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

    void Update()
    {
        MainStateMachine();
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
            menuLight.enabled = true;
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
        menuLight.enabled = false;
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
            movingCameraToMenu = false;
        }
    }

    public int GetCurrentAct()
    {
        return currentAct;
    }

    public void SetCurrentAct(int act)
    {
        currentAct = act;
    }

    public void EndGame(GameEnds end)
    {
        SetCurrentAct(0);

        ejectButton.EjectDisket();
        shiftPressed = false;
        screenFocused = false;
        DeactivateScreen();
        EnableMenuCamera();

        SetState(GameState.Intro);
    }

    public void MenuStartGame()
    {
        EnableMainCamera();
    }
}
