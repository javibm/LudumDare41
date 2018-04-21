using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.UI
{
  /// <summary>
  /// Color Tweener to animate color elements
  /// </summary>
  public class ColorTweener : Tweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/

    [SerializeField]
    private Color initialColor;

    [SerializeField]
    private Color finalColor;

    private MaskableGraphic colorComponent;


    /**********************************************************************************************/
    /*  MonoBehaviour Methods                                                                     */
    /**********************************************************************************************/

    void Awake()
    {
      colorComponent = GetComponent<MaskableGraphic>();
    }


    /**********************************************************************************************/
    /*  Protected Methods                                                                         */
    /**********************************************************************************************/

    protected override void UpdateTransform(float curveValue)
    {
      colorComponent.color = Color.Lerp(initialColor, finalColor, curveValue);
    }

  }
}