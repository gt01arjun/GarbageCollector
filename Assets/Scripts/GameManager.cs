using UnityEngine;
using TMPro;

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

    private void Start()
    {
        CurrentTruckStorage = 0;
        MaxTruckStorage = 10;
        Score = 0;
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
            else
            {
                GameOver = true;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimerText.text = "Time Remaining : " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}