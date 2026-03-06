using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO; // เพิ่มเข้ามาเพื่อจัดการการเซฟไฟล์

// 1. สร้าง Class สำหรับเก็บตัวแปรทั้งหมดที่ต้องการเซฟ
[System.Serializable]
public class GameSaveData
{
    public float currentScore;
    public float hitPower;
    public float x;

    public int shop1prize;
    public int shop2prize;
    public int amount1;
    public float amount1Profit;
    public int amount2;
    public float amount2Profit;

    public int upgradePrize;
    public int level;
    public int exp;
    public int expToNextLevel;

    public int multiplierCost;
    public float multiplier;
}

public class Game : MonoBehaviour
{
    //CLICKER
    public TextMeshProUGUI scoreText;
    public float currentScore;
    public float hitPower;
    public float scoreIncreasedPerSecond;
    public float x;

    //SHOP
    public int shop1prize;
    public TextMeshProUGUI shop1text;
    public int shop2prize;
    public TextMeshProUGUI shop2text;

    //AMOUNT
    public TextMeshProUGUI amount1Text;
    public int amount1;
    public float amount1Profit;
    public TextMeshProUGUI amount2Text;
    public int amount2;
    public float amount2Profit;

    //UPGRADE
    public int upgradePrize;
    public TextMeshProUGUI upgradeText;

    //LEVEL SYSTEM
    public int level;
    public int exp;
    public int expToNextLevel;
    public TextMeshProUGUI levelText;

    //MULTIPLIER
    public int multiplierCost = 500;
    public TextMeshProUGUI multiplierCostText;
    public float multiplier = 1f;
    public TextMeshProUGUI multiplierText;

    // ตัวแปรสำหรับเก็บที่อยู่ไฟล์เซฟ
    private string saveFilePath;

    // Use this for initialization
    void Start()
    {
        // กำหนดที่อยู่ไฟล์เซฟลงในเครื่อง
        saveFilePath = Application.persistentDataPath + "/HeartClickerSave.json";

        // สั่งโหลดเกมทันทีที่เปิดฉากขึ้นมา
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {

        //CLICKER
        scoreText.text = "Heart Score: " + (int)currentScore + " Point";
        scoreIncreasedPerSecond = (x * multiplier) * Time.deltaTime;
        currentScore = currentScore + scoreIncreasedPerSecond;

        //SHOP
        shop1text.text = "Tier 1: " + shop1prize + " $";
        shop2text.text = "Tier 2: " + shop2prize + " $";

        //AMOUNT
        amount1Text.text = "Tier 1: " + amount1 + " arts $: " + amount1Profit + "/s";
        amount2Text.text = "Tier 2: " + amount2 + " arts $: " + amount2Profit + "/s";

        //UPGRADE
        upgradeText.text = "Cost: " + upgradePrize + " $";

        //LEVEL
        if (exp >= expToNextLevel)
        {
            level++;
            exp = 0;
            expToNextLevel *= 2;
        }

        levelText.text = level + " level";

        //MULTIPLIER
        multiplierText.text = "Multiplier: x" + multiplier;
        multiplierCostText.text = "Multiplier Cost: " + multiplierCost + " $";

    }

    //HIT
    public void Hit()
    {
        currentScore += hitPower * multiplier;
        //EXP
        exp++;
    }

    //SHOP
    public void Shop1()
    {
        if (currentScore >= shop1prize)
        {
            currentScore -= shop1prize;
            amount1 += 1;
            amount1Profit += 1;
            x += 1;
            shop1prize += 25;
        }
    }

    public void Shop2()
    {
        if (currentScore >= shop2prize)
        {
            currentScore -= shop2prize;
            amount2 += 1;
            amount2Profit += 5;
            x += 5;
            shop2prize += 125;
        }
    }

    //UPGRADE
    public void Upgrade()
    {
        if (currentScore >= upgradePrize)
        {
            currentScore -= upgradePrize;
            hitPower *= 2;
            upgradePrize += 50;
        }
    }

    //MULTIPLIER
    public void BuyMultiplier()
    {
        if (currentScore >= multiplierCost)
        {
            currentScore -= multiplierCost;
            multiplier += 1f;
            multiplierCost *= 2;
        }
    }

    // ---------------------------------------------------
    // ระบบ SAVE & LOAD (ส่วนที่เพิ่มเข้ามาใหม่)
    // ---------------------------------------------------

    public void SaveGame()
    {
        // 1. นำค่าปัจจุบันใส่ลงใน Class เซฟ
        GameSaveData data = new GameSaveData();
        data.currentScore = currentScore;
        data.hitPower = hitPower;
        data.x = x;
        data.shop1prize = shop1prize;
        data.shop2prize = shop2prize;
        data.amount1 = amount1;
        data.amount1Profit = amount1Profit;
        data.amount2 = amount2;
        data.amount2Profit = amount2Profit;
        data.upgradePrize = upgradePrize;
        data.level = level;
        data.exp = exp;
        data.expToNextLevel = expToNextLevel;
        data.multiplierCost = multiplierCost;
        data.multiplier = multiplier;

        // 2. แปลงเป็น JSON แล้วเซฟลงเครื่อง
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // ถ้าเคยเล่นแล้วและมีไฟล์เซฟ ให้ดึงข้อมูลมาทับตัวแปรปัจจุบัน
            string json = File.ReadAllText(saveFilePath);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

            currentScore = data.currentScore;
            hitPower = data.hitPower;
            x = data.x;
            shop1prize = data.shop1prize;
            shop2prize = data.shop2prize;
            amount1 = data.amount1;
            amount1Profit = data.amount1Profit;
            amount2 = data.amount2;
            amount2Profit = data.amount2Profit;
            upgradePrize = data.upgradePrize;
            level = data.level;
            exp = data.exp;
            expToNextLevel = data.expToNextLevel;
            multiplierCost = data.multiplierCost;
            multiplier = data.multiplier;
        }
        else
        {
            // ถ้าเป็นการเล่นครั้งแรก (ไม่มีเซฟ) ให้ตั้งค่าเริ่มต้นที่นี่
            currentScore = 0;
            hitPower = 1;
            x = 0f;
            level = 1;
            exp = 0;
            expToNextLevel = 10;

            // หมายเหตุ: ตัวแปรอื่นๆ เช่น ราคาช็อป (shop1prize) ถ้าไม่มีในนี้ 
            // ระบบจะใช้ค่าตั้งต้นที่คุณกรอกไว้ในหน้าต่าง Inspector ของ Unity ครับ
        }
    }

    // เซฟอัตโนมัติเมื่อพับหน้าจอเกม (แอปลงไปอยู่เบื้องหลัง)
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) SaveGame();
    }

    // เซฟอัตโนมัติเมื่อกดปิดเกม
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}