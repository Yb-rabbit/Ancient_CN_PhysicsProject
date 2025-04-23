using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision_Move : MonoBehaviour
{
    private bool isOverlapping = false; // �Ƿ���ָ������ľ��������غ�
    private bool isMoving = false; // �Ƿ������˶�
    private Rigidbody2D targetRb; // Ŀ������ĸ���

    // ָ���ľ�����������
    [SerializeField]
    private GameObject spriteMaskObject;

    // ��Ҫ�����˶���Ŀ������
    [SerializeField]
    private GameObject targetObject;

    // �˶��ٶ�
    [SerializeField]
    private float moveSpeed = 5f;

    // ����ϵ��
    [SerializeField]
    private float deceleration = 2f;

    private void Awake()
    {
        // ����Ŀ������ĸ�������
        if (targetObject != null)
        {
            targetRb = targetObject.GetComponent<Rigidbody2D>();
            if (targetRb == null)
            {
                Debug.LogError("Ŀ������ȱ�� Rigidbody2D �����");
            }
        }
        else
        {
            Debug.LogError("δָ��Ŀ�����壡");
        }

        // �����������Ƿ����
        if (Camera.main == null)
        {
            Debug.LogError("������û�����������");
        }
    }

    private void Update()
    {
        // ������������º��ɿ�
        if (isOverlapping)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMoving = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (targetRb == null) return;

        if (isMoving)
        {
            // ��ǰ�����ƶ�Ŀ������
            Vector2 moveDirection = transform.up; // 2D ǰ��Ϊ transform.up
            targetRb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // ��ֹͣ�˶�
            targetRb.velocity = Vector2.Lerp(targetRb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����Ƿ���ָ���ľ������������غ�
        if (collision.gameObject == spriteMaskObject)
        {
            isOverlapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ����Ƿ��뿪ָ���ľ�����������
        if (collision.gameObject == spriteMaskObject)
        {
            isOverlapping = false;
            isMoving = false; // ֹͣ�˶�
        }
    }
}
