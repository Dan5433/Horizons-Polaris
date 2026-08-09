using System.IO;
using UnityEngine;

public class FileManager : Singleton<FileManager>
{
    public static string RoomDirectoryPath => Path.Combine(Path.GetDirectoryName(Application.dataPath), Instance.roomDirectoryName);

    [SerializeField] string roomDirectoryName;

    protected override void Awake()
    {
        base.Awake();
        CloneEmptyRoom();
    }

    void CloneEmptyRoom()
    {
        string emptyRoomStreamingPath = Path.Combine(Application.streamingAssetsPath, Instance.roomDirectoryName);
        string emptyRoomCopyPath = RoomDirectoryPath;
        CloneDirectory(emptyRoomStreamingPath, emptyRoomCopyPath);
    }

    void CloneDirectory(string sourcePath, string destinationPath)
    {
        if (!Directory.Exists(sourcePath))
        {
            Debug.LogError($"[Clone Failed] Source directory does not exist: {sourcePath}");
            return;
        }

        Directory.CreateDirectory(destinationPath);

        foreach (string file in Directory.GetFiles(sourcePath))
        {
            string fileName = Path.GetFileName(file);
            if (fileName.EndsWith(".meta") || fileName.StartsWith('.'))
                continue;

            string copyDestination = Path.Combine(destinationPath, fileName);
            File.Copy(file, copyDestination, overwrite: true);
        }

        foreach (string directory in Directory.GetDirectories(sourcePath))
        {
            string directoryName = Path.GetFileName(directory);
            string copyDestination = Path.Combine(destinationPath, directoryName);
            CloneDirectory(directory, copyDestination);
        }
    }

    public static void CloneClue(string assetPath, string roomDestinationPath)
    {
        string fileExtension = Path.GetExtension(assetPath);
        int number = Random.Range(1, 1000);
        string fileName = $"~clue{number}~{fileExtension}";

        string sourcePath = Path.Combine(Application.streamingAssetsPath, ClueManager.clueAssetDirectory, assetPath);
        string destinationPath = Path.Combine(RoomDirectoryPath, roomDestinationPath, fileName);
        File.Copy(sourcePath, destinationPath, overwrite: true);
    }

    void OnApplicationQuit()
    {
        Directory.Delete(RoomDirectoryPath, recursive: true);
    }
}