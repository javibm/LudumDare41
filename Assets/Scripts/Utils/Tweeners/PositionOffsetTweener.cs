using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.UI
{
  /// <summary>
  /// Position Tweener to animate UI transforms
  /// </summary>
  public class PositionOffsetTweener : TransformTweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/

    [SerializeField]
    private Vector3 finalOffset;

    private Vector3 initialPosition;
    private Vector3 finalPosition;


    /**********************************************************************************************/
    /*  Protected Methods                                                                         */
    /**********************************************************************************************/
    void Start ()
    {
      initialPosition = transform.localPosition;
      finalPosition = initialPosition + finalOffset;
    }

    protected override void UpdateTransform(float curveValue)
    {
      cachedTransform.localPosition = initialPosition + ((finalPosition - initialPosition) * curveValue);
    }
  }
}