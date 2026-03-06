using UnityEngine;
using System.IO;

// โครงสร้างข้อมูลสำหรับเซฟ
[System.Serializable]
public class GameData
{
    public float currentScore = 0f;
    public float hitPower = 1f;
    public float x = 0f;

    public int shop1prize = 25;
    public int shop2prize = 125;
    public int amount1 = 0;
    public float amount1Profit = 0f;
    public int amount2 = 0;
    public float amount2Profit = 0f;

    public int upgradePrize = 50;

    public int level = 1;
    public int exp = 0;
    public int expToNextLevel = 10;

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
            DontDestroyOnLoad(gameObject); // ทำให้ SaveManager ไม่ถูกทำลายตอนเปลี่ยนฉาก
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
        // ดึงข้อมูลล่าสุดจากหน้าจอเกมมาเก็บไว้ก่อนเซฟลงไฟล์
        Game activeGame = FindObjectOfType<Game>();
        if (activeGame != null)
        {
            currentData.currentScore = activeGame.currentScore;
            currentData.hitPower = activeGame.hitPower;
            currentData.x = activeGame.x;
            currentData.shop1prize = activeGame.shop1prize;
            currentData.shop2prize = activeGame.shop2prize;
            currentData.amount1 = activeGame.amount1;
            currentData.amount1Profit = activeGame.amount1Profit;
            currentData.amount2 = activeGame.amount2;
            currentData.amount2Profit = activeGame.amount2Profit;
            currentData.upgradePrize = activeGame.upgradePrize;
            currentData.level = activeGame.level;
            currentData.exp = activeGame.exp;
            currentData.expToNextLevel = activeGame.expToNextLevel;
            currentData.multiplierCost = activeGame.multiplierCost;
            currentData.multiplier = activeGame.multiplier;
        }

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
            currentData = new GameData();
        }
    }

    // ฟังก์ชันลบไฟล์เซฟ
    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted!");
        }
        currentData = new GameData(); // รีเซ็ตข้อมูลกลับเป็นค่าเริ่มต้น
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