using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClueObject : MonoBehaviour
{
    [SerializeField] ClueSO[] clues;

    public void MakeDifferent()
    {
    }

    [Button(buttonHeight: 36)]
    public void RevealClues(int amount)
    {
        HashSet<ClueSO> clueSet = clues.ToHashSet();

        int iterations = Mathf.Min(amount, clues.Length);
        for (int i = 0; i < iterations; i++)
        {
            int index = Random.Range(0, clueSet.Count);
            ClueSO clue = clueSet.ElementAt(index);

            Instantiate(clue.GameworldCluePrefab, transform);
            FileManager.CloneClue(clue.FileCluePath, clue.ClueDestinationPath);
        }
    }
}
