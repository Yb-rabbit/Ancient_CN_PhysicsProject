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
            Debug.LogError("当前物体缺少 Collider2D 组件！");
        }

        if (targetObject != null)
        {
            targetCollider = targetObject.GetComponent<Collider2D>();
            if (targetCollider == null)
            {
                Debug.LogError("目标物体缺少 Collider2D 组件！");
            }
        }
        else
        {
            Debug.LogError("未指定目标物体！");
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogError("未指定需要激活的物体！");
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
                Debug.LogWarning("需要激活的物体已被销毁或未设置！");
            }
        }

        isCountingDown = false;
        countdownCoroutine = null;
    }
}
