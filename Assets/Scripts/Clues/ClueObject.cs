using EditorAttributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ClueObject : MonoBehaviour
{
    [Tooltip("Path from Room directory. Array for path separators")]
    [SerializeField] string[] clueDestinationPath;

    [SerializeField] ClueSO[] clues;

    public void MakeDifferent()
    {
    }

    [Button(buttonHeight: 36)]
    public void RevealClues(int amount, int fakeAmount)
    {
        HashSet<ClueSO> clueSet = clues.ToHashSet();

        int iterations = Mathf.Min(amount, clues.Length);
        for (int i = 0; i < iterations; i++)
        {
            int index = Random.Range(0, clueSet.Count);
            ClueSO clue = clueSet.ElementAt(index);

            Instantiate(clue.GameworldCluePrefab, transform);
            FileManager.CloneClue(clue.FileCluePath, Path.Combine(clueDestinationPath));
        }

        for (int i = 0; i < fakeAmount; i++)
        {
            FalseClueSO clue = ClueManager.Instance.RandomFalseClue;
            FileManager.CloneClue(clue.FileCluePath, Path.Combine(clueDestinationPath));
        }
    }
}