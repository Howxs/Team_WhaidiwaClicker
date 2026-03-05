using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    //CLICKER
    public TextMeshProUGUI scoreText;
    public float currentScore;
    public float hitPower;
    public float scoreIncreasedPerSecond;
    public float x;

    // Use this for initialization
    void Start () {

        //CLICKER
        currentScore =0;
        hitPower =1;
        //scoreIncreasedPerSecond =1;
        x =0f;

    }

    // Update is called once per frame
    void Update () {

        //CLICKER
        scoreText.text = "Heart Score: " + (int)currentScore + " Point";
        //currentScore += scoreIncreasedPerSecond * Time.deltaTime;

    }

    //HIT
    public void Hit() {

        currentScore += hitPower;

    }
}
