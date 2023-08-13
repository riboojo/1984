using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    private static ConversationManager instance;

    [SerializeField]
    TextMeshProUGUI conversation;

    [SerializeField]
    TextMeshProUGUI log;

    [SerializeField]
    ButtonManager buttons;

    [SerializeField]
    private DisketBehavior[] diskets;

    [SerializeField]
    private TextAsset[] inkFiles;

    [SerializeField]
    private GameObject[] choices;

    [SerializeField]
    private GameObject scrollUpperIcon;

    [SerializeField]
    private GameObject scrollBottomIcon;

    private Story script;

    private bool disketPlugged = false;

    private float timer;
    private int charIndex = 0;

    private string newText = null;

    private TextMeshProUGUI[] choicesText;

    private bool spaceReleased = false;

    private int numberOfChoices = 0;
    private int MAX_CHOICES = 4;

    private bool isLogShown = false;
    private bool isUpArrowPressed = false;
    private bool isDownArrowPressed = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
    }

    public static ConversationManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        int choiceIndex = 0;

        conversation.text = "";

        choicesText = new TextMeshProUGUI[choices.Length];
        foreach (GameObject choice in choices)
        {
            choicesText[choiceIndex] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.SetActive(false);
            choiceIndex++;
        }
    }

    public bool NewDisketPlayed()
    {
        bool ret = false;

        foreach (DisketBehavior disket in diskets)
        {
            if (disket.IsPlugged())
            {
                log.text = "<align=center><b>Conversation Log</b><align=justified>\n\n";
                log.pageToDisplay = 1;

                script = new Story(inkFiles[Array.IndexOf(diskets, disket)].text);
                ContinueStory();
                disketPlugged = true;
                ret = true;
                break;
            }
        }

        return ret;
    }

    public void DisketUnplugged()
    {
        disketPlugged = false;
        conversation.text = "";

        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    private void Update()
    {
        if (disketPlugged)
        {
            if (charIndex != 0)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    spaceReleased = true;
                }

                AddText(newText);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spaceReleased = false;
                    ContinueStory();
                }
            }
        }
    }

    void ContinueStory()
    {
        if (script.canContinue)
        {
            newText = script.Continue();
            AddText(newText);
            AddTextToLog();
        }
    }

    void AddText(string newLine)
    {
        if (newLine != null)
        {
            if (spaceReleased && Input.GetKeyDown(KeyCode.Space))
            {
                AddAllText(newLine);
            }
            else
            {
                AddNewChar(newLine);
            }
        }
    }

    void AddAllText(string newText)
    {
        conversation.text = newText;

        charIndex = 0;
        timer = 0f;

        DisplayChoices();
    }

    void AddNewChar(string newText)
    {
        if (newText != null)
        {
            timer -= Time.deltaTime * 2;
            if (timer <= 0f)
            {
                timer += 0.25f;

                charIndex++;
                conversation.text = newText.Substring(0, charIndex);

                if (charIndex >= newText.Length)
                {
                    charIndex = 0;
                    timer = 0f;

                    DisplayChoices();
                }
            }

        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = script.currentChoices;
        numberOfChoices = script.currentChoices.Count;

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;

            buttons.ClearChoices();
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (-1 < choiceIndex)
        {
            script.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    public bool areChoicesDisplayed()
    {
        bool ret = false;

        if (disketPlugged && script != null)
        {
            if (script.currentChoices.Count > 0 && charIndex == 0)
            {
                ret = true;
            }
        }

        return ret;
    }

    public int NumberOfChoices()
    {
        int number = 0;
        
        if (disketPlugged && script != null)
        {
            number = numberOfChoices;
        }

        return number;
    }

    public void ShowLog()
    {
        if (disketPlugged && script != null)
        {
            CursorManager.GetInstance().HideCursor();

            if (!isLogShown)
            {
                for (int i = 0; i < numberOfChoices; i++)
                {
                    choices[i].gameObject.SetActive(false);
                }

                conversation.enabled = false;
                log.enabled = true;
                isLogShown = true;
            }
            else
            {
                CheckScroll();
            }
        }
    }

    public void HideLog()
    {
        CursorManager.GetInstance().ShowCursor();

        for (int i = 0; i < numberOfChoices; i++)
        {
            choices[i].gameObject.SetActive(true);
        }

        log.enabled = false;
        conversation.enabled = true;

        scrollBottomIcon.SetActive(false);
        scrollUpperIcon.SetActive(false);

        isLogShown = false;
    }

    private void CheckScroll()
    {
        if ((log.textInfo.pageCount > 1) && (log.pageToDisplay != log.textInfo.pageCount))
        {
            scrollBottomIcon.SetActive(true);
        }
        else
        {
            scrollBottomIcon.SetActive(false);
        }

        if (log.pageToDisplay > 1)
        {
            scrollUpperIcon.SetActive(true);
        }
        else
        {
            scrollUpperIcon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUpArrowPressed = true;
        }
        else
        {
            if (isUpArrowPressed)
            {
                if (log.pageToDisplay > 1)
                {
                    log.pageToDisplay--;
                }
                isUpArrowPressed = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDownArrowPressed = true;
        }
        else
        {
            if (isDownArrowPressed)
            {
                if ((log.textInfo.pageCount > 1) && (log.pageToDisplay != log.textInfo.pageCount))
                {
                    log.pageToDisplay++;
                }

                isDownArrowPressed = false;
            }
        }
    }

    private void AddTextToLog()
    {
        log.text += newText;
    }
}
