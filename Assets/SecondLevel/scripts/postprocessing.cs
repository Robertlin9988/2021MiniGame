using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postprocessing : MonoBehaviour
{

     [SerializeField]Texture texture;
    [SerializeField]Material aaa;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
       Graphics.Blit(texture,dest,aaa);
    }

}
