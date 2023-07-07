using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cursorDefault;
    [SerializeField]
    private GameObject cursorClicked;

    [SerializeField]
    private CursorManager cursorManager;

    [SerializeField]
    ConversationManager conversationManager;

    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Image[] buttonsBacks;

    [SerializeField]
    private Transform[] delimitersBtn1;
    [SerializeField]
    private Transform[] delimitersBtn2;
    [SerializeField]
    private Transform[] delimitersBtn3;
    [SerializeField]
    private Transform[] delimitersBtn4;

    private int selectedBtn = -1;

    void Start()
    {
        SetCursorDefault();
    }

    void Update()
    {
        if (conversationManager.areChoicesDisplayed())
        {
            CheckMousePosition();
        }
    }

    void CheckMousePosition()
    {
        Vector3 cursorPosition = cursorManager.GetCursorPosition();

        if ((cursorPosition.x >= delimitersBtn1[0].position.x) && (cursorPosition.x <= delimitersBtn1[1].position.x)
            && (cursorPosition.y <= delimitersBtn1[2].position.y) && (cursorPosition.y >= delimitersBtn1[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = true;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = false;

            selectedBtn = 0;
        }
        else if ((cursorPosition.x >= delimitersBtn2[0].position.x) && (cursorPosition.x <= delimitersBtn2[1].position.x)
            && (cursorPosition.y <= delimitersBtn2[2].position.y) && (cursorPosition.y >= delimitersBtn2[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = true;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = false;

            selectedBtn = 1;
        }
        else if ((cursorPosition.x >= delimitersBtn3[0].position.x) && (cursorPosition.x <= delimitersBtn3[1].position.x)
            && (cursorPosition.y <= delimitersBtn3[2].position.y) && (cursorPosition.y >= delimitersBtn3[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = true;
            buttonsBacks[3].enabled = false;

            selectedBtn = 2;
        }
        else if ((cursorPosition.x >= delimitersBtn4[0].position.x) && (cursorPosition.x <= delimitersBtn4[1].position.x)
            && (cursorPosition.y <= delimitersBtn4[2].position.y) && (cursorPosition.y >= delimitersBtn4[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = true;

            selectedBtn = 3;
        }
        else
        {
            SetCursorDefault();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = false;

            selectedBtn = -1;
        }
    }

    void ChechMouseClicked()
    {
        if (conversationManager.areChoicesDisplayed())
        {
            if (Input.GetMouseButtonDown(0))
            {
                conversationManager.MakeChoice(selectedBtn);
            }
        }
    }

    void SetCursorDefault()
    {
        cursorDefault.SetActive(true);
        cursorClicked.SetActive(false);
    }

    void SetCursorClicked()
    {
        cursorDefault.SetActive(false);
        cursorClicked.SetActive(true);
    }
}
