using System.Collections;
using TMPro;
using UnityEngine;

public class ShapeSorterGameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public ShapeDraggable fallingShapePrefab;

    [Header("Shape Sprites")]
    public Sprite circleSprite;
    public Sprite squareSprite;
    public Sprite triangleSprite;

    [Header("Scene References")]
    public RectTransform playArea;
    public Canvas canvas;
    public TMP_Text promptText;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    public ShapeBasket circleBasket;
    public ShapeBasket squareBasket;
    public ShapeBasket triangleBasket;

    [Header("Settings")]
    public float fallSpeed = 100f;
    public float spawnY = 500f;
    public float spawnXMin = -600f;
    public float spawnXMax = 600f;
    public float missY = -300f;

    private ShapeDraggable currentShape;
    private int score = 0;

    void Start()
    {
        ApplyDifficulty();

        if (promptText != null)
            promptText.text = "Drag the shape to the matching basket!";

        if (feedbackText != null)
            feedbackText.text = "";

        UpdateScoreUI();
        SpawnNextShape();
    }

    void Update()
    {
        if (currentShape != null)
        {
            RectTransform rt = currentShape.GetComponent<RectTransform>();
            rt.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

            if (rt.anchoredPosition.y < missY)
            {
                if (feedbackText != null)
                    feedbackText.text = "Try again!";

                Destroy(currentShape.gameObject);
                currentShape = null;
                StartCoroutine(SpawnAfterDelay());
            }
        }
    }

    void ApplyDifficulty()
    {
        if (GameSettings.Instance == null)
            return;

        switch (GameSettings.Instance.difficulty)
        {
            case BalloonGameManager.Difficulty.Easy:
                fallSpeed = 80f;
                break;

            case BalloonGameManager.Difficulty.Medium:
                fallSpeed = 150f;
                break;

            case BalloonGameManager.Difficulty.Hard:
                fallSpeed = 200f;
                break;
        }
    }

    void SpawnNextShape()
    {
        if (currentShape != null)
            return;

        int randomIndex = Random.Range(0, 3);

        string chosenShape = "";
        Sprite chosenSprite = null;

        switch (randomIndex)
        {
            case 0:
                chosenShape = "Circle";
                chosenSprite = circleSprite;
                break;

            case 1:
                chosenShape = "Square";
                chosenSprite = squareSprite;
                break;

            case 2:
                chosenShape = "Triangle";
                chosenSprite = triangleSprite;
                break;
        }

        currentShape = Instantiate(fallingShapePrefab, playArea);

        RectTransform rt = currentShape.GetComponent<RectTransform>();
        float randomX = Random.Range(spawnXMin, spawnXMax);
        rt.anchoredPosition = new Vector2(randomX, spawnY);

        currentShape.Setup(chosenShape, chosenSprite, this, canvas);
        currentShape.RecordStartPosition();
    }

    public void HandleShapeReleased(ShapeDraggable shape)
    {
        if (shape == null)
            return;

        ShapeBasket matchedBasket = GetTouchedBasket(shape);

        if (matchedBasket != null && matchedBasket.basketType == shape.shapeType)
        {
            score++;
            UpdateScoreUI();

            if (feedbackText != null)
                feedbackText.text = "Good job!";

            Destroy(shape.gameObject);
            currentShape = null;
            StartCoroutine(SpawnAfterDelay());
        }
        else
        {
            if (feedbackText != null)
                feedbackText.text = "Try again!";

            shape.ResetPosition();
        }
    }

    ShapeBasket GetTouchedBasket(ShapeDraggable shape)
    {
        RectTransform shapeRect = shape.GetComponent<RectTransform>();

        if (RectOverlaps(shapeRect, circleBasket.GetComponent<RectTransform>()))
            return circleBasket;

        if (RectOverlaps(shapeRect, squareBasket.GetComponent<RectTransform>()))
            return squareBasket;

        if (RectOverlaps(shapeRect, triangleBasket.GetComponent<RectTransform>()))
            return triangleBasket;

        return null;
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Vector3[] aCorners = new Vector3[4];
        Vector3[] bCorners = new Vector3[4];

        a.GetWorldCorners(aCorners);
        b.GetWorldCorners(bCorners);

        Rect rectA = new Rect(aCorners[0], aCorners[2] - aCorners[0]);
        Rect rectB = new Rect(bCorners[0], bCorners[2] - bCorners[0]);

        return rectA.Overlaps(rectB);
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SpawnNextShape();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}