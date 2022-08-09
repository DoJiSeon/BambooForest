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
    //[Header("�̵��ӵ� ����")]
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
            Debug.Log("��ҽ��ϴ�.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("������ ȹ��");
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
        speed_vec = Vector2.zero;//1�ʸ��� ��� 0���� ������Ʈ
        if (Input.GetKey(KeyCode.RightArrow))//������ ����Ű ������
        {
            speed_vec.x += 0.008f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))//���� ����Ű ������
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
