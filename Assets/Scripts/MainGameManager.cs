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
    GameObject menuBack;

    [SerializeField]
    Light menuLight;

    [SerializeField]
    NotepadBehavior notepad;

    public enum GameState
    {
        Intro = 0,
        LookingAround,
        ObjectSelected,
        PlayingDisket,
        Default
    }

    private GameState state = GameState.Intro;

    private bool screenFocused = false;
    private bool shiftPressed = false;

    private bool mouseClicked = false;
    private bool movingCamera = false;

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
        if (movingCamera)
        {
            UpdateMenuCameraPosition();
        }
        else if (!Input.GetMouseButton(0) && mouseClicked)
        {
            movingCamera = true;
            mouseClicked = false;
        }
        else
        {
            menuLight.enabled = true;
            menuCamera.enabled = true;
            mainCamera.enabled = false;
            menuBack.SetActive(false);

            Ray ray = menuCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit currentHit;
            if (Physics.Raycast(ray, out currentHit))
            {
                if (currentHit.transform.tag == "UISelectable")
                {
                    menuBack.SetActive(true);

                    if (Input.GetMouseButton(0))
                    {
                        mouseClicked = true;
                    }
                }
            }
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


    private void StartGamePressed()
    {
        menuLight.enabled = false;
        mainCamera.enabled = true;
        menuCamera.enabled = false;
        menuBack.SetActive(false);

        SetState(GameState.Default);
    }

    private void UpdateMenuCameraPosition()
    {
        float step = 0.75f * Time.deltaTime;
        menuCamera.transform.position = Vector3.MoveTowards(menuCamera.transform.position, mainCamera.transform.position, step);
        menuCamera.transform.rotation = Quaternion.RotateTowards(menuCamera.transform.rotation, mainCamera.transform.rotation, 0.1f);
        
        if (Vector3.Distance(menuCamera.transform.position, mainCamera.transform.position) < 0.01f)
        {
            movingCamera = false;
            notepad.ShowNotepad();
            StartGamePressed();
            //mainCamera.GetComponentInParent<CameraMovement>().CenterCamera();
        }
    }
}
