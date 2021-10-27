using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : MonoBehaviour
{
    Rigidbody2D rb;
    Transform player;

    // Start is called before the first frame update
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector2 force =player.position- transform.position;
        //rb.AddForce(force.normalized*10.0f,ForceMode2D.Force);
    }
}
