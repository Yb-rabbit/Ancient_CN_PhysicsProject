using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Point : MonoBehaviour
{
    // Ŀ������
    public Transform target;

    // �Ƿ�����λ��ͬ��
    public bool enableFollow = true;

    // ƽ���ƶ��ٶ�
    public float smoothSpeed = 5f;

    // ���ƫ��뾶
    public float maxRadius = 10f;

    // ��ǰ�ٶȣ��� SmoothDamp ��̬������
    private Vector3 currentVelocity = Vector3.zero;

    // ��һ��Ŀ��λ��
    private Vector3 lastTargetPosition;

    // �Ƿ��Ѿ���¼��δָ��Ŀ��ľ���
    private bool hasLoggedWarning = false;

    // �������������������ܣ�
    private Rigidbody2D rb;

    void Start()
    {
        if (target != null)
        {
            // ��ʼ����һ��Ŀ��λ��
            lastTargetPosition = target.position;
        }

        // ��ȡ�������
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (enableFollow && target != null)
        {
            FollowTarget();
            hasLoggedWarning = false; // ���þ����־
        }
        else if (target == null && !hasLoggedWarning)
        {
            Debug.LogWarning("δָ��Ŀ�����壡");
            hasLoggedWarning = true; // �����ظ��������
        }
    }

    private void FollowTarget()
    {
        // ���Ŀ��λ���Ƿ����仯
        if (target.position != lastTargetPosition)
        {
            // ����Ŀ��λ���뵱ǰ����λ�õķ���;���
            Vector3 directionToTarget = target.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            // ������볬�����뾶������׷��Ŀ��λ�ò��������
            if (distanceToTarget > maxRadius)
            {
                // ֱ�ӽ�����λ������ΪĿ��λ��
                transform.position = target.position;

                // �������
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
            else
            {
                // �����Ʒ�Χ�ڣ�ʹ�� SmoothDamp ƽ���ƶ���Ŀ��λ��
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position,
                    ref currentVelocity,
                    1f / smoothSpeed // ƽ��ʱ�䣬ֵԽС�ƶ�Խ��
                );
            }

            // ������һ��Ŀ��λ��
            lastTargetPosition = target.position;
        }
    }
}
