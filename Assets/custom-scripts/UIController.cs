using UnityEngine.UI;  // required for text
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private player playerScript;

  public GameObject UIRacePanel;
    public GameObject LapSelectPanel;
    public GameObject MainMenuPanel;

  public Text UITextCurrentLap;
  public Text UITextCurrentTime;
  public Text UITextLastLapTime;
  public Text UITextBestLapTime;

  private int currentLap = -1;
  private float currentLapTime;
  private float lastLapTime;
  private float bestLapTime;



    void Start()
    {
       playerScript = GetComponent<player>();
    }


    void Update()
    {
        if (playerScript != null)
        {
            if (playerScript.CurrentLap != currentLap)
            {
                currentLap = playerScript.CurrentLap;
                UITextCurrentLap.text = $"LAP: {currentLap}";
            }

            // update the times
            if (playerScript.CurrentLapTime != currentLapTime)
            {
                currentLapTime = playerScript.CurrentLapTime;
                UITextCurrentTime.text = $"TIME: {(int)currentLapTime / 60}:{(currentLapTime) % 60:00.00}";
            }
            if (playerScript.LastLapTime != lastLapTime)
            {
                lastLapTime = playerScript.LastLapTime;
                UITextLastLapTime.text = $"LAST: {(int)lastLapTime / 60}:{(lastLapTime) % 60:00.00}";
            }
            if (playerScript.BestLapTime != bestLapTime)
            {
                bestLapTime = playerScript.BestLapTime;
                UITextBestLapTime.text = bestLapTime < 1000000 ? $"BEST: {(int)bestLapTime / 60}:{(bestLapTime) % 60:00.00}" : "BEST: ---";
            }
        }
    }


public void GoToLapChoice()
    {
        MainMenuPanel.SetActive(false);
        LapSelectPanel.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Lap1()
    {
        GameManager.instance.SetLapNumber(1);
        StartGame();
    }

    public void Lap3()
    {
        GameManager.instance.SetLapNumber(3);
        StartGame();
    }

    public void Lap5()
    {
        GameManager.instance.SetLapNumber(5);
        StartGame();
    }

    public void Lap10()
    {
        GameManager.instance.SetLapNumber(10);
        StartGame();
    }

}
