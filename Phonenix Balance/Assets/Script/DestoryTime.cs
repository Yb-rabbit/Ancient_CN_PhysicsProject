using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTime : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private float countdownTime = 3f;

    [SerializeField]
    private GameObject objectToActivate;

    private Collider2D spriteMaskCollider;
    private Collider2D targetCollider;
    private bool isCountingDown = false;
    private bool isOverlapping = false;

    private Coroutine countdownCoroutine;

    void Start()
    {
        spriteMaskCollider = GetComponent<Collider2D>();
        if (spriteMaskCollider == null)
        {
            Debug.LogError("��ǰ����ȱ�� Collider2D �����");
        }

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
        if (spriteMaskCollider != null && targetCollider != null && targetCollider.enabled && targetObject.activeInHierarchy)
        {
            bool currentlyOverlapping = spriteMaskCollider.IsTouching(targetCollider);

            if (currentlyOverlapping)
            {
                isOverlapping = true;

                if (!isCountingDown && countdownCoroutine == null)
                {
                    countdownCoroutine = StartCoroutine(StartCountdown());
                }
            }
            else
            {
                isOverlapping = false;

                if (isCountingDown && countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                    countdownCoroutine = null;
                    isCountingDown = false;
                }
            }
        }
        else
        {
            isOverlapping = false;
        }
    }

    private IEnumerator StartCountdown()
    {
        isCountingDown = true;

        yield return new WaitForSeconds(countdownTime);

        if (isOverlapping)
        {
            gameObject.SetActive(false);

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            else
            {
                Debug.LogWarning("��Ҫ����������ѱ����ٻ�δ���ã�");
            }
        }

        isCountingDown = false;
        countdownCoroutine = null;
    }
}
