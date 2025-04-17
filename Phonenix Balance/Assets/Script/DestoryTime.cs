using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTime : MonoBehaviour
{
    // ָ���ļ������
    [SerializeField]
    private GameObject targetObject;

    // ����ʱ��ʱ�䣨�룩
    [SerializeField]
    private float countdownTime = 3f;

    // ����ʱ��������Ҫ���������
    [SerializeField]
    private GameObject objectToActivate;

    private Collider2D spriteMaskCollider; // ��ǰ����ľ�������
    private Collider2D targetCollider; // ָ���������ײ��
    private bool isCountingDown = false; // �Ƿ����ڵ���ʱ
    private bool isOverlapping = false; // �Ƿ���Ŀ�������н���

    void Start()
    {
        // ��ȡ��ǰ����� Collider2D
        spriteMaskCollider = GetComponent<Collider2D>();
        if (spriteMaskCollider == null)
        {
            Debug.LogError("��ǰ����ȱ�� Collider2D �����");
        }

        // ��ȡĿ������� Collider2D
        if (targetObject != null)
        {
            targetCollider = targetObject.GetComponent<Collider2D>();
            if (targetCollider == null)
            {
                Debug.LogError("Ŀ������ȱ�� Collider2D �����");
            }
        }
        else
        {
            Debug.LogError("δָ��Ŀ�����壡");
        }

        // ȷ����Ҫ����������ѽ���
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogError("δָ����Ҫ��������壡");
        }
    }

    void FixedUpdate()
    {
        // ������⵱ǰ����ľ��������Ƿ���Ŀ�������н���
        if (spriteMaskCollider != null && targetCollider != null)
        {
            isOverlapping = spriteMaskCollider.IsTouching(targetCollider);

            if (isOverlapping && !isCountingDown)
            {
                StartCoroutine(StartCountdown());
            }
        }
    }

    private IEnumerator StartCountdown()
    {
        isCountingDown = true;

        // �ȴ�����ʱ����
        yield return new WaitForSeconds(countdownTime);

        // ���õ�ǰ����
        gameObject.SetActive(false);

        // ����ָ��������
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        isCountingDown = false;
    }
}
