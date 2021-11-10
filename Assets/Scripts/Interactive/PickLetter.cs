using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLetter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OperaManager.GetInstance().SetStateTrans();
    }
}
