using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {
  [SerializeField]
  private float velocityMagnitudeWin = 0.75f;

  private Rigidbody ballRigidbody;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Ball") && ballRigidbody == null)
    {
      ballRigidbody = other.GetComponent<Rigidbody>();
    }
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Ball"))
    {
      Debug.Log(ballRigidbody.velocity.magnitude);
      if (ballRigidbody.velocity.magnitude < velocityMagnitudeWin)
      {
        Debug.LogError("TERMINÓ");
        //GameController.Instance.EndGame();
      }
      else
      {
        Debug.Log("CASI!");
      }
    }
  }
}
