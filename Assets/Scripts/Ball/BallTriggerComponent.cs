using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerComponent : MonoBehaviour {
  [SerializeField]
  private BallInput ballInput;

  private Collider trigger;

  private void OnEnabled()
  {
    GameController.OnMinigolfTurn += OnMinigolfTurnHandler;
    GameController.OnPlayerMovementTurn += OnPlayerMovementTurnHandler;
  }

  private void OnDisable()
  {
    GameController.OnMinigolfTurn -= OnMinigolfTurnHandler;
    GameController.OnPlayerMovementTurn -= OnPlayerMovementTurnHandler;
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

}
