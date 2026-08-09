using EditorAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClueManager : Singleton<ClueManager>
{
    public static readonly string clueAssetDirectory = "Clues";

    [SerializeField] ClueObjectsMatch[] clueObjects;
    [SerializeField] int clueAmount, falseClueAmount;
    [SerializeField][Range(0, 1)] float clueObjectRevealFraction = 1;

    [Button(buttonHeight: 36)]
    public void RevealClues(string selectedAnswer)
    {
        ClueObject[] revealObjects = clueObjects.FirstOrDefault(obj => obj.matchingAnswer == selectedAnswer).clueObjects;
        if (revealObjects == null)
            return;

        int amountToReveal = (int)(revealObjects.Length * clueObjectRevealFraction);
        HashSet<ClueObject> fullSet = revealObjects.ToHashSet();

        for (int i = 0; i < amountToReveal; i++)
        {
            int randomIndex = Random.Range(0, fullSet.Count);
            ClueObject reveal = fullSet.ElementAt(randomIndex);

            reveal.RevealClueGroup(selectedAnswer, clueAmount, falseClueAmount);
            fullSet.Remove(reveal);
        }
    }

    [Serializable]
    struct ClueObjectsMatch
    {
        public string matchingAnswer;
        public ClueObject[] clueObjects;
    }
}
