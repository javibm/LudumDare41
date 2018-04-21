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


  bool mouseDown;

  Vector3 initialMousePosition;
  Vector3 finalDirection;

  bool chargingUpwards = true;
  float currentCharge;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);

  // Use this for initialization
  void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    if (mouseDown)
    {
      getDirection();
    }
	}

  private void OnMouseDown() {
    currentCharge = 0;
    mouseDown = true;

    initialMousePosition = Input.mousePosition;
  }

  private void OnMouseUp() {
    mouseDown = false;

    GetComponent<BallMovement>().MoveBall(-finalDirection * 1000);
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
