using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyFogTexToMaskTex : MonoBehaviour
{
    public RenderTexture texToSample;
    public Texture2D texToSet;

    void OnPostRender()
    {
        Debug.Log("sample me");
        var old_rt = RenderTexture.active;
        RenderTexture.active = texToSample;

        texToSet.ReadPixels(new Rect(0, 0, texToSample.width, texToSample.height), 0, 0);
        texToSet.Apply();

        RenderTexture.active = old_rt;
    }

    //void FixedUpdate()
    //{
    //    //RenderTexture.ReadPixels

    //    //if (timer % 50 == 0)
    //    //{
    //    Debug.Log("sample me");
    //    Texture2D tex = new Texture2D(texToSample.width, texToSample.height, TextureFormat.RGBA32, false);
    //    var old_rt = RenderTexture.active;
    //    RenderTexture.active = texToSample;

    //    tex.ReadPixels(new Rect(0, 0, texToSample.width, texToSample.height), 0, 0);
    //    tex.Apply();

    //    RenderTexture.active = old_rt;

    //    //gameObject.GetComponent<RawImage>().texture = texToPass;
    //    //}

    //    timer++;
    //}
}
