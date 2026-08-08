using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenuManager : MonoBehaviour
{
    [SerializeField] GameObject buttonPanel; // parent object holding the 5 buttons

    [SerializeField] TMP_Text answerText;
    [SerializeField] Image panel;
    [SerializeField] string selectedAns;
    [SerializeField] float delayBeforeFade = 2f;
    [SerializeField] float fadeDuration = 1.5f;

    [Header("Option Buttons")]
    [SerializeField] Button[] optionButtons;

    private Dictionary<string, List<string>> questionBank = new Dictionary<string, List<string>>
    {
        { "The Horizon", new List<string>{
            "Where is a place where the earth touches the sky?",
            "What is the name of this Hackathon?",
            "What is the name of a famous disaster? Deepwater ____?",
            "What line can you see but never reach?",
            "Fill in the blank: _______ Polaris?"
        }},
        { "A Cake", new List<string>{
            "What are we eating at the party?",
            "What is the dog's favourite food?",
            "What has multiple layers?",
            "What is made with flour, sugar, eggs, fat, and other ingredients?",
            "What food are candles put on?"
        }},
        { "A Blahaj", new List<string>{
            "What is basically a shark?",
            "What can only be bought at Ikea?",
            "What has a heart and soul?",
            "What can be loved and lost?",
            "What can be bought for 4 approved hours on the Horizons shop?"
        }},
        { "The Stars", new List<string>{
            "What is the origin of the \"star\" shape?",
            "What is the sun an example of?",
            "What is seen when looking up on a clear day?",
            "What is a prominent part of Horizons: Stardance?",
            "What symbol represents the great unknown?"
        }},
        { "Music", new List<string>{
            "What unites people all across the world?",
            "What is made up of many, many different instruments?",
            "What made Taylor Swift famous?",
            "What is seen on Sheet Music?",
            "What is played on a Guitar?"
        }},
    };

    void Awake()
    {
        RunAnswerSetup();
    }

    public void DelayAnswerSetup()
    {
        // waits 2 seconds and restarts the program with new answers + questions
        Invoke(nameof(RunAnswerSetup), 2f);
    }

    void RunAnswerSetup()
    {
        // reset visuals -> element visuals, color, transparecy
        answerText.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        answerText.color = new Color(answerText.color.r, answerText.color.g, answerText.color.b, 1f);
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1f);

        List<string> answers = new(questionBank.Keys);
        int randomIndex = Random.Range(0, answers.Count);
        selectedAns = answers[randomIndex];

        // intro text
        answerText.text = "<align=center>What question gives this Answer?</align>\n" +
            $"<align=center><font=\"Jersey10-Regular SDF\">{selectedAns}</font></align>" +
            "\n<align=center>Search around the room to find some clues</align>";

        // append questions to option butons 
        List<string> questions = questionBank[selectedAns];
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < questions.Count)
            {
                TMP_Text buttonText = optionButtons[i].GetComponentInChildren<TMP_Text>();
                if (buttonText != null)
                    buttonText.text = questions[i];
            }
        }

        StopAllCoroutines(); // prevents overlapping fades if triggered again mid-fade
        StartCoroutine(FadeTextAfterDelay());
    }

    IEnumerator FadeTextAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;
        Color textColor = answerText.color;
        Color squareColor = panel.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            answerText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            panel.color = new Color(squareColor.r, squareColor.g, squareColor.b, alpha);
            yield return null;
        }

        answerText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        panel.color = new Color(squareColor.r, squareColor.g, squareColor.b, 0f);
        answerText.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        buttonPanel.SetActive(!buttonPanel.activeSelf);
    }
}