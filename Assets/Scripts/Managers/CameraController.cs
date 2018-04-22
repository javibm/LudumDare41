using UnityEngine;
using System.Collections;

public class CameraController : Singleton<CameraController>
{
  void Start()
  {
    GameController.OnPlayerMovementTurn += ZoomIn;
    GameController.OnMinigolfTurn += ZoomOut;
    GameController.OnResetBallPosition += ResetPosition;

    camera = GetComponent<Camera>();
    player = GameObject.FindGameObjectWithTag("Player");
    ball = GameObject.FindGameObjectWithTag("Ball");

    cameraTarget = player;
    

    camera.orthographicSize = minZoom;

    zoomOffset = normalZoom - minZoom;
  }

  void Update()
  {
    finalPosition = cameraTarget.transform.position + new Vector3(6.0f, 6.0f, 6.0f);
    transform.position = finalPosition;
  }

  void ResetPosition()
  {
    FollowCharacter();
    ZoomOut();
  }

  public void FollowCharacter()
  {
    cameraTarget = player;
  }

  public void FollowBall()
  {
    cameraTarget = ball;
    ZoomIn();
  }

  private void ZoomIn()
  {
    StopAllCoroutines();
    if (camera.orthographicSize != minZoom)
    {
      StartCoroutine(ZoomInCamera(zoomOffset));
    }
  }

  private void ZoomOut()
  {
    StopAllCoroutines();
    if (camera.orthographicSize != normalZoom)
    {
      StartCoroutine(ZoomOutCamera(zoomOffset));
    }
  }

  private IEnumerator ZoomInCamera(float zoom)
  {
    float time = 0.0f;
    float cameraZoom = camera.orthographicSize;

    while (time < timeTransition)
    {
      camera.orthographicSize = cameraZoom - zoom * zoomCurve.Evaluate(time / timeTransition);
      time += Time.deltaTime;
      yield return null;
    }
    camera.orthographicSize = minZoom;
  }

  private IEnumerator ZoomOutCamera(float zoom)
  {
    float time = 0.0f;
    float cameraZoom = camera.orthographicSize;

    while (time < timeTransition)
    {
      camera.orthographicSize = cameraZoom + zoom * zoomCurve.Evaluate(time / timeTransition);
      time += Time.deltaTime;
      yield return null;
    }
    camera.orthographicSize = normalZoom;
  }

  void OnDestroy()
  {
    GameController.OnPlayerMovementTurn -= ZoomIn;
    GameController.OnMinigolfTurn -= ZoomOut;
  }

  [SerializeField]
  private AnimationCurve zoomCurve;
  [SerializeField]
  private float minZoom;
  [SerializeField]
  private float normalZoom;
  [SerializeField]
  private float timeTransition;

  private GameObject player;
  private GameObject ball;
  private GameObject cameraTarget;

  private Vector3 finalPosition;

  private new Camera camera;

  private float zoomOffset;

}