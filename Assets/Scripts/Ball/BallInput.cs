﻿using System.Collections;
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

  bool preparedToPlayGolf;
  bool mouseDown;

  Vector3 finalDirection;

  bool chargingUpwards = true;
  float currentCharge;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);
  private PlayerController playerController;

  public void StartGolfGame(PlayerController playerController)
  {
    GameController.Instance.MinigolfTurn();
    preparedToPlayGolf = true;
    Debug.Log("READY TO GOLF!");
  }

  // Update is called once per frame
  void Update () {
    if (!mouseDown && preparedToPlayGolf && Input.GetMouseButtonDown(0))
    {
      preparedToPlayGolf = false;
      mousePress();
    }

    if (mouseDown)
    {
      getDirection();
      chargeShot();

      if (Input.GetMouseButtonUp(0))
      {
        mouseRelease();
      }
    }
	}

  private void mousePress()
  {
    mouseDown = true;
    currentCharge = 0;
  }

  private void mouseRelease() {
    mouseDown = false;


    GetComponent<BallMovement>().MoveBall(-finalDirection * ballForce * currentCharge);
    GameController.Instance.PlayerMovementTurn();

    Debug.Log("GOLF FINISHED!");

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
