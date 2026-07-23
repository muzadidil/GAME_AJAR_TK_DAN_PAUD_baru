using TMPro;
using UnityEngine;

public class ShapeBasket : MonoBehaviour
{
    public string basketType;

    [SerializeField] private TMP_Text basketLabel;

    public void Setup(string type)
    {
        basketType = type;

        if (basketLabel != null)
            basketLabel.text = type;
    }
}
