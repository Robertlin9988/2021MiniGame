using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerController.mtag)
        {
            ScenneManagement.GetInstance().UnloadScene(4);
        }
    }
}
