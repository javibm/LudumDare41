using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

  private Rigidbody ballRigidbody;

  private void Awake()
  {
    ballRigidbody = GetComponent<Rigidbody>();
  }

  public void MoveBall(Vector3 forceDirection)
  {
    ballRigidbody.AddForce(forceDirection);
  }

  public void StopMovement()
  {
    ballRigidbody.velocity        = Vector3.zero;
    ballRigidbody.angularVelocity = Vector3.zero;

  }
}
