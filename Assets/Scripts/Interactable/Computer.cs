using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Application.OpenURL("file://" + FileManager.RoomDirectoryPath);
    }
}
