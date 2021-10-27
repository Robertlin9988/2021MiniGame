using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyattack : MonoBehaviour
{
    [Header("¹¥»÷·¶Î§")]
    public float atkradius = 2;
    [Range(0, 180)] public float atkangle = 60;

    Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (target == null) Debug.Log("no palyer find!");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position-transform.position;
        float distance = dir.magnitude;
        float ang = Vector3.Angle(transform.forward, dir);
        if(distance<= atkradius && ang<= atkangle/2)
        {
            UIManager.GetInstance().HideALLPanel();
            ScenneManagement.GetInstance().LoadSceneSingle(3);
            EventCenter.GetInstance().Clear();
        }
    }
}
