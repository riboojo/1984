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
        ClearChoices();
        SetCursorDefault();
    }

    void Update()
    {
        if (ConversationManager.GetInstance().areChoicesDisplayed())
        {
            CheckMousePosition();
        }
        else
        {
            ClearChoices();
        }
    }

    void CheckMousePosition()
    {
        bool isOverChoice = false;
        Vector3 cursorPosition = CursorManager.GetInstance().GetCursorPosition();
        int numberofChoices = ConversationManager.GetInstance().NumberOfChoices();

        for (int i = 1; i <= numberofChoices; i++)
        {
            if (i == 1)
            {
                if (MouseOverButton1(cursorPosition))
                {
                    isOverChoice = true;
                    break;
                }
            }
            else if (i == 2)
            {
                if (MouseOverButton2(cursorPosition))
                {
                    isOverChoice = true;
                    break;
                }
            }
            else if (i == 3)
            {
                if (MouseOverButton3(cursorPosition))
                {
                    isOverChoice = true;
                    break;
                }
            }
            else if (i == 4)
            {
                if (MouseOverButton4(cursorPosition))
                {
                    isOverChoice = true;
                    break;
                }
            }
            else { /* Do nothing */ }
        }

        if (!isOverChoice)
        {
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = false;

            selectedBtn = -1;

            SetCursorDefault();
        }

        CheckMouseClicked();
    }

    bool MouseOverButton1(Vector3 cursorPosition)
    {
        bool ret = false;

        if ((cursorPosition.x >= delimitersBtn1[0].position.x) && (cursorPosition.x <= delimitersBtn1[1].position.x)
            && (cursorPosition.y <= delimitersBtn1[2].position.y) && (cursorPosition.y >= delimitersBtn1[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = true;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = false;

            selectedBtn = 0;
            ret = true;
        }
        else
        {
            ret = false;
        }

        return ret;
    }

    bool MouseOverButton2(Vector3 cursorPosition)
    {
        bool ret = false;

        if ((cursorPosition.x >= delimitersBtn2[0].position.x) && (cursorPosition.x <= delimitersBtn2[1].position.x)
            && (cursorPosition.y <= delimitersBtn2[2].position.y) && (cursorPosition.y >= delimitersBtn2[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = true;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = false;

            selectedBtn = 1;
            ret = true;
        }
        else
        {
            ret = false;
        }

        return ret;
    }

    bool MouseOverButton3(Vector3 cursorPosition)
    {
        bool ret = false;

        if ((cursorPosition.x >= delimitersBtn3[0].position.x) && (cursorPosition.x <= delimitersBtn3[1].position.x)
            && (cursorPosition.y <= delimitersBtn3[2].position.y) && (cursorPosition.y >= delimitersBtn3[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = true;
            buttonsBacks[3].enabled = false;

            selectedBtn = 2;
            ret = true;
        }
        else
        {
            ret = false;
        }

        return ret;
    }

    bool MouseOverButton4(Vector3 cursorPosition)
    {
        bool ret = false;

        if ((cursorPosition.x >= delimitersBtn4[0].position.x) && (cursorPosition.x <= delimitersBtn4[1].position.x)
            && (cursorPosition.y <= delimitersBtn4[2].position.y) && (cursorPosition.y >= delimitersBtn4[3].position.y))
        {
            SetCursorClicked();
            buttonsBacks[0].enabled = false;
            buttonsBacks[1].enabled = false;
            buttonsBacks[2].enabled = false;
            buttonsBacks[3].enabled = true;

            selectedBtn = 3;
            ret = true;
        }
        else
        {
            ret = false;
        }

        return ret;
    }

    void CheckMouseClicked()
    {
        if (ConversationManager.GetInstance().areChoicesDisplayed())
        {
            if (Input.GetMouseButtonDown(0))
            {
                ConversationManager.GetInstance().MakeChoice(selectedBtn);
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

    public void ClearChoices()
    {
        buttonsBacks[0].enabled = false;
        buttonsBacks[1].enabled = false;
        buttonsBacks[2].enabled = false;
        buttonsBacks[3].enabled = false;
        SetCursorDefault();
    }
}
