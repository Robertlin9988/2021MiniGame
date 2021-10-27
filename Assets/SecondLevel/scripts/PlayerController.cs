using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static string mtag = "Player";
    const float bottom =-6.0f; 
    static bool firstObstacle =true;
    static public bool IsDead{
        get;
        set;
    }

   [SerializeField]float speed = 5.0f;
   [SerializeField]float jumoForce=0.3f;
   
   Animator animator;
   LayerMask ground;
   bool jump ;
   float playerInput;
   Rigidbody2D rb;
   //Animator animator;
   private void Start() {
       //animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();
       ground =LayerMask.GetMask("ground");
       animator = GetComponent<Animator>();
   }

    void Update(){
        if(IsDead){
            Respawn();
            IsDead=false;
        }
        playerInput = Input.GetAxis("Horizontal");
        jump |= Input.GetButtonDown("Jump");
        if(CheckFallen()){
            Respawn();
        }
        
        if(Mathf.Abs(playerInput)>0){
            transform.rotation = playerInput>0? Quaternion.identity:Quaternion.Euler(0,180,0);
            animator.SetBool("walk",true);
        }else{
            animator.SetBool("walk",false);
        }

        //animator.SetBool("fly",playerInput.x!=0);
    }
    private void FixedUpdate() {
        //transform.Translate(new Vector2(playerInput*Time.deltaTime*speed,0));
        
        rb.velocity = new Vector2(playerInput*speed,rb.velocity.y);
        Jump();
    }
    private bool CheckFallen(){
        if(transform.position.y<=bottom){
            return true;
        }
        return false;
    }


    private void Respawn(){
        //SceneTool.Instance.ReloadScene();
        ScenneManagement.GetInstance().LoadSceneSingle(4);
    }
    private void Jump(){
        if(Physics2D.Raycast(transform.position,Vector2.down,0.5f,ground)&&jump){
            rb.velocity =new Vector2(rb.velocity.x,jumoForce);
        }
        jump=false; 
    }

    public static bool IsFirstBlock(){
        if(firstObstacle){
            firstObstacle =false;
            return true;
        }
        return false;
    }

}
