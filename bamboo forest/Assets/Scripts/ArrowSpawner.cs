using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefeb;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    public GameObject targetObj;
    public Vector3 targetPosition;

    public GameObject bulletObjA;
    public float speed;

    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = 2f;
        target = FindObjectOfType<PlayerController>().transform;
        speed = 1.5f;
    }

    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            //gameobject arrow = instantiate(arrowprefeb, transform.position, transform.rotation);

            //arrow.transform.lookat(target);

            //spawnrate = 0.5f;

            Fire();
        }

    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = targetObj.transform.position - transform.position;
        rigid.AddForce(dirVec * speed, ForceMode2D.Impulse);
        Destroy(bullet, 2f);
    }
}
