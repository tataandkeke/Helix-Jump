using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Goal : MonoBehaviour
{
    public PlayFabManager playFabManager;
    public GameObject LeaderBoard;

    private void OnCollisionEnter(Collision collision)
    {
        playFabManager.SendLeaderboard(GameManager.singleton.best);

        if (GameManager.singleton.currentStage == 2)
        {
            Time.timeScale = 0f;
            LeaderBoard.SetActive(true);
        }
        else
        {
            GameManager.singleton.NextLevel();
        }
        
    }
}
