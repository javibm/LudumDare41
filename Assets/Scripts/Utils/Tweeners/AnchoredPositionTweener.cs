using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.UI
{
  /// <summary>
  /// Position Tweener to animate UI transforms
  /// </summary>
  public class AnchoredPositionTweener : RectTransformTweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/

    [SerializeField]
    private Vector3 initialPosition;

    [SerializeField]
    private Vector3 finalPosition;

    /**********************************************************************************************/
    /*  Protected Methods                                                                         */
    /**********************************************************************************************/

    protected override void UpdateTransform(float curveValue)
    {
      cachedRectTransform.anchoredPosition = initialPosition + ((finalPosition - initialPosition) * curveValue);
    }

  }
}