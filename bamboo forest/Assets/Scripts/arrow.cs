using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D arrowRigidbody;

    void Start()
    {
        //arrowRigidbody = GetComponent<Rigidbody2D>();

        //arrowRigidbody.velocity = transform.forward * speed;

        //Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("ав╬З╫ю╢о╢ы.-arrow");
            PlayerController.Die();
        }
    }
}
