using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int woodCount;
    public int questIndex;
    public int wellPhase;
}

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void SaveGame()
    {
        var data = new SaveData
        {
            woodCount = Inventory.Instance != null ? Inventory.Instance.woodCount : 0,
            questIndex = UnityEngine.Object.FindObjectOfType<QuestBoard>()?.CurrentIndex ?? 0,
            wellPhase = UnityEngine.Object.FindObjectOfType<VillageProgress>()?.CurrentPhaseIndex ?? 0
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            return new SaveData();
        }

        var json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
