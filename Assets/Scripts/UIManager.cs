using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBest;
    public TextMeshProUGUI leaderText;

    // Update is called once per frame
    void Update()
    {
        textBest.text = "Best: " + GameManager.singleton.best;
        leaderText.text = "Score: " + GameManager.singleton.best;
        textScore.text = "Score: " + GameManager.singleton.score;
    }
}
