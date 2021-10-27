using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float changepoint =20.0f;
    const float attackprobs = 0.2f;
    [SerializeField]GameObject attackpiece;
    Transform player;
    SpriteRenderer spriteRenderer;
    Animator animator;
    float checkfrequency =2.0f;
    float timer =0.5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(PlayerController.mtag).transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
    }
    private void Update() {
        timer+= Time.deltaTime;
        if(timer>checkfrequency){
            attack();
            timer =0.0f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(transform.position.x>=changepoint){
            Color laserRed = new Vector4 (2.0f, 0.0f, 0.0f, 1.0f);
            spriteRenderer. material.SetColor("_Color",laserRed);
            move.speed =2.0f;
        }
        transform.position = new Vector3(player.position.x,transform.position.y,transform.position.z);
    }

    void attack(){
        if(Random.Range(0.0f,1.0f)>attackprobs){
            animator.SetTrigger("attack");
            Instantiate(attackpiece,transform.position,Quaternion.identity);
        }
    }
}
