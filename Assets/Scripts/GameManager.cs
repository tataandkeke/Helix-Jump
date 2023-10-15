using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public int best;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    // Start is called before the first frame update
    void Awake()
    {
        //ADS not working
        //Advertisement.Initialize("5443555");

        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        //retrieves the highscore from playerprefs
        best = PlayerPrefs.GetInt("Highscore");
    }

    // Update is called once per frame
    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        // show ads, ADS arent working
        //Advertisement.Show("rewarded");
        

        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        //reload the stage 

    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score > best)
        {
            best = score;
            //store high store/ best in playerprefs
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
