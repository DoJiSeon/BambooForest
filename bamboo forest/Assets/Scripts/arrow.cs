using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D arrowRigidbody;
    Vector3 newdir;
    Transform tr;
    public GameObject MyOBJ;
    bool Collision;

    private float spawnRate;
    private float timeAfterSpawn;
    void Start()
    {
        //arrowRigidbody = GetComponent<Rigidbody2D>();

        //arrowRigidbody.velocity = transform.forward * speed;

        //Destroy(gameObject, 3f);
        newdir = new Vector3(0, 0, 0);
        tr = gameObject.transform;
        float tr_z = tr.eulerAngles.z;
        newdir.z = tr_z;
        tr.eulerAngles = newdir;

        timeAfterSpawn = 0f;
        spawnRate = 2f;
        Collision = false;
    }

    private void Update()
    {
        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn >= spawnRate)
        {
            if (MyOBJ.CompareTag("Enemy_10min"))
            {
                Collision = true;
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("ав╬З╫ю╢о╢ы.-arrow");
            PlayerController.Die();
        }else if (collision.tag == "wall")
        {
            if (Collision)
            {
                Destroy(gameObject);
            }
        }
    }
}
