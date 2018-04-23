using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWorld : MonoBehaviour
{

    public void SaveEgg()
    {
        gameObject.GetComponentInParent<PlayerController>().StartMovement();
    }
}
