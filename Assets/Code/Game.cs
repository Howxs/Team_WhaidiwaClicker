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

    // Use this for initialization
    void Start () {

        //CLICKER
        currentScore =0;
        hitPower =1;
        scoreIncreasedPerSecond =1;
        x =0f;

    }

    // Update is called once per frame
    void Update () {

        //CLICKER
        scoreText.text = "Heart Score: " + (int)currentScore + " Point";
        scoreIncreasedPerSecond = x * Time.deltaTime;
        currentScore = currentScore + scoreIncreasedPerSecond;

        //SHOP
        shop1text.text = "Tier 1: " + shop1prize + " $";
        shop2text.text = "Tier 2: " + shop2prize + " $";

        //AMOUNT
        amount1Text.text = "Tier 1: " + amount1 + " arts $: " + amount1Profit + "/s";
        amount2Text.text = "Tier 2: " + amount2 + " arts $: " + amount2Profit + "/s";

    }

    //HIT
    public void Hit() {

        currentScore += hitPower;

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
}
