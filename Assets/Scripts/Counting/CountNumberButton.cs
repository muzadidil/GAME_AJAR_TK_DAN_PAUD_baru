using TMPro;
using UnityEngine;

public class CountNumberButton : MonoBehaviour
{
    public int buttonValue;

    [SerializeField] private TMP_Text buttonText;

    private CountGameManager gameManager;

    public void Setup(int value, CountGameManager manager)
    {
        buttonValue = value;
        gameManager = manager;

        if (buttonText != null)
            buttonText.text = buttonValue.ToString();
    }

    public void OnButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.CheckAnswer(buttonValue);
        }
    }
}