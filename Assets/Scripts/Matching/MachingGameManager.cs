using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchingGameManager : MonoBehaviour
{
    [Header("Card Setup")]
    public MatchingCard cardPrefab;
    public Transform cardBoard;

    [Header("UI")]
    public TMP_Text promptText;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    [Header("Layout")]
    public int columns = 4;
    public float spacingX = 120f;
    public float spacingY = 140f;

    private List<MatchingCard> cards = new List<MatchingCard>();
    private MatchingCard firstCard;
    private MatchingCard secondCard;
    private bool canSelect = true;
    private int score = 0;

    void Start()
    {
        SetupDifficulty();
        CreateCards();
        UpdateScoreUI();

        if (promptText != null)
            promptText.text = "Cocokkan huruf yang sama!";

        if (feedbackText != null)
            feedbackText.text = "";
    }

    void SetupDifficulty()
    {
        if (GameSettings.Instance == null)
            return;

        switch (GameSettings.Instance.difficulty)
        {
            case BalloonGameManager.Difficulty.Easy:
                columns = 4;
                break;

            case BalloonGameManager.Difficulty.Medium:
                columns = 4;
                break;

            case BalloonGameManager.Difficulty.Hard:
                columns = 4;
                break;
        }
    }

    void CreateCards()
    {
        List<string> values = GetLetterPairsForDifficulty();

        for (int i = 0; i < values.Count; i++)
        {
            MatchingCard newCard = Instantiate(cardPrefab, cardBoard);
            newCard.Setup(values[i], this);
            cards.Add(newCard);
        }

        PositionCards();
    }

    List<string> GetLetterPairsForDifficulty()
    {
        List<string> cardValues = new List<string>();

        List<char> allLetters = new List<char>()
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
            'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
            'Y', 'Z'
        };

        int pairCount = 2; 

        if (GameSettings.Instance != null)
        {
            switch (GameSettings.Instance.difficulty)
            {
                case BalloonGameManager.Difficulty.Easy:
                    pairCount = 4;
                    break;
                case BalloonGameManager.Difficulty.Medium:
                    pairCount = 6;
                    break;
                case BalloonGameManager.Difficulty.Hard:
                    pairCount = 8;
                    break;
            }
        }

        ShuffleLetters(allLetters);

        for (int i = 0; i < pairCount; i++)
        {
            string upper = allLetters[i].ToString().ToUpper();
            string lower = allLetters[i].ToString().ToLower();

            cardValues.Add(upper);
            cardValues.Add(lower);
        }

        Shuffle(cardValues);

        return cardValues;
    }

    void PositionCards()
    {
        int totalCards = cards.Count;
        int rows = Mathf.CeilToInt((float)totalCards / columns);

        float totalWidth = (columns - 1) * spacingX;
        float totalHeight = (rows - 1) * spacingY;

        for (int i = 0; i < cards.Count; i++)
        {
            int row = i / columns;
            int col = i % columns;

            RectTransform rt = cards[i].GetComponent<RectTransform>();

            float x = col * spacingX - totalWidth / 2f;
            float y = -row * spacingY + totalHeight / 2f;

            rt.anchoredPosition = new Vector2(x, y);
        }
    }

    public void SelectCard(MatchingCard selected)
    {
        if (!canSelect)
            return;

        selected.ShowFront();

        if (firstCard == null)
        {
            firstCard = selected;
            return;
        }

        if (secondCard == null)
        {
            secondCard = selected;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        canSelect = false;
        yield return new WaitForSeconds(0.8f);

        if (IsMatchingPair(firstCard.cardValue, secondCard.cardValue))
        {
            firstCard.SetMatched();
            secondCard.SetMatched();
            score++;

            if (feedbackText != null)
                feedbackText.text = "Bagus sekali!";
        }
        else
        {
            firstCard.ShowBack();
            secondCard.ShowBack();

            if (feedbackText != null)
                feedbackText.text = "Coba lagi!";
        }

        firstCard = null;
        secondCard = null;
        canSelect = true;

        UpdateScoreUI();
    }

    bool IsMatchingPair(string a, string b)
    {
        return a.ToLower() == b.ToLower() && a != b;
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Skor: " + score;
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void ShuffleLetters(List<char> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            char temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}