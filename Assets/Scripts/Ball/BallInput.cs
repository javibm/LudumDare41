using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInput : MonoBehaviour {

  [SerializeField]
  private float minCharge = 0;
  [SerializeField]
  private float maxCharge = 1;
  [SerializeField]
  private float chargeRate = 0.1f;

  [SerializeField]
  private float ballForce = 500;


  bool mouseDown;

  Vector3 finalDirection;

  bool chargingUpwards = true;
  float currentCharge;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.LogError("PLAYER AQUÍ");
      currentCharge = 0;
      mouseDown = true;
    }
  }

  // Update is called once per frame
  void Update () {
    if (mouseDown)
    {
      getDirection();
      chargeShot();
    }
	}

  private void OnMouseUp() {
    mouseDown = false;

    GetComponent<BallMovement>().MoveBall(-finalDirection * ballForce);
  }

  private void getDirection() {
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    float enter;
    if (plane.Raycast(ray, out enter))
    {
      var hitPoint = ray.GetPoint(enter);
      finalDirection = hitPoint - transform.position;
      finalDirection = Vector3.Normalize(finalDirection);
      finalDirection.y = 0;
    }

  }

  private void chargeShot()
  {
    if (chargingUpwards)
    {
      currentCharge += chargeRate;
      if (currentCharge >= maxCharge)
      {
        currentCharge = maxCharge;
        chargingUpwards = false;
      }
    }
    else
    {
      currentCharge -= chargeRate;
      if (currentCharge <= minCharge)
      {
        currentCharge = minCharge;
        chargingUpwards = true;
      }
    }
  }

  private void OnDrawGizmos()
  {
    if (mouseDown)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(transform.position, -finalDirection);
    }
  }
}
