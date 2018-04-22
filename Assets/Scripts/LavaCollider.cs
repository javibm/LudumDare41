using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollider : MonoBehaviour {

  bool stopCollision = false;

  private void OnEnable()
  {
    GameController.OnPlayerDead += StopCollision;
  }

  private void OnDestroy()
  {
    GameController.OnPlayerDead -= StopCollision;
  }

  void OnCollisionEnter(Collision collision)
  {
    if (!stopCollision && collision.gameObject.CompareTag("Player"))
    {
      GameController.Instance.PlayerDead();
      StartCoroutine(AnimateCollider());
    }
  }

  private void StopCollision()
  {
    stopCollision = true;
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
}
