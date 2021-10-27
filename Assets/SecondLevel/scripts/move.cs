using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public  static float speed =1.0f;
   private void FixedUpdate() {
       transform.position += Vector3.right*Time.deltaTime*speed;
   }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == PlayerController.mtag){
            PlayerController.IsDead=true;
        }
    }
}
