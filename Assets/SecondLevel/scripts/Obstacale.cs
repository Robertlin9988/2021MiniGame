using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacale : TriggerBase
{
    const float hideDepth = 3.0f;
    const int breakCount =10;
    bool isInside =false;
    int prescount;
    [SerializeField] float popSpeed=4.0f;
    [SerializeField]GameObject textGuid;
    [SerializeField] GameObject brock;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag ==PlayerController.mtag){
            isInside =true;
            if(PlayerController.IsFirstBlock()){
                textGuid.SetActive(true);
            }
        }
    }
    private void Update() {
        if(isInside){
            if(Input.GetKeyDown(KeyCode.D)){
                prescount++;
            }
            if(prescount>breakCount){
                Instantiate(brock,transform.position,Quaternion.Euler(180,0.0f,0.0f));
                textGuid.SetActive(false);
                Destroy(gameObject);
            }
        }else{
            prescount=0;
        }
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag ==PlayerController.mtag){
            isInside =false;
            textGuid.SetActive(false);
        }
    }
    protected override void TheTriggerEvent()
    {
        StartCoroutine(PopUp());
        base.TheTriggerEvent();
    }
    IEnumerator PopUp(){
        Vector3 dest = transform.position +new Vector3(0, hideDepth);
        while(dest.y - transform.position.y>0.1f){
            transform.position = Vector3.Lerp(transform.position,dest,Time.deltaTime*popSpeed);
            yield return null;
        }
    }

}
