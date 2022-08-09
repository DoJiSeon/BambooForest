using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 speed_vec;
    float moveX, moveY;

    [Header("이동속도 조절")]
    [SerializeField] [Range(1f, 80f)] float moveSpeed = 80f;

    // Update is called once per frame
    void Update()
    {

        //moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        //transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);

        speed_vec = Vector2.zero;//1초마다 계속 0으로 업데이트
        if (Input.GetKey(KeyCode.RightArrow))//오른쪽 방향키 누르면
        {
            speed_vec.x += 0.008f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽 방향키 누르면
        {
            speed_vec.x += -0.008f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed_vec.y += 0.008f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed_vec.y += -0.008f;
        }
        transform.Translate(speed_vec);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("wall"))
        {
            Debug.Log("닿았습니다.");
        }
    }

    public static void Die()
    {
        GameManager.instance().GameoverCheck(true);
        Debug.Log("죽었습니다");
    }
}
