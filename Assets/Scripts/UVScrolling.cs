using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScrolling : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";

    private Renderer rend;

    private Material material;

    Vector2 uvOffset = Vector2.zero;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        material = rend.materials[materialIndex];
    }

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled)
        {
            material.SetTextureOffset(textureName, uvOffset);
        }
    }
}
