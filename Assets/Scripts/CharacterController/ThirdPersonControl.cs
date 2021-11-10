using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonControl : MonoBehaviour
{
    [Header("速度")]
    public float turnspeed = 20f;
    public float movespeed = 0.5f;
    public float jumpspeed = 5;

    [Header("是否启用移动")]
    public bool canmove;

    [Header("动画状态")]
    public bool isjump;
    public bool isfall;
    public bool isaiming;

    [Header("触地检测")]
    public GameObject feet;
    public float maxraylength = 0.1f;


    /// <summary>
    /// should be initialized by birthpoints
    /// </summary>
    private Vector3 initialpos;
    private Quaternion initialrotation;
    public GameObject[] birthpoints;
    public bool initialbirthpoints = false;

    Animator character_anim;
    CharacterController m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;
    Vector3 playercelocity;
    Vector3 m_Movement;
    Vector3 movedir;

    float horizontal;
    float vertical;
    float currentjumpspeed;
    bool isWalking;


    public void SetCanmove(bool state)
    {
        canmove = state;
    }

    private void Awake()
    {
        int index = 0;
        if (PlayerPrefs.HasKey(savesettings.birthpoint))
        {
            index = PlayerPrefs.GetInt(savesettings.birthpoint);
        }
        if (birthpoints.Length > 0)
        {
            initialpos = birthpoints[index].transform.position;
            initialrotation = birthpoints[index].transform.rotation;
        }
        else
        {
            initialpos = transform.position;
            initialrotation = transform.rotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        character_anim = GetComponent<Animator>();
        m_Rigidbody = GetComponent<CharacterController>();
        canmove = true;
        currentjumpspeed = jumpspeed;

        if(initialbirthpoints)
        {
            //初始化位置
            m_Rigidbody.Move(initialpos - transform.position);
            transform.rotation = initialrotation;
        }
    }

    void WalkAndRotate()
    {
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isWalking = canmove && (hasHorizontalInput || hasVerticalInput);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnspeed * Time.deltaTime, 0f);
        if (!isWalking||isjump) desiredForward = transform.forward;
        if (isaiming) desiredForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        m_Rotation = Quaternion.LookRotation(desiredForward);


        character_anim.SetBool(AnimParms.IsWalking, isWalking);
        character_anim.SetFloat(AnimParms.horizontal, horizontal);
        character_anim.SetFloat(AnimParms.vertical, vertical);
    }

    void Jump()
    {
        currentjumpspeed += Physics.gravity.y * Time.fixedDeltaTime;
        playercelocity.y = currentjumpspeed;
    }

    void Fall()
    {
        playercelocity.y += Physics.gravity.y * Time.fixedDeltaTime;
    }


    bool LandDetector()
    {
        bool detected = false;
        if(Physics.Raycast(feet.transform.position, -Vector3.up, maxraylength))
        {
            detected = true;
        }
        character_anim.SetBool(AnimParms.Land, detected);
        Color raycol = detected ? Color.green : Color.yellow;
        Debug.DrawRay(feet.transform.position, -Vector3.up * maxraylength, raycol);
        return detected;
    }




    // Update is called once per frame
    void Update()
    {
        //仅当不处于跳跃阶段时开启移动检测
        if(!isjump)
        {
            //所有input都应该放在update中
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        //以相机正方向的移动 (两向量相加)
        m_Movement = new Vector3(Camera.main.transform.forward.x,0, Camera.main.transform.forward.z) *vertical+ new Vector3(Camera.main.transform.right.x,0, Camera.main.transform.right.z) *horizontal;
        m_Movement.Normalize();
    }



    private void FixedUpdate()
    {
        WalkAndRotate();
        LandDetector();

        if(isfall)
        {
            //Debug.Log("fall");
            Fall();
        }
        else if(isjump)
        {
            Jump();
        }
        else
        {
            playercelocity = Vector3.zero;
            currentjumpspeed = jumpspeed;
        }
    }


    void OnAnimatorMove()
    {
        if (canmove||isfall)
        {
            Vector3 movedir = m_Movement * new Vector3(character_anim.deltaPosition.x,0, character_anim.deltaPosition.z).magnitude * movespeed;
            movedir.y = playercelocity.y* Time.fixedDeltaTime;
            m_Rigidbody.Move(movedir);
            transform.rotation = m_Rotation;
        }
    }
}
