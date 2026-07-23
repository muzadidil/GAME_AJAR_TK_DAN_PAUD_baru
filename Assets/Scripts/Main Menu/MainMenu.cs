using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Balloon Pop Game
    public void StartEasyBalloonGame()
    {
        GameSettings.Instance.selectedGame = GameType.BalloonPop;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Easy;
        SceneManager.LoadScene("BalloonGame");
    }

    public void StartMediumBalloonGame()
    {
        GameSettings.Instance.selectedGame = GameType.BalloonPop;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Medium;
        SceneManager.LoadScene("BalloonGame");
    }

    public void StartHardBalloonGame()
    {
        GameSettings.Instance.selectedGame = GameType.BalloonPop;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Hard;
        SceneManager.LoadScene("BalloonGame");
    }

    // Matching Game
    // TODO: Change to MatchingGame scene
    public void StartMatchingEasy()
    {
        GameSettings.Instance.selectedGame = GameType.MatchingGame;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Easy;
        SceneManager.LoadScene("MatchingGame");
    }
    public void StartMatchingMedium()
    {
        GameSettings.Instance.selectedGame = GameType.MatchingGame;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Medium;
        SceneManager.LoadScene("MatchingGame");
    }
    public void StartMatchingHard()
    {
        GameSettings.Instance.selectedGame = GameType.MatchingGame;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Hard;
        SceneManager.LoadScene("MatchingGame");
    }

    // Count Objects Game
    // TODO: Change to CountObjects scene
    public void StartCountObjectsEasy()
    {
        GameSettings.Instance.selectedGame = GameType.CountObjects;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Easy;
        SceneManager.LoadScene("CountGame");
    }
    public void StartCountObjectsMedium()
    {
        GameSettings.Instance.selectedGame = GameType.CountObjects;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Medium;
        SceneManager.LoadScene("CountGame");
    }
    public void StartCountObjectsHard()
    {
        GameSettings.Instance.selectedGame = GameType.CountObjects;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Hard;
        SceneManager.LoadScene("CountGame");
    }

    // Shape Sorter Game
    // TODO: Change to ShapeSorter scene
    public void StartShapeSorterEasy()
    {
        GameSettings.Instance.selectedGame = GameType.ShapeSorter;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Easy;
        SceneManager.LoadScene("ShapeSorterGame");
    }
    public void StartShapeSorterMedium()
    {
        GameSettings.Instance.selectedGame = GameType.ShapeSorter;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Medium;
        SceneManager.LoadScene("ShapeSorterGame");
    }
    public void StartShapeSorterHard()
    {
        GameSettings.Instance.selectedGame = GameType.ShapeSorter;
        GameSettings.Instance.difficulty = BalloonGameManager.Difficulty.Hard;
        SceneManager.LoadScene("ShapeSorterGame");
    }


    //Quit Game
    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();

    }
}
