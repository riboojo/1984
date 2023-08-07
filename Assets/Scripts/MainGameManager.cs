using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;

    [SerializeField]
    private GameObject blur;

    [SerializeField]
    private GameObject guides;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    GameObject UIText_Guides;

    public enum GameState
    {
        Intro = 0,
        LookingAround,
        ObjectSelected,
        PlayingDisket
    }

    private GameState state = GameState.LookingAround;
    
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
    }

    private void UpdateScreenStatus()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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
        //blur.SetActive(true);
        //guides.SetActive(false);
        UIText_Guides.GetComponent<TextMeshProUGUI>().text = "Tip: Press Space to continue";
        CursorManager.GetInstance().SetCursorStatus(true);

        mainCamera.GetComponent<CameraMovement>().CenterCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", true);
    }

    private void DeactivateScreen()
    {
        //blur.SetActive(false);
        //guides.SetActive(true);
        UIText_Guides.GetComponent<TextMeshProUGUI>().text = "Tip: Hold Shift to focus";
        CursorManager.GetInstance().SetCursorStatus(false);

        mainCamera.GetComponent<CameraMovement>().MoveCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", false);
    }

}
