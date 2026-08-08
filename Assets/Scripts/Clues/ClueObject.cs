using EditorAttributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ClueObject : MonoBehaviour
{
    [Tooltip("Path from Room directory. Array for path separators")]
    [SerializeField] string[] clueDestinationPath;

    [SerializeField] ClueGroup[] questionClues;
    ClueGroup selectedGroup;

    [Button(buttonHeight: 36)]
    public void MakeDifferent(int answerIndex)
    {
        selectedGroup = questionClues[answerIndex];
        //RevealClues();
    }

    [Button(buttonHeight: 36)]
    public void RevealClues(int amount, int fakeAmount)
    {
        HashSet<ClueSO> clueSet = selectedGroup.Clues.ToHashSet();

        int iterations = Mathf.Min(amount, selectedGroup.Clues.Length);
        for (int i = 0; i < iterations; i++)
        {
            int index = Random.Range(0, clueSet.Count);
            ClueSO clue = clueSet.ElementAt(index);

            Instantiate(clue.GameworldCluePrefab, transform);
            FileManager.CloneClue(clue.FileCluePath, Path.Combine(clueDestinationPath));
        }

        HashSet<FalseClueSO> falseClueSet = selectedGroup.FalseClues.ToHashSet();

        iterations = Mathf.Min(fakeAmount, selectedGroup.FalseClues.Length);
        for (int i = 0; i < iterations; i++)
        {
            int index = Random.Range(0, falseClueSet.Count);
            FalseClueSO clue = falseClueSet.ElementAt(index);
            FileManager.CloneClue(clue.FileCluePath, Path.Combine(clueDestinationPath));
        }
    }
}