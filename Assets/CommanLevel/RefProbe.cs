using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefProbe : MonoBehaviour
{
    ReflectionProbe reflectionProbe;
    float timer;
    [SerializeField] RenderTexture rt;
    [SerializeField]Material material;
    Texture texture;
    [SerializeField]float duration;
    private void Awake() {
         reflectionProbe = GetComponent<ReflectionProbe>();
         reflectionProbe.RenderProbe();
         
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        if(timer>duration){
            myupdate();
            timer =0.0f;
        }
    }
    void myupdate(){
        reflectionProbe.RenderProbe();
        texture = reflectionProbe.texture;
        Graphics.Blit(texture,rt);
        material.SetTexture("_CubeMap",texture);
    }
}
