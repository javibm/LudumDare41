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
  private ArrowSprite arrowSprite;

  private Vector3 lastPosition;

  bool preparedToPlayGolf;
  bool mouseDown;

  Vector3 finalDirection;

  private Vector3 finalHitPoint;
  float currentCharge;

  private PlayerController player;

  private Plane plane = new Plane(Vector3.up, Vector3.zero);

  void OnEnable()
  {
    GameController.OnResetBallPosition += OnResetLastPosition;
  }

  void OnDisable()
  {
    GameController.OnResetBallPosition -= OnResetLastPosition;
  }

  public void StartGolfGame(PlayerController playerController)
  {
    if (player == null)
    {
      player = playerController;
    }
    GetComponent<BallMovement>().StopMovement();
    GameController.Instance.MinigolfTurn();
    preparedToPlayGolf = true;
    lastPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (!mouseDown && preparedToPlayGolf && Input.GetMouseButtonDown(0))
    {
      arrowSprite.EnableArrow(true);
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

    if (transform.position.y < -5)
    {
      GameController.Instance.ResetBallPosition();
    }
    else if (transform.position.y < -1.5)
    {
      CameraController.Instance.FollowBall();
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
    StartCoroutine(waitToApplyForce());
    StartCoroutine(waitToReturnPlayerControl());
  }

  private void getDirection() {

    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    float enter;
    if (plane.Raycast(ray, out enter))
    {
      finalHitPoint = ray.GetPoint(enter);
      finalDirection = finalHitPoint - transform.position;
      finalDirection = Vector3.Normalize(finalDirection);
      finalDirection.y = 0;
      arrowSprite.RotateArrow(finalDirection);
    }
  }

  private void chargeShot()
  {
    currentCharge = (finalHitPoint - transform.position).magnitude;
    currentCharge = currentCharge * chargeRate;
    currentCharge = Mathf.Clamp(currentCharge, minCharge, maxCharge);
    arrowSprite.ScaleArrow(currentCharge, minCharge, maxCharge);
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

  private void OnDrawGizmos()
  {
    if (mouseDown)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(transform.position, -finalDirection);
    }
    Gizmos.DrawRay(transform.position, Vector3.down);
  }

  private void OnResetLastPosition()
  {
    transform.position = lastPosition;
    RaycastHit hit;
    if (!Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
    {
      transform.position = player.transform.position;
    }
    GetComponent<BallMovement>().StopMovement();
  }
}
