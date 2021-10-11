using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayerMovement : MonoBehaviour
{
    [Header("转动速度")]
    public float turnspeed =20f;
    public float movespeed = 0.5f;


    Animator character_anim;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Vector3 movedir;
    Quaternion m_Rotation = Quaternion.identity;

    float horizontal;
    float vertical;



    // Start is called before the first frame update
    void Start()
    {
        character_anim = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //所有input都应该放在update中
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
    }

    private void FixedUpdate()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnspeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        character_anim.SetBool(AnimParms.IsWalking, isWalking);
    }


    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * character_anim.deltaPosition.magnitude* movespeed);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
