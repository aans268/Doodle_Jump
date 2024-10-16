using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{

    public Transform player;
    public Text scoreText;
    private float highestPoint;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        highestPoint = player.position.y;
        score =0;
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y >highestPoint)
        {
            highestPoint =player.position.y;
            score = Mathf.FloorToInt(highestPoint);
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text= "Score: "+score.ToString();
    }
}
