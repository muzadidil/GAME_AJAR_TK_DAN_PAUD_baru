using UnityEngine;
using UnityEngine.InputSystem;

public class Balloon : MonoBehaviour
{
    public string balloonColor;

    private Camera mainCamera;
    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Click.performed += OnClick;
    }

    private void OnDisable()
    {
        controls.Player.Click.performed -= OnClick;
        controls.Player.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 screenPos;

        // Determine if using mouse or touchscreen
        if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
        }
        else if (Touchscreen.current != null)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            return;
        }

        Vector2 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        if (hit != null && hit.gameObject == gameObject)
        {
            Debug.Log($"Balloon Clicked: {balloonColor}!");

            FindObjectOfType<BalloonGameManager>().CheckBalloon(this);
        }
    }
}
