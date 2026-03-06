using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


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

    //STAGE2
    public Image targetButtonImage;
    public Sprite newSprite;

    //UPGRADE ANIMATION
    public Image animatedImage;
    public GameObject Image;
    public Sprite[] upgradeAnimationSprites;
    public float animationSpeed = 0.2f;

    private bool animationStarted = false;
    private int animationIndex = 0;
    private float animationTimer = 0f;

    //SHOP BUTTON SHOW OBJECT
    public GameObject ShopButton;

    //SHOP BUTTON Close OBJECT
    public GameObject BackButton;


    // Use this for initialization
    void Start () {

        //CLICKER
        currentScore =0;
        hitPower =1;
        scoreIncreasedPerSecond =1;
        x =0f;

        //LEVEL
        level = 1;
        exp = 0;
        expToNextLevel = 10;

    }

    // Update is called once per frame
    void Update () {

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

        //STAGE2
        if (level >= 5)
        {
            targetButtonImage.sprite = newSprite;
        }

        //UPGRADE ANIMATION LOOP
        if (animationStarted && upgradeAnimationSprites.Length > 0)
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

                animatedImage.sprite = upgradeAnimationSprites[animationIndex];
            }
        }



    }

    //HIT
    public void Hit() {

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

    //SHOP BUTTON SHOW OBJECT
    public void ShowShopObject()
    {
        ShopButton.SetActive(true);
    }

    //SHOP BUTTON Close OBJECT
    public void CloseShopObject()
    {
        BackButton.SetActive(false);
    }
    //imageOn
    public void ImageOn()
    {
        Image.SetActive(true);
    }
}
