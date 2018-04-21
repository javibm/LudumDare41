using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public static event Action OnStartGame = delegate { };
    public static event Action OnPlayerMovementTurn = delegate { };
    public static event Action OnMinigolfTurn = delegate { };
    public static event Action OnEndGame = delegate { };

    protected new void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        OnStartGame();
    }

    public void PlayerMovementTurn()
    {
        OnPlayerMovementTurn();
    }

    public void MinigolfTurn()
    {
        OnMinigolfTurn();
    }

    public void EndGame()
    {
        OnEndGame();
        ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
