using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_A_Mouse : MonoBehaviour
{
    private bool isDragging = false; // �Ƿ�������ק
    private Vector3 offset; // ����������ƫ����
    private Rigidbody2D rb; // �������
    private Camera mainCamera; // �������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // ��������������ã�����ÿ֡���� Camera.main
    }

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            ToggleDragging();
        }

        // �϶�����
        if (isDragging)
        {
            DragObject();
        }
    }

    private void ToggleDragging()
    {
        // �л���ק״̬
        isDragging = !isDragging;

        if (isDragging)
        {
            // ��ʼ��קʱ����������������ƫ����
            Vector3 mousePosition = GetMouseWorldPosition();
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            if (collider != null && collider.transform == transform)
            {
                offset = transform.position - mousePosition;

                // ���ø��������������������ģ��
                if (rb != null)
                {
                    rb.gravityScale = 0;
                }
            }
            else
            {
                // ������δ����������ϣ�ȡ����ק
                isDragging = false;
            }
        }
        else
        {
            // ֹͣ��קʱ���ָ����������
            if (rb != null)
            {
                rb.gravityScale = 1;
            }
        }
    }

    private void DragObject()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 targetPosition = mousePosition + offset;

        // ʹ�� Rigidbody2D �� MovePosition �����ƶ�����
        if (rb != null)
        {
            rb.MovePosition(targetPosition);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ȷ�� z ����Ϊ 0
        return mousePosition;
    }
}
