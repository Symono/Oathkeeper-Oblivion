using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerData playerData;
    private int currentStage;

    private string savePath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/save.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(PlayerData player, int stage)
    {
        playerData = player;
        currentStage = stage;

        string json = JsonUtility.ToJson(new SaveData(playerData, currentStage));
        File.WriteAllText(savePath, json);
    }

    public PlayerData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            playerData = saveData.player;
            currentStage = saveData.stage;

            Debug.Log("Player Name: " + playerData.playerName);
            Debug.Log("Player Level: " + playerData.level);
            Debug.Log("Current Stage: " + currentStage);

            return playerData;
        }
        else
        {
            Debug.Log("Save file not found.");
            return null;
        }
    }

    // Data structure to hold player data and stage
    [System.Serializable]
    private class SaveData
    {
        public PlayerData player;
        public int stage;

        public SaveData(PlayerData player, int stage)
        {
            this.player = player;
            this.stage = stage;
        }
    }
}
