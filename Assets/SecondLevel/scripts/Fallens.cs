using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fallens : TriggerBase
{
    const string alphatest = "_alphatest";
    const int burstpress =8;
    int pressCount =0;
    bool condition =false;
    Transform player;
    [SerializeField] Transform particels;
    [SerializeField]GameObject textGuid;
    Rigidbody2D[] rbarray;
    protected override void Start()
    {   
        player = GameObject.FindGameObjectWithTag(PlayerController.mtag).transform;
        rbarray =particels.GetComponentsInChildren<Rigidbody2D>();
        particels.gameObject.SetActive(false);
        base.Start();
        
    }   
    private void Update() {
        if(condition){
            if(Input.GetKeyDown(KeyCode.Space)){
                pressCount++;
            }
            if(pressCount>burstpress){
                 foreach(var rb in rbarray){
                    Vector2 force = rb.transform.position -player.position;
                    rb.AddForce(force*10.0f,ForceMode2D.Impulse);
                }
                StartCoroutine(dissolve());
                condition=false;
            }
        }
    }
    IEnumerator dissolve(){
        textGuid.SetActive(false);
        float t =0.0f;
        Material tmp;
        while(t<1.0f){
            t+=Time.deltaTime*1.5f;
            foreach(var rb in rbarray){
               tmp = rb.gameObject.GetComponent<SpriteRenderer>().material;
               tmp.SetFloat(alphatest,t);
            }
            yield return null;
        }
        Destroy(gameObject);
    }
    protected override void TheTriggerEvent(){
        particels.gameObject.SetActive(true);
        textGuid.SetActive(true);
        foreach(var rb in rbarray){
            Vector2 force = rb.transform.position -player.position;
            rb.AddForce(-force*3.0f,ForceMode2D.Impulse);
            condition =true;
        }
        base.TheTriggerEvent();
    }
}
