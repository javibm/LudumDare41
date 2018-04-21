using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeShotUI : MonoBehaviour {
  [SerializeField]
  private GameObject background;
  [SerializeField]
  private GameObject fill;

  private Slider slider;

  private void Start()
  {
    slider = GetComponent<Slider>();
    EnableCharge(false);
  }

  public void EnableCharge(bool enabled)
  {
    background.gameObject.SetActive(enabled);
    fill.gameObject.SetActive(enabled);
    slider.value = 0;
  }

  public void SetCharge(float current, float total)
  {
    slider.value = current / total;
  }
}
