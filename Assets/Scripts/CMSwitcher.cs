using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase thirdpersoncm;
    public CinemachineVirtualCameraBase aimcm;

    AxisState xAxis;


    private static CMSwitcher instance;
    public static CMSwitcher GetInstance() => instance;

    private void Awake()
    {
        instance = this;
        thirdpersoncm.MoveToTopOfPrioritySubqueue();
        thirdpersoncm.GetComponent<CinemachineFreeLook>().m_XAxis.Value = 0;
        thirdpersoncm.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.5f;
    }

    public void SwitchCM()
    {
        if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Name== thirdpersoncm.Name)
        {
            aimcm.GetComponent<CinemachineFreeLook>().m_XAxis.Value = thirdpersoncm.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
            aimcm.GetComponent<CinemachineFreeLook>().m_YAxis.Value = thirdpersoncm.GetComponent<CinemachineFreeLook>().m_YAxis.Value;
            aimcm.MoveToTopOfPrioritySubqueue();
        }
        else
        {
            thirdpersoncm.GetComponent<CinemachineFreeLook>().m_XAxis.Value = aimcm.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
            thirdpersoncm.GetComponent<CinemachineFreeLook>().m_YAxis.Value = aimcm.GetComponent<CinemachineFreeLook>().m_YAxis.Value;
            thirdpersoncm.MoveToTopOfPrioritySubqueue();
        }
    }

    private void Update()
    {
        
    }
}
