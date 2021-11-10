using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRange : MonoBehaviour
{
    public float awaketime;
    public float targetscale;
    private float currentawaketime;
    private bool isalive;



    private void OnEnable()
    {
        isalive = true;
        currentawaketime = 0;
    }

    private void OnDisable()
    {
        isalive = false;
        currentawaketime = 0;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(isalive&& currentawaketime<= awaketime)
        {
            currentawaketime += Time.deltaTime;
            float scale = Mathf.Lerp(0, targetscale, currentawaketime / awaketime);
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
