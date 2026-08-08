using UnityEngine;

[CreateAssetMenu(menuName = "Clues/Group")]
public class ClueGroup : ScriptableObject
{
    [SerializeField] ClueSO[] clues;
    [SerializeField] FalseClueSO[] falseClues;

    public ClueSO[] Clues => clues;
    public FalseClueSO[] FalseClues => falseClues;
}
