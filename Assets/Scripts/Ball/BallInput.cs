using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInput : MonoBehaviour {

  [SerializeField]
  private float minCharge = 0;
  [SerializeField]
  private float maxCharge = 500;
  [SerializeField]
  private float chargeRate = 0.1f;
  [SerializeField]
  private ChargeShotUI chargeShotUi;

  bool preparedToPlayGolf;
  bool mouseDown;

  Vector3 finalDirection;

  bool chargingUpwards = true;
  float currentCharge;

  PlayerController playerController;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);

  public void StartGolfGame(PlayerController playerController)
  {
    if (playerController == null)
    {
      this.playerController = playerController;
    }
    Debug.LogError(playerController);
    GetComponent<BallMovement>().StopMovement();
    GameController.Instance.MinigolfTurn();
    preparedToPlayGolf = true;
  }

  // Update is called once per frame
  void Update () {
    if (!mouseDown && preparedToPlayGolf && Input.GetMouseButtonDown(0))
    {
      preparedToPlayGolf = false;
      mousePress();
      chargeShotUi.EnableCharge(true);
    }

    if (mouseDown)
    {
      Debug.Log(playerController);
      getDirection();
      chargeShot();

      if (Input.GetMouseButtonUp(0))
      {
        mouseRelease();
      }
    }

    if (transform.position.y < -1.5)
    {
      GameController.Instance.EndGame();
    }
	}

  private void mousePress()
  {
    mouseDown = true;
    currentCharge = 0;
  }

  private void mouseRelease() {
    GameController.Instance.BallShot();
    mouseDown = false;
    chargeShotUi.EnableCharge(false);
    StartCoroutine(waitToApplyForce());
    StartCoroutine(waitToReturnPlayerControl());
  }

  private void getDirection() {

    Debug.Log(playerController);

    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    float enter;
    if (plane.Raycast(ray, out enter))
    {
      var hitPoint = ray.GetPoint(enter);
      finalDirection = hitPoint - transform.position;
      finalDirection = Vector3.Normalize(finalDirection);
      finalDirection.y = 0;

      Debug.LogError(playerController);
      playerController.gameObject.transform.position = hitPoint;
    }
  }

  private IEnumerator waitToApplyForce()
  {
    yield return new WaitForSeconds(0.5f);
    GetComponent<BallMovement>().MoveBall(-finalDirection * currentCharge);
  }

  private IEnumerator waitToReturnPlayerControl()
  {
    yield return new WaitForSeconds(1f);
    GameController.Instance.PlayerMovementTurn();
  }

  private void chargeShot()
  {
    if (chargingUpwards)
    {
      currentCharge += (chargeRate + currentCharge/20);
      if (currentCharge >= maxCharge)
      {
        currentCharge = maxCharge;
        chargingUpwards = false;
      }
    }
    else
    {
      currentCharge -= (chargeRate + currentCharge/20);
      if (currentCharge <= minCharge)
      {
        currentCharge = minCharge;
        chargingUpwards = true;
      }
    }
    chargeShotUi.SetCharge(currentCharge, maxCharge);
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
