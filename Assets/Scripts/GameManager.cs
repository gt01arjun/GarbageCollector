using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static bool GameOver;
    public float TimeRemaining;

    public static int CurrentTruckStorage;
    public static int MaxTruckStorage;
    public static int Score;

    public TMP_Text TimerText;
    public TMP_Text TruckCapacityText;
    public TMP_Text ScoreText;

    public GameObject PauseMenuScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private void Start()
    {
        Time.timeScale = 1f;
        CurrentTruckStorage = 0;
        MaxTruckStorage = 10;
        Score = 0;
        GameOver = false;
    }

    void Update()
    {
        if (GameOver == false)
        {
            ScoreText.text = "Score : " + Score;
            TruckCapacityText.text = "Capacity : " + CurrentTruckStorage + " / " + MaxTruckStorage;
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                DisplayTime(TimeRemaining);
            }
            else if (TimeRemaining < 0 && GarbageSpawner.NumberOfGarbages > 0)
            {
                GameOver = true;
                GameLose();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                PauseMenuScreen.SetActive(true);
            }

            if (GarbageSpawner.NumberOfGarbages <= 0 && TimeRemaining >= 0)
            {
                GameOver = true;
                GameWin();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimerText.text = "Time Remaining : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        PauseMenuScreen.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    private void GameLose()
    {
        LoseScreen.SetActive(true);
    }

    private void GameWin()
    {
        WinScreen.SetActive(true);
    }
}