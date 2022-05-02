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


    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = 0.5f;
        target = FindObjectOfType<PlayerController>().transform;

    }

    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if(timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            GameObject arrow = Instantiate(arrowPrefeb, transform.position, transform.rotation);

            arrow.transform.LookAt(target);

            spawnRate = 0.5f;
        }
    }
}
