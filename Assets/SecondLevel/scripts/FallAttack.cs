using UnityEngine;

public class FallAttack : MonoBehaviour
{
    float length ;
    float speed;
    float currentlen;
    Vector3 origin;
    private void Start() {
        speed = Random.Range(2.0f,6.0f); 
        length =10.0f;
        origin = new Vector3(transform.position.x,3.0f,transform.position.z);
        currentlen =origin.y - transform.position.y;
    }
    private void Update() {
        currentlen %= length;
        currentlen += speed*Time.deltaTime;
        Vector3 down = currentlen* Vector3.down;
        transform.position = origin+ down;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag ==PlayerController.mtag){
            PlayerController.IsDead=true;
        }
    }
}
