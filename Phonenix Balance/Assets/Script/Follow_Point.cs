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

    // ��һ��Ŀ��λ��
    private Vector3 lastTargetPosition;

    void Start()
    {
        if (target != null)
        {
            // ��ʼ����һ��Ŀ��λ��
            lastTargetPosition = target.position;
        }
    }

    void Update()
    {
        if (enableFollow && target != null)
        {
            // ���Ŀ��λ���Ƿ����仯
            if (target.position != lastTargetPosition)
            {
                // ƽ���ƶ���Ŀ��λ��
                transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);

                // ������һ��Ŀ��λ��
                lastTargetPosition = target.position;
            }
        }
        else if (target == null)
        {
            Debug.LogWarning("δָ��Ŀ�����壡");
        }
    }
}
