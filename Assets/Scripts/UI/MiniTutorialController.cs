using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTutorialController : MonoBehaviour {

	void Start () {
    move.SetActive(false);
    drag.SetActive(false);
    if (GameController._tutorialPlayed)
    {
      Destroy(this);
    }

    GameController.OnBallReady += OnBallReadyHandler;
  }

  void OnDestroy()
  {
    GameController.OnMinigolfTurn -= OnMinigolfTurnHandler;
    GameController.OnBallShot -= OnBallShotHandler;
    GameController.OnEndGame -= OnEndGameHandler;
    GameController.OnPlayerWin -= OnEndGameHandler;
  }

  void OnBallReadyHandler()
  {
    move.SetActive(true);
    GameController.OnMinigolfTurn += OnMinigolfTurnHandler;
    GameController.OnBallShot += OnBallShotHandler;
    GameController.OnEndGame += OnEndGameHandler;
    GameController.OnPlayerWin += OnEndGameHandler;

    GameController.OnBallReady -= OnBallReadyHandler;
  }

  void OnMinigolfTurnHandler()
  {
    move.SetActive(false);
    drag.SetActive(true);
  }

  void OnBallShotHandler()
  {
    drag.SetActive(false);
    move.SetActive(true);
  }

  void OnEndGameHandler()
  {
    drag.SetActive(false);
    move.SetActive(false);
  }

  [SerializeField]
  private GameObject move;

  [SerializeField]
  private GameObject drag;

}
