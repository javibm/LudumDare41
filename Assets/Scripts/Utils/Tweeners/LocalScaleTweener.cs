using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.UI
{
  /// <summary>
  /// Scale Tweener
  /// </summary>
  public class LocalScaleTweener : TransformTweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/

    [SerializeField]
    private Vector3 initialLocalScale;

    [SerializeField]
    private Vector3 finalLocalScale;

    /**********************************************************************************************/
    /*  Protected Methods                                                                         */
    /**********************************************************************************************/

    protected override void UpdateTransform(float curveValue)
    {
      cachedTransform.localScale = initialLocalScale + ((finalLocalScale - initialLocalScale) * curveValue);
    }
  }
}