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
    canvas.transform.parent.eulerAngles = new Vector3(90, 0, 0);
  }

  public void RotateArrow(float rotationX)
  {
    canvas.transform.parent.eulerAngles = new Vector3(rotationX, 0, 0) ;
  }

}
