using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingCard : MonoBehaviour
{
    public string cardValue;
    public bool isMatched = false;

    [SerializeField] private TMP_Text letterText;
    [SerializeField] private Button button;
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;

    private MatchingGameManager gameManager;
    private bool isFaceUp = false;

    public void Setup(string value, MatchingGameManager manager)
    {
        cardValue = value;
        gameManager = manager;
        isMatched = false;
        isFaceUp = false;
        ShowBack();
    }

    public void OnCardClicked()
    {
        if (isMatched || isFaceUp)
            return;

        gameManager.SelectCard(this);
    }

    public void ShowFront()
    {
        isFaceUp = true;

        if (cardImage != null && frontSprite != null)
            cardImage.sprite = frontSprite;

        if (letterText != null)
            letterText.text = cardValue;
    }

    public void ShowBack()
    {
        isFaceUp = false;

        if (cardImage != null && backSprite != null)
            cardImage.sprite = backSprite;

        if (letterText != null)
            letterText.text = "";
    }

    public void SetMatched()
    {
        isMatched = true;
        isFaceUp = true;
    }
}