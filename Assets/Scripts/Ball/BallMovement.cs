using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

  public void MoveBall(Vector3 forceDirection)
  {
    GetComponent<Rigidbody>().AddForce(forceDirection);
  }
}
