using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.UI;

public class MapTile : MonoBehaviour 
{
	public void Activate ()
	{
		gameObject.SetActive(true);
		_deactivateTweener1.ResetTweener();
		_deactivateTweener2.ResetTweener();
	}

	public void Deactivate ()
	{
		_deactivateTweener1.PlayTweener();
		_deactivateTweener2.PlayTweener();
	}

	[SerializeField]
	private Tweener _deactivateTweener1;
	[SerializeField]
	private Tweener _deactivateTweener2;
}
