using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    public int score;
    public int score2;
    public int highScore;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI scorePauseUI;
    public TextMeshProUGUI score2UI;
    public TextMeshProUGUI highScoreUI;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
      /*  PlayerPrefs.DeleteAll(); */       // use to reset scores
        highScore = PlayerPrefs.GetInt("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = score.ToString();
        scorePauseUI.text = score.ToString(); 
        score2UI.text = score2.ToString();
        highScoreUI.text = highScore.ToString();
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);    
        }

    }
    void OnTriggerEnter(Collider other)
    {
         
        if(other.gameObject.tag == "scoreup")
        {
            if (AppInitialisation.instance.hasGameOver == true)  // do not increse the scote
            {
                return;
            }
            score++;
            score2 = score;
        }
    }

}
