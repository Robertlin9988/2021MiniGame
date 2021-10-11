using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public class BezierUtils
{
    /// <summary>
    /// ��ȡ�洢���������ߵ������
    /// </summary>
    /// <param name="startPoint"></param>��ʼ��
    /// <param name="controlPoint"></param>���Ƶ�
    /// <param name="endPoint"></param>Ŀ���
    /// <param name="segmentNum"></param>�����������
    /// <returns></returns>�洢���������ߵ������
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
    /// ��ȡ���Ƶ�.
    /// </summary>
    /// <param name="startPos">���.</param>
    /// <param name="endPos">�յ�.</param>
    /// <param name="offset">ƫ����.</param>
    public static Vector3 CalcControlPos(Vector3 startPos, Vector3 endPos, float offset)
    {
        //����(����ʼ��ָ���յ�)
        Vector3 dir = endPos - startPos;
        //ȡ����һ������. ����ȡ����.
        Vector3 otherDir = Vector3.up;

        //��ƽ�淨��.  ע��otherDir��dir���ܵ���λ��,ƽ��ķ������з����,(����λ�ûᵼ�·��߷����෴)
        //unity����������ʹ�õ�����������ϵ,���Է��ߵķ���Ӧ�������ֶ����ж�.
        Vector3 planeNormal = Vector3.Cross(otherDir, dir);

        //����startPos��endPos�Ĵ���. ��ʵ��������һ�β��.
        Vector3 vertical = Vector3.Cross(dir, planeNormal).normalized;
        //�е�.
        Vector3 centerPos = (startPos + endPos) / 2f;
        //���Ƶ�.
        Vector3 controlPos = centerPos + vertical * offset;

        return controlPos;
    }

    /// <summary>
    /// ����Tֵ�����㱴���������������Ӧ�ĵ�
    /// </summary>
    /// <param name="t"></param>Tֵ
    /// <param name="p0"></param>��ʼ��
    /// <param name="p1"></param>���Ƶ�
    /// <param name="p2"></param>Ŀ���
    /// <returns></returns>����Tֵ��������ı��������ߵ�
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


    [Header("���������߲���")]
    public int pointnum;
    public float offset;

    [Header("�߲��ʲ���")]
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
