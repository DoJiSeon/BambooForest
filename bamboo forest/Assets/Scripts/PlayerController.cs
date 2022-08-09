using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 speed_vec;
    float moveX, moveY;

    [Header("�̵��ӵ� ����")]
    [SerializeField] [Range(1f, 80f)] float moveSpeed = 80f;

    // Update is called once per frame
    void Update()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("wall"))
        {
            Debug.Log("��ҽ��ϴ�.");
        }
    }

    public static void Die()
    {
        GameManager.instance().GameoverCheck(true);
        Debug.Log("�׾����ϴ�");
    }
}
