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
        speed = 1.2f;
    }

    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;
            spawnRate = Random.Range(1.4f, 1.8f);
            Fire();
        }
    }


    public void Fire()
    {

        //GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Vector3 dir = targetObj.transform.position - transform.position; //�÷��̾�� ȭ�� ������ ����
        Quaternion rot = Quaternion.LookRotation(dir.normalized); // ���ʹϾ� ������ ���� �����ؼ� ���� ����
        rot.y = 0;
        GameObject bullet = Instantiate(bulletObjA, transform.position, rot); // ȭ��ǥ ���� ���� �� ����
        
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = targetObj.transform.position - transform.position;
        rigid.AddForce(dirVec * (speed*20), ForceMode2D.Force);
        Destroy(bullet, 5f);
    }
}
