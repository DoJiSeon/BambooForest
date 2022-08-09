using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 speed_vec;
    float moveX, moveY;
    SpriteRenderer sprite;
    float coroutine_timer;
    static bool isgod;
    //[Header("이동속도 조절")]
    //[SerializeField] [Range(1f, 80f)] float moveSpeed = 80f;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 1f);
        coroutine_timer = 0;
        isgod = false;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "wall")
        {
            Debug.Log("닿았습니다.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("아이템 획득");
            StartCoroutine(godMode());
        }
    }
    public static void Die()
    {
        if (!isgod)
        {
            GameManager.instance().GameoverCheck(true);
        }
        
        
        
    }
    IEnumerator godMode()
    {
        
        while (coroutine_timer <5)
        {
            
            coroutine_timer += 0.5f;
            isgod = true;
            Debug.Log(coroutine_timer);
            sprite.color = new Color(1, 1, 1, 0.6f);
            if(coroutine_timer == 4)
            {
                isgod = false;
                sprite.color = new Color(1, 1, 1, 1f);
                coroutine_timer = 0;
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    private void PlayerMove()
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
}
