using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI conversation;

    [SerializeField]
    ButtonManager buttons;

    private float timer;
    private int charIndex = 0;

    private string newText = null;

    [SerializeField]
    private TextAsset inkFile;

    private Story script;

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private bool spaceReleased = false;

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

        script = new Story(inkFile.text);
    }

    private void Update()
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

        if (script.currentChoices.Count > 0 && charIndex == 0)
        {
            ret = true;
        }

        return ret;
    }
}
