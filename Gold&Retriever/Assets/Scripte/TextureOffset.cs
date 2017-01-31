using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffset : MonoBehaviour
{
    public Vector2 uvAnimationRate = new Vector2(0.6f, 0.0f);

    private string textureName = "_MainTex";
    private Vector2 uvOffset = Vector2.zero;
    private Renderer renderer;

    void Start ()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (renderer.enabled)
        {
            renderer.materials[0].SetTextureOffset(textureName, uvOffset);
        }
    }
}