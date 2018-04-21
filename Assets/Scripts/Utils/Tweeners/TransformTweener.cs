using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.UI
{
  /// <summary>
  /// Transform Tweener for Rect Transform animations
  /// </summary>
  public abstract class TransformTweener : Tweener
  {

    /**********************************************************************************************/
    /*  Members                                                                                   */
    /**********************************************************************************************/
    protected Transform cachedTransform;

    /**********************************************************************************************/
    /*  MonoBehaviour Methods                                                                     */
    /**********************************************************************************************/
    void Awake()
    {
      cachedTransform = transform;
    }

  }
}