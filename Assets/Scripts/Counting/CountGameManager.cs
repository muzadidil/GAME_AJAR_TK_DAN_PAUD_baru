using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountGameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject countObjectPrefab;
    public CountNumberButton numberButtonPrefab;

    [Header("Parents")]
    public Transform objectBoard;
    public Transform buttonBoard;

    [Header("UI")]
    public TMP_Text promptText;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    [Header("Layout")]
    public float objectSpacingX = 100f;
    public float objectSpacingY = 100f;
    public float buttonSpacingX = 110f;

    private int correctAnswer;
    private int score = 0;

    private readonly List<GameObject> spawnedObjects = new(); 
    private readonly List<CountNumberButton> spawnedButtons = new();

    void Start()
    {
        UpdateScoreUI();

        if (promptText != null)
            promptText.text = "How many objects are there?";

        if (feedbackText != null)
            feedbackText.text = "";

        StartRound();
    }

    void StartRound()
    {
        ClearRound();

        correctAnswer = GetRandomAnswerByDifficulty();

        SpawnObjects(correctAnswer);
        SpawnAnswerButtons();
    }

    int GetRandomAnswerByDifficulty()
    {
        int min = 1;
        int max = 4;

        if (GameSettings.Instance != null)
        {
            switch (GameSettings.Instance.difficulty)
            {
                case BalloonGameManager.Difficulty.Easy:
                    min = 1; max = 5;
                    break;
                case BalloonGameManager.Difficulty.Medium:
                    min = 1; max = 10;
                    break;
                case BalloonGameManager.Difficulty.Hard:
                    min = 1; max = 20;
                    break;
            }
        }

        return Random.Range(min, max + 1);
    }

    void SpawnObjects(int count)
    {
        int columns = 5;
        int rows = Mathf.CeilToInt((float)count / columns);

        float totalWidth = (columns - 1) * objectSpacingX;
        float totalHeight = (rows - 1) * objectSpacingY;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(countObjectPrefab, objectBoard);
            spawnedObjects.Add(obj);

            RectTransform rt = obj.GetComponent<RectTransform>();

            int row = i / columns;
            int col = i % columns;

            float x = col * objectSpacingX - totalWidth / 2f;
            float y = -row * objectSpacingY + totalHeight / 2f;

            rt.anchoredPosition = new Vector2(x, y);
        }
    }

    void SpawnAnswerButtons()
    {
        List<int> answerChoices = GenerateAnswerChoices(correctAnswer);

        float totalWidth = (4 - 1) * buttonSpacingX;

        for (int i = 0; i < answerChoices.Count; i++)
        {
            CountNumberButton btn = Instantiate(numberButtonPrefab, buttonBoard);
            spawnedButtons.Add(btn);

            btn.Setup(answerChoices[i], this);

            RectTransform rt = btn.GetComponent<RectTransform>();
            float x = i * buttonSpacingX - totalWidth / 2f;

            rt.anchoredPosition = new Vector2(x, 0);
        }
    }

    public void CheckAnswer(int chosenNumber)
    {
        if (chosenNumber == correctAnswer)
        {
            score++;

            if (feedbackText != null)
                feedbackText.text = "Good job!";

            UpdateScoreUI();

            CancelInvoke(nameof(StartRound));
            Invoke(nameof(StartRound), 1f);
        }
        else
        {
            if (feedbackText != null)
                feedbackText.text = "Try again!";

            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void ClearRound()
    {
        foreach (GameObject obj in spawnedObjects)
            Destroy(obj);
        spawnedObjects.Clear();

        foreach (CountNumberButton btn in spawnedButtons)
            Destroy(btn.gameObject);
        spawnedButtons.Clear();
    }

    List<int> GenerateAnswerChoices(int correct)
    {
        List<int> choices = new List<int>();
        choices.Add(correct);

        int maxRange = 5;

        if (GameSettings.Instance != null)
        {
            switch (GameSettings.Instance.difficulty)
            {
                case BalloonGameManager.Difficulty.Easy:
                    maxRange = 5;
                    break;

                case BalloonGameManager.Difficulty.Medium:
                    maxRange = 10;
                    break;

                case BalloonGameManager.Difficulty.Hard:
                    maxRange = 20;
                    break;
            }
        }

        while (choices.Count < 4)
        {
            int wrongAnswer = Random.Range(1, maxRange + 1);

            if (!choices.Contains(wrongAnswer))
                choices.Add(wrongAnswer);
        }

        ShuffleIntList(choices);
        return choices;
    }

    void ShuffleIntList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
