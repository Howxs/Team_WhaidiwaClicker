using UnityEngine;
using System.IO;

// 1. โครงสร้างข้อมูลที่รวมตัวแปรทั้งหมดจาก Game.cs
[System.Serializable]
public class GameData
{
    // CLICKER
    public float currentScore = 0f;
    public float hitPower = 1f;
    public float x = 0f;

    // SHOP (เซฟราคาและจำนวนที่ซื้อไปแล้ว)
    public int shop1prize = 25; // ใส่ค่าเริ่มต้นไว้เผื่อเล่นครั้งแรก
    public int shop2prize = 125;
    public int amount1 = 0;
    public float amount1Profit = 0f;
    public int amount2 = 0;
    public float amount2Profit = 0f;

    // UPGRADE
    public int upgradePrize = 50;

    // LEVEL SYSTEM
    public int level = 1;
    public int exp = 0;
    public int expToNextLevel = 10;

    // MULTIPLIER
    public int multiplierCost = 500;
    public float multiplier = 1f;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public GameData currentData;
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Application.persistentDataPath + "/HeartRescueSave.json";
        LoadGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            currentData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game Loaded!");
        }
        else
        {
            // ถ้าเล่นครั้งแรก จะสร้างข้อมูลตั้งต้นให้ (จะดึงค่าจากด้านบนมาใช้)
            currentData = new GameData();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}