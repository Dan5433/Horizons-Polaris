using UnityEngine;

public class ClueManager : Singleton<ClueManager>
{
    public static readonly string clueAssetDirectory = "Clues";

    [SerializeField] FalseClueSO[] redHerrings;
    [SerializeField] ClueObject[] clueObjects;


}
