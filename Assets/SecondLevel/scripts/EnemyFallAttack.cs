using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallAttack : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float force =5f;
    [SerializeField] GameObject brock;
     Transform playerpos;
     float depth =-10.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       playerpos = GameObject.FindGameObjectWithTag(PlayerController.mtag).transform;
         Vector3 dir = playerpos.position -transform.position;
        rb.AddForce(dir.normalized*force,ForceMode2D.Impulse);
    }
    private void FixedUpdate() {
        Vector3 dir = playerpos.position -transform.position;
        dir = Vector3.right*dir.x;
        rb.AddForce(dir.normalized*force*Time.deltaTime,ForceMode2D.Force);
        if(transform.position.y<depth){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == PlayerController.mtag){
            PlayerController.IsDead =true;
        }
        if(other.tag =="Grid"){
            Instantiate(brock,transform.position,Quaternion.Euler(180,0.0f,0.0f));
            Destroy(gameObject);
        }
    }
}
