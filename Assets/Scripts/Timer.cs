using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float countdown;
    private int intText;
    // Start is called before the first frame update
    void Start()
    {
        countdown = 61.0f;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        intText = (int)countdown;
        timeText.text = "Time: " + intText;

        if (intText == 0)
        {
            Time.timeScale = 0f;
            FindObjectOfType<Goal>().LeaderBoard.SetActive(true);
        }
    }
}
