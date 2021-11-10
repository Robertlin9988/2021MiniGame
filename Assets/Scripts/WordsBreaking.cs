using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsBreaking : MonoBehaviour
{
    private Vector3 offset = new Vector3(-1.7f, 1.2f, 0);


    public void explode()
    {
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce((rb.transform.position - (transform.position+offset)).normalized*200);
        }
    }

    private void Awake()
    {
        explode();
        Destroy(this.gameObject, 2);
    }
}
