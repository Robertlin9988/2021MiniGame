using UnityEngine.Events;
using UnityEngine;

public class Tcollider : MonoBehaviour
{
    const string playertag = "Player";
    private event UnityAction triggerEvent;
    public void AddEvent( UnityAction func){
        triggerEvent +=  func;
    }
    public void RemoveEvent(UnityAction func){
        triggerEvent-= func;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag==playertag){
            if(triggerEvent!=null){
                triggerEvent();
            }
        }
    }
}
