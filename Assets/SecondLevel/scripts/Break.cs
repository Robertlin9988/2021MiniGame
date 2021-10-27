using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    const string alphatest = "_alphatest";
    [SerializeField]GameObject platform;
    Material dissolve;
    private void Start() {
        dissolve = platform.GetComponent<SpriteRenderer>().material;
        dissolve.SetFloat(alphatest,0.0f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag==PlayerController.mtag){
             Debug.Log(1);
             StartCoroutine(PlatformBreak());
        }
    }

    // Start is called before the first frame update
    IEnumerator PlatformBreak(){
        float t =0.0f;
        while(t<1.0f){
            t+=Time.deltaTime*1.5f;
            dissolve.SetFloat(alphatest,t);
            yield return null;
        }
        Destroy(platform);
    }
}
