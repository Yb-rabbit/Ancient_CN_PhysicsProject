using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_A_Mouse : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ���λ��
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // �������Ƿ����ڵ�ǰ������
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (collider != null && collider.transform == transform)
            {
                isDragging = true;
                offset = transform.position - mousePosition;

                // ���ø���״̬
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }

        // ����������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // �������ø���״̬
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }

        // �϶�����
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }
}
