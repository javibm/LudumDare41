using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    protected new void Awake()
    {
        base.Awake();
    }

    public void LooseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
