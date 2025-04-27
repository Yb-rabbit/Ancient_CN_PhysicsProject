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
    private GameObject defaultObjectToActivate;

    [SerializeField]
    private List<GameObject> additionalObjectsToActivate; // 激活多个物体

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

        // 默认激活物体初始化
        if (defaultObjectToActivate != null)
        {
            defaultObjectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogError("未指定默认需要激活的物体！");
        }

        // 初始化其他需要激活的物体
        if (additionalObjectsToActivate != null && additionalObjectsToActivate.Count > 0)
        {
            foreach (var obj in additionalObjectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
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

            // 激活默认物体
            if (defaultObjectToActivate != null)
            {
                defaultObjectToActivate.SetActive(true);
            }
            else
            {
                Debug.LogWarning("默认需要激活的物体已被销毁或未设置！");
            }

            // 激活其他物体
            if (additionalObjectsToActivate != null && additionalObjectsToActivate.Count > 0)
            {
                foreach (var obj in additionalObjectsToActivate)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }

        isCountingDown = false;
        countdownCoroutine = null;
    }
}
