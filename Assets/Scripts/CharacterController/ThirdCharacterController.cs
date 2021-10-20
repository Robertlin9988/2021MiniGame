using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCharacterController : MonoBehaviour
{
    [Header("转动速度")]
    public float turnspeed = 20f;
    public float movespeed = 0.5f;


    Animator character_anim;
    CharacterController m_Rigidbody;
    Vector3 m_Movement;
    Vector3 movedir;
    Quaternion m_Rotation = Quaternion.identity;

    float horizontal;
    float vertical;
    bool isWalking;

    public bool canmove { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        character_anim = GetComponent<Animator>();
        m_Rigidbody = GetComponent<CharacterController>();
        canmove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //所有input都应该放在update中
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (m_Rigidbody.isGrounded)
        {
            m_Movement= transform.TransformDirection(new Vector3(horizontal,0, vertical));
            m_Movement.Normalize();
        }
        m_Movement.y += Physics.gravity.y * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z), turnspeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
        character_anim.SetBool(AnimParms.IsWalking, isWalking);
    }


    void OnAnimatorMove()
    {
        if (canmove)
        {
            m_Rigidbody.Move(m_Movement * character_anim.deltaPosition.magnitude * movespeed);
            if (isWalking)
            {
                transform.rotation = m_Rotation;
            }
        }
    }
}
