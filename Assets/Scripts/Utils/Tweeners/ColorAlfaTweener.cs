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
  public class ColorAlfaTweener : Tweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/

    [SerializeField]
    private float initialAlpha;

    [SerializeField]
    private float finalAlpha;

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
      Color c = colorComponent.color;
      c.a = Mathf.Lerp(initialAlpha, finalAlpha, curveValue);
      colorComponent.color = c;
    }

  }
}