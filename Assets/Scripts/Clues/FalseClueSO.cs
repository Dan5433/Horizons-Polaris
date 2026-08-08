using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Clues/False Clue")]
public class FalseClueSO : ScriptableObject
{
    [Tooltip("Path from StreamingAssets/Clues directory. Array for path separators")]
    [SerializeField] string[] fileCluePath;

    public string FileCluePath => Path.Combine(fileCluePath);
}
