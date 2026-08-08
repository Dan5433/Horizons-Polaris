using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class startScreen : MonoBehaviour
{
    public TMP_Text myText;
    public Image spriteRenderer;

    [SerializeField]
    public string selectedAns;

    public float delayBeforeFade = 1f;
    public float fadeDuration = 1.5f;

    [Header("Option Buttons")]
    public List<Button> optionButtons = new List<Button>();

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
        { "A Blåhaj", new List<string>{
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

    // used on first call 
    void Start()
    {
        Begin();
    }

    public void RunAnswerSetup()
    {
        // waits 2 seconds and restarts the program with new answers + questions
        Invoke(nameof(Begin), 2f);
    }
    // public so can be called from button OnClick
    public void Begin()
    {
        // reset visuals -> element visuals, color, transparecy
        myText.gameObject.SetActive(true);
        spriteRenderer.gameObject.SetActive(true);
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 1f);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

        List<string> answers = new List<string>(questionBank.Keys);
        int randomIndex = UnityEngine.Random.Range(0, answers.Count);
        selectedAns = answers[randomIndex];

        // intro text
        myText.text = "<align=center>What question gives this Answer?</align>\n" +
            $"<align=center><font=\"Jersey10-Regular SDF\">{selectedAns}</font></align>" +
            "\n<align=center>Search around the room to find some clues</align>";

        // append questions to option butons 
        List<string> questions = questionBank[selectedAns];
        for (int i = 0; i < optionButtons.Count; i++)
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
        Color textColor = myText.color;
        Color squareColor = spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            myText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            spriteRenderer.color = new Color(squareColor.r, squareColor.g, squareColor.b, alpha);
            yield return null;
        }

        myText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        spriteRenderer.color = new Color(squareColor.r, squareColor.g, squareColor.b, 0f);
        myText.gameObject.SetActive(false);
        spriteRenderer.gameObject.SetActive(false);
    }
}