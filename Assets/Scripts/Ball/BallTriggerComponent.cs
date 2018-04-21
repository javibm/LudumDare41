using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerComponent : MonoBehaviour {
  [SerializeField]
  private BallInput ballInput;
  
  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      ballInput.StartGolfGame(other.GetComponent<PlayerController>());
    }
  }
}
