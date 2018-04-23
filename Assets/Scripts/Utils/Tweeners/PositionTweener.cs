using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.UI
{
  /// <summary>
  /// Position Tweener to animate UI transforms
  /// </summary>
  public class PositionTweener : TransformTweener
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
      cachedTransform.localPosition = initialPosition + ((finalPosition - initialPosition) * curveValue);
    }
  }
}