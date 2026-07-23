using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public enum GameType
{
    BalloonPop,
    MatchingGame,
    CountObjects,
    ShapeSorter
}

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public BalloonGameManager.Difficulty difficulty = BalloonGameManager.Difficulty.Easy;
    public GameType selectedGame = GameType.BalloonPop;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
