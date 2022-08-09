using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArrowSpawner: MonoBehaviour
{
    public GameObject arrowPrefeb; // 화살 프리팹
    private float spawnRate; // 발사 시간
    private float timeAfterSpawn; // 시간 계산
    public string position; // 스포너 위치 Left, Right, Up, Down
    int random_fire;
    public GameObject bulletObjA; // 화살 프리팹
    public float speed;
    SpriteRenderer sprite;
    void Start()
    {
        timeAfterSpawn = 0f;
        speed = 1.2f;
        random_fire = 1;
        spawnRate = 10;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;
            
            if (random_fire > 4)
            {
                random_fire = 1;
                Fire();
            }
            else
            {
                Fire();
                random_fire += 1;
            }
        }
    }

    private void Fire()
    {
        
        switch (random_fire)
        {
            case 1: // 왼쪽
                if (position=="Left")
                {
                    GameObject Lbullet = Instantiate(bulletObjA, transform.position, transform.rotation); // 화살표 방향 설정 후 복제
                    Rigidbody2D Lrigid = Lbullet.GetComponent<Rigidbody2D>();
                    Lrigid.AddForce(Vector3.right * (speed * 90), ForceMode2D.Force); 
                    Destroy(Lbullet, 7f);
                }
                break;
            case 2: // 위쪽
                if (position == "Up")
                {
                    GameObject Ubullet = Instantiate(bulletObjA, transform.position, transform.rotation); // 화살표 방향 설정 후 복제
                    Rigidbody2D Urigid = Ubullet.GetComponent<Rigidbody2D>();
                    Urigid.AddForce(Vector3.down * (speed * 90), ForceMode2D.Force);
                    Destroy(Ubullet, 7f);
                }
                break;
            case 3: // 오른쪽
                if (position == "Right")
                {
                    GameObject Rbullet = Instantiate(bulletObjA, transform.position, transform.rotation); // 화살표 방향 설정 후 복제
                    Rigidbody2D Rrigid = Rbullet.GetComponent<Rigidbody2D>();
                    Rrigid.AddForce(Vector3.left * (speed * 90), ForceMode2D.Force); 
                    Destroy(Rbullet, 7f);
                }
                break;
            case 4: // 아래쪽
                if (position == "Down")
                {
                    GameObject Dbullet = Instantiate(bulletObjA, transform.position, transform.rotation); // 화살표 방향 설정 후 복제
                    Rigidbody2D Drigid = Dbullet.GetComponent<Rigidbody2D>();
                    Drigid.AddForce(Vector3.up * (speed * 90), ForceMode2D.Force); 
                    Destroy(Dbullet, 7f);
                }
                break;
        }
    }
}
