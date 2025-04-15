using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_A_Mouse : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private Camera mainCamera;

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
            HandleMouseDown();
        }

        // ����������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }

        // �϶�����
        if (isDragging)
        {
            DragObject();
        }
    }

    private void HandleMouseDown()
    {
        // ��ȡ���λ��
        Vector3 mousePosition = GetMouseWorldPosition();

        // �������Ƿ����ڵ�ǰ������
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);
        if (collider != null && collider.transform == transform)
        {
            isDragging = true;
            offset = transform.position - mousePosition;

            // ���ø��������������������ģ��
            if (rb != null)
            {
                rb.gravityScale = 0;
            }
        }
    }

    private void HandleMouseUp()
    {
        isDragging = false;

        // �ָ����������
        if (rb != null)
        {
            rb.gravityScale = 1;
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
