using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDestroy : MonoBehaviour
{
    public float cubesize = 0.2f;
    public int cubeinrow = 5;
    public Material material;
    public GameObject[] fractured;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //explode();
            Break();
        }
    }

    public void explode()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < cubeinrow; i++)
            for (int j = 0; j < cubeinrow; j++)
                for (int k = 0; k < cubeinrow; k++)
                    createpieces(i, j, k);
    }

    void createpieces(int x,int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);


        piece.GetComponent<MeshRenderer>().material = material;
        piece.transform.position = transform.position + new Vector3(cubesize * x, cubesize * y, cubesize * z);
        piece.transform.localScale = new Vector3(cubesize, cubesize, cubesize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubesize;
    }

    void Break()
    {
        foreach(GameObject obj in fractured)
        {
            obj.SetActive(true);
            Destroy(obj, 2);
        }
        Destroy(gameObject);
    }
}
