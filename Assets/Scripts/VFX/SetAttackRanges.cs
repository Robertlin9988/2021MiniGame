using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttackRanges : MonoBehaviour
{
    public Material ranges;
    public Color rangecolor;
    enemyattack[] enemypatrols;
    List<float> circleRadius = new List<float>();
    List<float> circleangle = new List<float>();
    List<Vector4> circlepos = new List<Vector4>();
    List<Vector4> forwarddir = new List<Vector4>();

    // Start is called before the first frame update
    void Start()
    {
        enemypatrols = GetComponentsInChildren<enemyattack>();
        ranges.SetInt("_pointnum", enemypatrols.Length);
        ranges.SetColor("_Color",rangecolor);
        foreach(enemyattack i in enemypatrols)
        {
            circleRadius.Add(i.atkradius);
            circleangle.Add(i.atkangle);
            circlepos.Add(i.transform.position);
            forwarddir.Add(i.transform.forward);
        }
        ranges.SetFloatArray("_circleRadius", circleRadius);
        ranges.SetFloatArray("_angle", circleangle);
        ranges.SetVectorArray("_selfcirclePos", circlepos);
        ranges.SetVectorArray("_forward", forwarddir);
        #region texture
        //Texture2D datatexture = new Texture2D(2, 3);//一个像素存4个float值 以texture形式传递
        //for (int i=0;i<3;i++)
        //{
        //    datatexture.SetPixel(0, i, new Color(2, -2, -5, 60));
        //    datatexture.SetPixel(1, i, new Color(0, 1, 0, 1));
        //}
        //datatexture.Apply();
        //ranges.SetTexture("_DataTexture", datatexture);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<enemypatrols.Length;i++)
        {
            circlepos[i] = enemypatrols[i].transform.position;
            forwarddir[i] = enemypatrols[i].transform.forward;
        }
        ranges.SetVectorArray("_selfcirclePos", circlepos);
        ranges.SetVectorArray("_forward", forwarddir);
    }

    private void OnDestroy()
    {
        ranges.SetInt("_pointnum", 0);
    }
}
