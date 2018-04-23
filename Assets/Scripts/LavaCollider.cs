using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollider : MonoBehaviour {

  bool stopCollision = false;

  void OnCollisionEnter(Collision collision)
  {
    if (!stopCollision && collision.gameObject.CompareTag("Ball"))
    {
      StopCollision(true);
      StartCoroutine(AnimateBall());
      CameraController.Instance.FollowBall();
      GameController.Instance.BallLava();
    }
    else if (!stopCollision && collision.gameObject.CompareTag("Player"))
    {
      StopCollision(true);
      GameController.Instance.CharacterLava();
      GameController.Instance.PlayerDead();
      StartCoroutine(AnimateCollider());
    }
  }

  private void StopCollision(bool stop)
  {
    stopCollision = stop;
  }

  private IEnumerator AnimateCollider()
  {
    yield return new WaitForSeconds(0.5f);

    float totalTime = 2;
    float currentTime = 0;
    Vector3 initialPosition = transform.position;

    while (currentTime < totalTime)
    {
      transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.down, currentTime / totalTime);
      currentTime += Time.deltaTime;
      yield return 0;
    }
    GameController.Instance.EndGame();
  }

  private IEnumerator AnimateBall()
  {
    yield return new WaitForSeconds(0.5f);

    float totalTime = 1;
    float currentTime = 0;
    Vector3 initialPosition = transform.position;

    while (currentTime < totalTime)
    {
      transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.down, currentTime / totalTime);
      currentTime += Time.deltaTime;
      yield return 0;
    }
    GameController.Instance.ResetBallPosition();
    StopCollision(false);

    transform.position = initialPosition;
  }
}
