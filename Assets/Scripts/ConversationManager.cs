using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    private static ConversationManager instance;

    [SerializeField]
    TextMeshProUGUI conversation;

    [SerializeField]
    ButtonManager buttons;

    private float timer;
    private int charIndex = 0;

    private string newText = null;

    [SerializeField]
    private DisketBehavior[] diskets;

    [SerializeField]
    private TextAsset[] inkFiles;

    private Story script;

    private bool disketPlugged = false;

    [SerializeField]
    private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    private bool spaceReleased = false;

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
}
