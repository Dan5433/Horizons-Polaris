using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using Unity.Multiplayer.Center.Common;
using UnityEngine.UI;
using JetBrains.Annotations;

public class startScreen : MonoBehaviour
{
    public TMP_Text myText;
    public Image spriteRenderer;

    [SerializeField]
    public string selectedAns;

    public List<string> ans = new List<string>{"The Horizon", "A Cake", "The Stars", "Music", "A Blåhaj"};

    public float delayBeforeFade = 1f;   // wait time before fading starts
    public float fadeDuration = 1.5f;    // how long the fade takes
    void Start()
    {
       // Unity's Random.Range is inclusive of min, exclusive of max for ints
        int randomIndex = UnityEngine.Random.Range(0, ans.Count);
        selectedAns = ans[randomIndex];
        myText.text = "<align=center>What question gives this Answer?</align>\n" + $"<align=center><font=\"Jersey10-Regular SDF\">{selectedAns}</font></align>" + "\n<align=center>Search around the room to find some clues</align>";
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

        // Ensure it's fully transparent at the end
        myText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        spriteRenderer.color = new Color(squareColor.r, squareColor.g, squareColor.b, 0f);
        // remove element
        myText.gameObject.SetActive(false);
        spriteRenderer.gameObject.SetActive(false);
    }
}

