using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSprite : MonoBehaviour {

  Canvas canvas;

	// Use this for initialization
	void Start () {
    canvas = GetComponent<Canvas>();
    EnableArrow(false);
	}

  private void OnEnable()
  {
    GameController.OnMinigolfTurn += OnMinigolfTurnHandler;
    GameController.OnBallShot     += OnBallShotHandler;
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
  }

  public void RotateArrow(Vector3 rotation)
  {
    canvas.transform.parent.rotation = Quaternion.LookRotation(rotation);
    canvas.transform.parent.eulerAngles = new Vector3(0, canvas.transform.parent.eulerAngles.y + 90, 0);
  }

}
