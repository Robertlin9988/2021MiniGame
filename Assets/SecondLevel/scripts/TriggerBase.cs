using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : MonoBehaviour
{
   [SerializeField]Tcollider tcollider;

   protected virtual void Start() {
       tcollider.AddEvent(TheTriggerEvent);
   }
   protected virtual void TheTriggerEvent(){
       tcollider.enabled=false;
   }

}
