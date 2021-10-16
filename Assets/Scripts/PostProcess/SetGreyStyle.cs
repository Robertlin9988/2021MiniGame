using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGreyStyle : MonoBehaviour
{
    [Header("材质")]
    public Material greystyle;

    [Header("范围参数")]
    [Range(0.0f, 3.0f)]
    public float insidecircleRadius = 1;
    [Range(0.0f, 20.0f)]
    public float outercircleRadius = 10.0f;
    [Range(0.0f, 3.0f)]
    public float offset = 0.5f;
    [Range(0, 180)]
    public float angle = 60;

    [Header("后处理参数")]
    [Range(0.0f, 1.0f)]
    public float brightness = 0.0f;
    [Range(0.0f, 3.0f)]
    public float saturation = 1.0f;
    [Range(0.0f, 3.0f)]
    public float contrast = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (greystyle == null) Debug.LogError("Material not set!");
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("_insidecircleRadius", insidecircleRadius);
        Shader.SetGlobalFloat("_outercircleRadius", outercircleRadius);
        Shader.SetGlobalVector("_circlePos", new Vector4(transform.position.x, transform.position.y, transform.position.z, 1));
        greystyle.SetFloat("_offset", offset);
        greystyle.SetFloat("_angle", angle);
        greystyle.SetVector("_forward", new Vector4(transform.forward.x, transform.forward.y, transform.forward.z, 1));
        greystyle.SetFloat("_Brightness", brightness);
        greystyle.SetFloat("_Saturation", saturation);
        greystyle.SetFloat("_Contrast", contrast);
    }
}
