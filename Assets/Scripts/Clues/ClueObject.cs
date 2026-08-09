using EditorAttributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClueObject : MonoBehaviour
{
    [Tooltip("Path from Room directory. Array for path separators")]
    [SerializeField] string[] clueDestinationPath;

    [SerializeField] ClueGroup[] questionClues;
    [SerializeField] string[] clueGroupMatching;
    ClueGroup selectedGroup;

    [Button(buttonHeight: 36)]
    public void RevealClueGroup(string answer, int clueAmount, int fakeClueAmount)
    {
        int answerIndex = System.Array.IndexOf(clueGroupMatching, answer);
        selectedGroup = questionClues[answerIndex];

        RevealClues(clueAmount, fakeClueAmount);
    }

    [Button(buttonHeight: 36)]
    void RevealClues(int amount, int fakeAmount)
    {
        HashSet<ClueSO> clueSet = selectedGroup.Clues.ToHashSet();

        int iterations = Mathf.Min(amount, clueSet.Count);
        for (int i = 0; i < iterations; i++)
        {
            int index = Random.Range(0, clueSet.Count);
            ClueSO clue = clueSet.ElementAt(index);
            clueSet.Remove(clue);

            Instantiate(clue.GameworldCluePrefab, transform);
            FileManager.CloneClue(clue.FileCluePath, Path.Combine(clueDestinationPath));
        }

        //allow double takes of false clues
        List<FalseClueSO> falseClueSet = selectedGroup.FalseClues.ToList();
        falseClueSet.AddRange(falseClueSet);

        iterations = Mathf.Min(fakeAmount, falseClueSet.Count);
        for (int i = 0; i < iterations; i++)
        {
            FalseClueSO clue = falseClueSet[Random.Range(0, falseClueSet.Count)];
            falseClueSet.Remove(clue);

            FileManager.CloneClue(clue.FileCluePath, Path.Combine(clueDestinationPath));
        }
    }
}