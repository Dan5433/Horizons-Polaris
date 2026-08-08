using System.IO;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        string gameRootDirectory = Path.GetDirectoryName(Application.dataPath);
        Application.OpenURL("file://" + gameRootDirectory);
    }
}
