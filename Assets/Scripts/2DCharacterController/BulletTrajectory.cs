using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贝塞尔曲线类
/// </summary>
public class BezierUtils
{
    /// <summary>
    /// 获取存储贝塞尔曲线点的数组
    /// </summary>
    /// <param name="startPoint"></param>起始点
    /// <param name="controlPoint"></param>控制点
    /// <param name="endPoint"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量
    /// <returns></returns>存储贝塞尔曲线点的数组
    public static Vector3[] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum + 1];
        path[0] = startPoint;
        for (int i = 1; i <= segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculateCubicBezierPoint(t, startPoint,
                controlPoint, endPoint);
            path[i] = pixel;
        }
        return path;
    }

    /// <summary>
    /// 获取控制点.
    /// </summary>
    /// <param name="startPos">起点.</param>
    /// <param name="endPos">终点.</param>
    /// <param name="offset">偏移量.</param>
    public static Vector3 CalcControlPos(Vector3 startPos, Vector3 endPos, float offset)
    {
        //方向(由起始点指向终点)
        Vector3 dir = endPos - startPos;
        //取另外一个方向. 这里取向上.
        Vector3 otherDir = Vector3.up;

        //求平面法线.  注意otherDir与dir不能调换位置,平面的法线是有方向的,(调换位置会导致法线方向相反)
        //unity中世界坐标使用的是左手坐标系,所以法线的方向应该用左手定则判断.
        Vector3 planeNormal = Vector3.Cross(otherDir, dir);

        //再求startPos与endPos的垂线. 其实就是再求一次叉乘.
        Vector3 vertical = Vector3.Cross(dir, planeNormal).normalized;
        //中点.
        Vector3 centerPos = (startPos + endPos) / 2f;
        //控制点.
        Vector3 controlPos = centerPos + vertical * offset;

        return controlPos;
    }

    /// <summary>
    /// 根据T值，计算贝塞尔曲线上面相对应的点
    /// </summary>
    /// <param name="t"></param>T值
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点
    /// <param name="p2"></param>目标点
    /// <returns></returns>根据T值计算出来的贝赛尔曲线点
    public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}




public class BulletTrajectory : BaseVFX
{
    LineRenderer trajectory;


    [Header("贝塞尔曲线参数")]
    public int pointnum;
    public float offset;

    [Header("线材质参数")]
    public Material errormaterial;
    public Material correctmaterial;
    public Material projectioncircle;
    public float circleradius;
    [Range(3,10)] public float circlewidth=5;


    public Vector3 startpoint { get; set; }
    public Vector3 endpoint { get; set; }

    public void SetMaterial(bool state)
    {
        if(state)
        {
            trajectory.material = correctmaterial;
            projectioncircle.SetColor("_Color", correctmaterial.color);
        }
        else
        {
            trajectory.material = errormaterial;
            projectioncircle.SetColor("_Color", errormaterial.color);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        trajectory = GetComponent<LineRenderer>();
        if(trajectory == null) Debug.LogError("Line Renderer not set!");
        trajectory.positionCount = pointnum + 1;
        if(projectioncircle==null) Debug.LogError("Circle shader not set!");
        projectioncircle.SetFloat("_circleRadius", circleradius);
        projectioncircle.SetFloat("_Width", circlewidth);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<=pointnum;i++)
        {
            Vector3 controlpoint = BezierUtils.CalcControlPos(startpoint, endpoint, offset);
            Vector3 tmppoint = BezierUtils.CalculateCubicBezierPoint(i / (float)pointnum, startpoint, controlpoint, endpoint);
            trajectory.SetPosition(i, tmppoint);
        }
        projectioncircle.SetVector("_circlePos", new Vector4(endpoint.x, endpoint.y, endpoint.z, 1));
    }

    private void OnDestroy()
    {
        projectioncircle.SetFloat("_circleRadius", 0);
        projectioncircle.SetVector("_circlePos", new Vector4(0, 0, 0, 1));
    }
}
