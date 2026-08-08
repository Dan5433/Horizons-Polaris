using UnityEngine;

public class ClueManager : Singleton<ClueManager>
{
    public static readonly string clueAssetDirectory = "Clues";

    [Tooltip("Have to be generic to work with all clue objects")]
    [SerializeField] FalseClueSO[] redHerrings;
    [SerializeField] ClueObject[] clueObjects;

    public FalseClueSO RandomFalseClue => redHerrings[Random.Range(0, redHerrings.Length)];
}
