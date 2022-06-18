using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveX, moveY;

    [Header("�̵��ӵ� ����")]
    [SerializeField] [Range(1f, 30f)] float moveSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);
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
