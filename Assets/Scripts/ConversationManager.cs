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
    private GameObject scrollUpperIcon, scrollBottomIcon;

    [SerializeField]
    private Color PlayerColorTag, BrokenColorTag, CreateColorTag, RebellionColorTag, WarriorColorTag, MentorColorTag, ConversationColorTag;

    private Story script;

    private bool disketPlugged = false;
    private bool disketValid = false;

    private float timer;
    private int charIndex = 0;
    private int charNameIndex = 0;

    private string newText = null;

    private TextMeshProUGUI[] choicesText;

    private bool spaceReleased = false;

    private int numberOfChoices = 0;
    private int MAX_CHOICES = 4;

    private bool isLogShown = false;
    private bool isUpArrowPressed = false;
    private bool isDownArrowPressed = false;

    private int currentAct = 0;

    private bool isWrittingName = false;
    private Color currentSpeakerColor = Color.gray;
    private string currentSpeaker = "Default";
    private string coloredSpeaker = "";

    private const string PLAYER_TAG = "You";
    private const string BROKEN_TAG = "BrokenAI";
    private const string CREATE_TAG = "CreateAI";
    private const string REBEL_TAG = "RebelAI";
    private const string WARRIOR_TAG = "WarriorAI";
    private const string MENTOR_TAG = "MentorAI";

    private Dictionary<string, string> TAGS_NAMES = new Dictionary<string, string>()
    {
        {PLAYER_TAG, "> "},
        {BROKEN_TAG, "Broken: "},
        {CREATE_TAG, "Create: "},
        {REBEL_TAG, "Rebellion: "},
        {WARRIOR_TAG, "Battle: "},
        {MENTOR_TAG, "Guru: "}
    };

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
            if ((charIndex != 0) || (charNameIndex != 0))
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
            conversation.text = "";
            newText = script.Continue();

            HandleEmpties();
            HandleTags(script.currentTags);
            AddText(newText);
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
        string coloredSpeaker = "<color=#" + ColorUtility.ToHtmlStringRGB(currentSpeakerColor) + ">" + currentSpeaker + "</color>";
        string coloredText = "<color=#" + ColorUtility.ToHtmlStringRGB(ConversationColorTag) + ">" + newText + "</color>";

        conversation.text = coloredSpeaker + coloredText;

        charIndex = 0;
        charNameIndex = 0;
        timer = 0f;

        DisplayChoices();
        AddTextToLog();
    }

    void AddNewChar(string newText)
    {
        if (newText != null)
        {
            timer -= Time.deltaTime * 2;
            if (timer <= 0f)
            {
                timer += 0.25f;
                
                string colored = "";
                
                if (IsWrittingName())
                {
                    string plain = currentSpeaker.Substring(0, charNameIndex);
                    colored = "<color=#" + ColorUtility.ToHtmlStringRGB(ColorConversation(currentSpeaker)) + ">" + plain + "</color>";
                    coloredSpeaker = colored;
                    charNameIndex++;
                }
                else
                {
                    string plain = newText.Substring(0, charIndex);
                    colored = coloredSpeaker + "<color=#" + ColorUtility.ToHtmlStringRGB(ColorConversation("Default")) + ">" + plain + "</color>";
                    charIndex++;
                }

                conversation.text = colored;

                if ((charIndex + charNameIndex) > (newText.Length + currentSpeaker.Length))
                {
                    coloredSpeaker = "";
                    charIndex = 0;
                    charNameIndex = 0;
                    timer = 0f;

                    DisplayChoices();
                    AddTextToLog();
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
        log.text = conversation.text;
    }

    private void HandleEmpties()
    {
        if (newText == "\n")
        {
            newText = script.Continue();
        }
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
                    SetCurrentSpeaker(tagValue);
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

    private Color GetSpeakerColor(string tag)
    {
        Color ret = ConversationColorTag;

        switch (tag)
        {
            case PLAYER_TAG:
                ret = PlayerColorTag;
                break;
            case BROKEN_TAG:
                ret = BrokenColorTag;
                break;
            case CREATE_TAG:
                ret = CreateColorTag;
                break;
            case MENTOR_TAG:
                ret = MentorColorTag;
                break;
            case REBEL_TAG:
                ret = RebellionColorTag;
                break;
            case WARRIOR_TAG:
                ret = WarriorColorTag;
                break;
            default:
                ret = ConversationColorTag;
                break;
        }

        return ret;
    }

    private void SetCurrentSpeaker(string tag)
    {
        currentSpeakerColor = GetSpeakerColor(tag);

        foreach (var dict in TAGS_NAMES)
        {
            if (dict.Key == tag)
            {
                isWrittingName = true;
                currentSpeaker = dict.Value;
                break;
            }
        }
    }

    private bool IsWrittingName()
    {
        bool ret = false;

        if (conversation.text.Contains(currentSpeaker))
        {
            ret = false;
        }
        else
        {
            ret = true;
        }

        return ret;
    }

    private Color ColorConversation(string tag)
    {
        Color ret; 

        if (IsWrittingName())
        {
            ret = currentSpeakerColor;
        }
        else
        {
            ret = ConversationColorTag;
        }

        return ret;
    }
}
