using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSprite : MonoBehaviour {

  [SerializeField]
  private float minScale;
  [SerializeField]
  private float maxScale;
  [SerializeField]
  private Image arrow;

  Canvas canvas;

	// Use this for initialization
	void Start () {
    canvas = GetComponent<Canvas>();
    EnableArrow(false);
	}

  private void OnEnable()
  {
    GameController.OnBallShot += OnBallShotHandler;
  }

  private void OnDisable()
  {
    GameController.OnBallShot -= OnBallShotHandler;
  }

  private void OnMinigolfTurnHandler()
  {
    EnableArrow(true);
  }

  private void OnBallShotHandler()
  {
    EnableArrow(false);
  }

  public void EnableArrow(bool enabled)
  {
    canvas.enabled = enabled;
    canvas.transform.parent.eulerAngles = Vector3.zero;
  }

  public void RotateArrow(Vector3 rotation)
  {
    canvas.transform.parent.rotation = Quaternion.LookRotation(rotation);
    canvas.transform.parent.eulerAngles = new Vector3(0, canvas.transform.parent.eulerAngles.y + 90, 0);
  }

  public void ScaleArrow(float current, float min, float max)
  {
    float scale = Mathf.Lerp(minScale, maxScale, current / max);
    arrow.rectTransform.localScale = new Vector3(scale, scale, scale);
  }

}
