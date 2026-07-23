using System.Collections.Generic;
using TMPro; // Make sure you have TextMeshPro installed!
using UnityEngine;

public class BalloonGameManager : MonoBehaviour
{
    public GameObject[] balloonPrefabs;           // Prefabs assigned in Inspector
    public TextMeshProUGUI promptText;            // Drag TextMeshPro UI here
    public TextMeshProUGUI feedbackText;          // Drag TextMeshPro UI here
    public int balloonCount = 10;                  // How many balloons to spawn per round

    public TextMeshProUGUI scoreText;

    private int score = 0; 

    private string targetColor;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    private bool roundOver = false;

    [Header("Difficulty")]
    public Difficulty difficulty = Difficulty.Easy;
    public int easyCount = 10; 
    public int mediumCount = 15;
    public int hardCount = 20;
    public float hardSpeed = 0.9f; // how fast balloons drift in Hard

    private int GetBalloonCount()
    {
        switch (difficulty)
        {
            case Difficulty.Medium: return mediumCount;
            case Difficulty.Hard: return hardCount;
            case Difficulty.Easy: return easyCount;
            default: return easyCount;
        }
    }

    void Start()
    {
        if (GameSettings.Instance != null)
        {
            difficulty = GameSettings.Instance.difficulty;
        }

        Debug.Log($"[GM] Difficulty: {difficulty} (E/M/H = {easyCount}/{mediumCount}/{hardCount})"); 

        score = 0;
        UpdateScoreUI();
        SpawnChallenge();
    }

    public void SpawnChallenge()
    {
        ClearOldBalloons();
        roundOver = false;

        // Select a random target balloon prefab

        GameObject targetPrefab = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];
        targetColor = targetPrefab.GetComponent<Balloon>().balloonColor;

        // Update prompt text
        promptText.text = $"Pop the {targetColor} balloon!";
        feedbackText.text = "";

        bool correctSpawned = false;
        int count = GetBalloonCount();

        for (int i = 0; i < count; i++)
        {
            GameObject prefabToSpawn;

            // Ensure one correct balloon is spawned
            if (!correctSpawned && i == balloonCount - 1)
            {
                prefabToSpawn = targetPrefab;
            }
            else
            {
                prefabToSpawn = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];
                if (prefabToSpawn.GetComponent<Balloon>().balloonColor == targetColor)
                {
                    correctSpawned = true;
                }
            }

            Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(
                Random.Range(0.1f, 0.9f), 
                Random.Range(0.2f, 0.8f)
            ));

            GameObject balloon = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            balloon.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(0, 100);

            // HARD: enable slow bouncing
            if (difficulty == Difficulty.Hard)
            {
                var mover = balloon.GetComponent<BalloonBouncer>();
                if (mover == null) mover = balloon.AddComponent<BalloonBouncer>();
                mover.speed = hardSpeed;
            }


        }
    }

    public void CheckBalloon(Balloon clickedBalloon)
    {
        if (roundOver) return; // Prevent multiple checks if round is already over

        if (clickedBalloon.balloonColor == targetColor)
        {
            score += 1;
            UpdateScoreUI(); 

            feedbackText.text = "Good job!";
            Destroy(clickedBalloon.gameObject);

            roundOver = true; // Prevent further input until next round
            Invoke(nameof(SpawnChallenge), 1.5f);
        }
        else
        {
            feedbackText.text = "Try again!";
            Destroy(clickedBalloon.gameObject); // optional: let them retry instead
        }
    }

    void ClearOldBalloons()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Balloon"))
        {
            Destroy(b);
        }
        
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        else
            Debug.LogWarning("ScoreText not assigned in the Inspector!"); 
    }
}
