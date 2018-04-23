using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTutorialController : MonoBehaviour {

	void Start () {
    move.SetActive(false);
    drag.SetActive(false);

    GameController.OnBallReady += OnBallReadyHandler;
    GameController.OnMinigolfTurn += OnMinigolfTurnHandler;
    GameController.OnBallShot += OnBallShotHandler;
  }

  void OnDestroy()
  {
    GameController.OnBallReady -= OnBallReadyHandler;
    GameController.OnMinigolfTurn -= OnMinigolfTurnHandler;
    GameController.OnBallShot -= OnBallShotHandler;
  }

  void OnBallReadyHandler()
  {
      move.SetActive(true);
  }

  void OnMinigolfTurnHandler()
  {
    move.SetActive(false);
    drag.SetActive(true);
  }

  void OnBallShotHandler()
  {
    drag.SetActive(false);
    Destroy(this);
  }

  [SerializeField]
  private GameObject move;

  [SerializeField]
  private GameObject click;

  [SerializeField]
  private GameObject drag;

}
