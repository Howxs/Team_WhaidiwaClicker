using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour
{
    //CLICKER
    public TextMeshProUGUI scoreText;
    public float currentScore;
    public float hitPower;
    public float scoreIncreasedPerSecond;
    public float x;

    //SHOP
    public float shop1prize;
    public TextMeshProUGUI shop1text;
    public float shop2prize;
    public TextMeshProUGUI shop2text;

    //AMOUNT
    public TextMeshProUGUI amount1Text;
    public float amount1;
    public float amount1Profit;
    public TextMeshProUGUI amount2Text;
    public float amount2;
    public float amount2Profit;

    //UPGRADE
    public float upgradePrize;
    public TextMeshProUGUI upgradeText;

    //LEVEL SYSTEM
    public float level;
    public float exp;
    public float expToNextLevel;
    public TextMeshProUGUI levelText;

    //MULTIPLIER
    public float multiplierCost = 500;
    public TextMeshProUGUI multiplierCostText;
    public float multiplier = 1f;
    public TextMeshProUGUI multiplierText;

    //SHOP BUTTON OBJECTS
    public GameObject ShopButton;
    public GameObject BackButton;

    // HEART BOUNCE ANIMATION
    public Transform heartTransform;
    public float bounceSize = 0.85f;
    public float bounceDuration = 0.1f;
    private Vector3 originalHeartScale;

    // STAGE 2 (เปลี่ยนรูปเมื่อถึงเลเวล 5)
    public Image targetButtonImage;
    public Sprite newSprite;
    private Sprite defaultSprite;

    // ------------------------------------
    // UPGRADE ANIMATION (นำกลับมาแล้ว!)
    // ------------------------------------
    public Image animatedImage;
    public GameObject Image;
    public Sprite[] upgradeAnimationSprites;
    public float animationSpeed = 0.2f;

    private bool animationStarted = false;
    private int animationIndex = 0;
    private float animationTimer = 0f;

    // Use this for initialization
    void Start()
    {
        // เก็บค่าขนาดเริ่มต้นของหัวใจ
        if (heartTransform != null)
        {
            originalHeartScale = heartTransform.localScale;
        }

        // เก็บรูปภาพตั้งต้นเอาไว้ เผื่อเวลาย้อนกลับไปเลเวล 1 (ปุ่ม Release)
        if (targetButtonImage != null)
        {
            defaultSprite = targetButtonImage.sprite;
        }

        // โหลดข้อมูลจาก SaveManager มาแสดงผล
        if (SaveManager.Instance != null && SaveManager.Instance.currentData != null)
        {
            GameData data = SaveManager.Instance.currentData;
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

            // เช็คว่าถ้าเคยอัปเกรดไปแล้ว ให้แสดงอนิเมชั่นเลยตอนโหลดเกม
            if (hitPower > 1f)
            {
                ImageOn();
                animationStarted = true;
            }
        }
        else
        {
            ResetVariables();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //CLICKER
        scoreText.text = "Heart Score: " + currentScore.ToString("0") + " Point"; ;
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

        // เช็คเลเวลเพื่อเปลี่ยนรูป
        if (level >= 5 && targetButtonImage != null && newSprite != null)
        {
            targetButtonImage.sprite = newSprite;
        }

        // ------------------------------------
        // UPGRADE ANIMATION LOOP
        // ------------------------------------
        if (animationStarted && upgradeAnimationSprites != null && upgradeAnimationSprites.Length > 0)
        {
            animationTimer += Time.deltaTime;

            if (animationTimer >= animationSpeed)
            {
                animationTimer = 0f;
                animationIndex++;

                if (animationIndex >= upgradeAnimationSprites.Length)
                {
                    animationIndex = 0;
                }

                if (animatedImage != null)
                {
                    animatedImage.sprite = upgradeAnimationSprites[animationIndex];
                }
            }
        }
    }

    //HIT
    public void Hit()
    {
        currentScore += hitPower * multiplier;
        exp++;

        // เรียกใช้อนิเมชั่นหัวใจเด้ง
        if (heartTransform != null)
        {
            StopCoroutine("BounceRoutine");
            StartCoroutine("BounceRoutine");
        }
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

            // เรียกใช้อนิเมชั่นตอนอัปเกรด
            if (!animationStarted)
            {
                ImageOn();
                animationStarted = true;
            }
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

    //SHOP BUTTON SHOW/CLOSE OBJECT
    public void ShowShopObject()
    {
        if (ShopButton != null) ShopButton.SetActive(true);
    }

    public void CloseShopObject()
    {
        if (ShopButton != null) ShopButton.SetActive(false);
    }

    public void ImageOn()
    {
        if (Image != null) Image.SetActive(true);
    }

    // RELEASE BUTTON (ปุ่มลบเซฟ)
    public void ReleaseGame()
    {
        // 1. สั่งให้ SaveManager ลบไฟล์ทิ้ง
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.DeleteSave();
        }

        // 2. รีเซ็ตตัวเลขบนหน้าจอทั้งหมดกลับเป็นค่าเริ่มต้น
        ResetVariables();
    }

    // ฟังก์ชันสำหรับรีเซ็ตตัวแปรกลับเป็นศูนย์
    private void ResetVariables()
    {
        currentScore = 0;
        hitPower = 1;
        x = 0f;
        shop1prize = 25;
        shop2prize = 125;
        amount1 = 0;
        amount1Profit = 0f;
        amount2 = 0;
        amount2Profit = 0f;
        upgradePrize = 50;
        level = 1;
        exp = 0;
        expToNextLevel = 10;
        multiplierCost = 500;
        multiplier = 1f;

        // เปลี่ยนรูปหัวใจกลับเป็นรูปเดิมตอนเลเวล 1
        if (targetButtonImage != null && defaultSprite != null)
        {
            targetButtonImage.sprite = defaultSprite;
        }

        // ปิดอนิเมชั่นและซ่อนรูปอัปเกรด
        animationStarted = false;
        animationIndex = 0;
        animationTimer = 0f;
        if (Image != null) Image.SetActive(false);
    }

    // อนิเมชั่นให้หัวใจยุบและพองออก
    private IEnumerator BounceRoutine()
    {
        heartTransform.localScale = originalHeartScale * bounceSize;
        float timer = 0f;

        while (timer < bounceDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / bounceDuration;
            heartTransform.localScale = Vector3.Lerp(originalHeartScale * bounceSize, originalHeartScale, progress);
            yield return null;
        }

        heartTransform.localScale = originalHeartScale;
    }
}