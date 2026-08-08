using System.IO;
using UnityEngine;

public class FileManager : Singleton<FileManager>
{
    public static string RoomDirectoryPath => Path.Combine(Path.GetDirectoryName(Application.dataPath), Instance.roomDirectoryName);

    [SerializeField] string roomDirectoryName;

    void Start()
    {
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
            if (fileName.EndsWith(".meta"))
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
}