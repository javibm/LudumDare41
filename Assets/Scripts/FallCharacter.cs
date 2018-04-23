using System.Collections;
using UnityEngine;

public class FallCharacter : MonoBehaviour {
  private void OnEnable()
  {
    GameController.OnPlayerDead += OnPlayerDeadEventHandler;
  }

  private void OnDisable()
  {
    GameController.OnPlayerDead -= OnPlayerDeadEventHandler;
  }

  private void OnPlayerDeadEventHandler()
  {
    if (transform.position.y < -1)
    {
      StartCoroutine(AnimateDead());
    }
  }

  private IEnumerator AnimateDead()
  {
    Vector3 initialScale = transform.localScale;
    float totalTime = 2;
    float currentTime = 0;
    while (currentTime < totalTime)
    {
      transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, currentTime / totalTime);
      transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Mathf.Lerp(0, 720, currentTime / totalTime), transform.eulerAngles.z);
      currentTime += Time.deltaTime;
      yield return 0;
    }
    GameController.Instance.EndGame();
  }
}
