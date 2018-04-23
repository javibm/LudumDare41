using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerComponent : MonoBehaviour {
  [SerializeField]
  private BallInput ballInput;

  private Collider trigger;

  private void Awake()
  {
    trigger = GetComponent<Collider>();
    trigger.enabled = false;
  }

  public void InitTrigger()
  {
    trigger.enabled = true;
  }

  private void OnEnable()
  {
    GameController.OnBallReady += InitTrigger;
    GameController.OnMinigolfTurn += OnMinigolfTurnHandler;
    GameController.OnPlayerMovementTurn += OnPlayerMovementTurnHandler;
    GameController.OnPlayerWin += DeleteTrigger;
  }

  private void OnDisable()
  {
    GameController.OnBallReady -= InitTrigger;
    GameController.OnMinigolfTurn -= OnMinigolfTurnHandler;
    GameController.OnPlayerMovementTurn -= OnPlayerMovementTurnHandler;
    GameController.OnPlayerWin -= DeleteTrigger;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      ballInput.StartGolfGame(other.GetComponent<PlayerController>());
    }
  }

  private void OnMinigolfTurnHandler()
  {
    trigger.enabled = false;
  }

  private void OnPlayerMovementTurnHandler()
  {
    trigger.enabled = true;
  }

  private void DeleteTrigger()
  {
    trigger.enabled = false;
  }
}
