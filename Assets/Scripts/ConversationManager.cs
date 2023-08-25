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
    private bool disketValid = false;

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

    private int currentAct = 0;

    private const string PLAYER_TAG = "You";
    private const string BROKEN_TAG = "BrokenAI";
    private const string CREATE_TAG = "CreateAI";
    private const string REBEL_TAG = "RebelAI";
    private const string WARRIOR_TAG = "WarriorAI";
    private const string MENTOR_TAG = "MentorAI";

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
        bool valid = false;

        foreach (DisketBehavior disket in diskets)
        {
            if (disket.IsPlugged())
            {
                log.text = "<align=center><b>Conversation Log</b><align=justified>\n\n";
                log.pageToDisplay = 1;
                ret = true;

                if ("EmptySpace" == disket.GetName() && currentAct == 0)
                {
                    currentAct = 1;
                    valid = true;
                }
                else if ("CROP" == disket.GetName() && currentAct == 1)
                {
                    currentAct = 2;
                    valid = true;
                }
                else
                {
                    conversation.text = "<align=center><b>\n\n\n\n\n\nInsert a valid disket</b><align=justified>";
                    disketPlugged = true;
                    ret = false;
                }

                if (valid)
                {
                    conversation.text = "";
                    script = new Story(inkFiles[Array.IndexOf(diskets, disket)].text);
                    ContinueStory();
                    disketPlugged = true;
                    disketValid = true;
                    
                    break;
                }
            }
            else
            {
                conversation.text = "<align=center><b>\n\n\n\n\n\nInsert a valid disket</b><align=justified>";
                ret = false;
            }
        }

        return ret;
    }

    public void DisketUnplugged()
    {
        disketPlugged = false;
        disketValid = false;
        conversation.text = "";
        currentAct = 0;

        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }

        foreach (TextMeshProUGUI choice in choicesText)
        {
            choice.text = "";
        }

        buttons.ClearChoices();
    }

    private void Update()
    {
        if (disketValid)
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
            HandleTags(script.currentTags);
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

        if (disketValid && script != null)
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
        
        if (disketValid && script != null)
        {
            number = numberOfChoices;
        }

        return number;
    }

    public void ShowLog()
    {
        if (disketValid && script != null)
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

    private void HandleTags(List<string> tags)
    {
        if (tags.Count > 0)
        {
            foreach (string tag in tags)
            {
                string[] splitTag = tag.Split(':');

                if (splitTag.Length != 2)
                {
                    Debug.LogError("Incorrect tag: " + tag);
                }

                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();

                if (tagKey == "Speaker")
                {
                    switch (tagValue)
                    {
                        case PLAYER_TAG:
                            conversation.color = Color.black;
                            break;
                        case BROKEN_TAG:
                            conversation.color = Color.magenta;
                            break;
                        case CREATE_TAG:
                            conversation.color = Color.yellow;
                            break;
                        case MENTOR_TAG:
                            conversation.color = Color.green;
                            break;
                        case REBEL_TAG:
                            conversation.color = Color.red;
                            break;
                        case WARRIOR_TAG:
                            conversation.color = Color.blue;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.LogError("Incorrect tag key: " + tagKey);
                }
            }
        }
        else
        {
            conversation.color = Color.black;
        }
    }
}
