using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI conversation;

    private float timer;
    private int charIndex = 0;

    private string newText = null;

    [SerializeField]
    private TextAsset inkFile;

    private Story script;

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

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
            AddText(newText);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
            timer -= Time.deltaTime * 2;
            if (timer <= 0f)
            {
                timer += 0.25f;
                charIndex++;
                conversation.text = newLine.Substring(0, charIndex);

                if (charIndex >= newLine.Length)
                {
                    charIndex = 0;
                    timer = 0f;
                    newLine = null;

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
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        script.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
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
