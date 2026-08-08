using UnityEngine;

public class ClueManager : Singleton<ClueManager>
{
    public static readonly string clueAssetDirectory = "Clues";

    [Tooltip("Have to be generic to work with all clue objects")]
    [SerializeField] ClueObject[] clueObjects;

    protected override void Awake()
    {
        base.Awake();
        //choose answer index and set clue objects selected group to respective index
    }
}
