using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

  private Rigidbody ballRigidbody;

  private void Start()
  {
    ballRigidbody = GetComponent<Rigidbody>();
  }

  public void MoveBall(Vector3 forceDirection)
  {
    Debug.Log(forceDirection);
    ballRigidbody.AddForce(forceDirection);
  }

  public void StopMovement()
  {
    ballRigidbody.velocity        = Vector3.zero;
    ballRigidbody.angularVelocity = Vector3.zero;

  }

  private void Update()
  {
    if (ballRigidbody.velocity.y != 0)
    {
      Debug.Log("VELOCIDAD EN Y NO SÉ POR QUÉ!");
    }
  }
}
