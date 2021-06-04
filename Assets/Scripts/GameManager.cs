using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public float TimeRemaining = 10;

    public static int CurrentTruckStorage;
    public static int MaxTruckStorage;
    public static int Score;

    private void Start()
    {
        CurrentTruckStorage = 0;
        MaxTruckStorage = 15;
        Score = 0;
    }

    void Update()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            DisplayTime(TimeRemaining);

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}