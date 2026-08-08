using UnityEngine;

[CreateAssetMenu(menuName = "Clues/Clue")]
public class ClueSO : FalseClueSO
{
    [SerializeField] GameObject gameworldCluePrefab;

    public GameObject GameworldCluePrefab => gameworldCluePrefab;
}
