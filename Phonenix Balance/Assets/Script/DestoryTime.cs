using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTime : MonoBehaviour
{
    // 指定的检测物体
    [SerializeField]
    private GameObject targetObject;

    // 倒计时的时间（秒）
    [SerializeField]
    private float countdownTime = 3f;

    // 倒计时结束后需要激活的物体
    [SerializeField]
    private GameObject objectToActivate;

    private Collider2D spriteMaskCollider; // 当前物体的精灵遮罩
    private Collider2D targetCollider; // 指定物体的碰撞器
    private bool isCountingDown = false; // 是否正在倒计时
    private bool isOverlapping = false; // 是否与目标物体有交集

    void Start()
    {
        // 获取当前物体的 Collider2D
        spriteMaskCollider = GetComponent<Collider2D>();
        if (spriteMaskCollider == null)
        {
            Debug.LogError("当前物体缺少 Collider2D 组件！");
        }

        // 获取目标物体的 Collider2D
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

        // 确保需要激活的物体已禁用
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
        // 持续检测当前物体的精灵遮罩是否与目标物体有交集
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

        // 等待倒计时结束
        yield return new WaitForSeconds(countdownTime);

        // 禁用当前物体
        gameObject.SetActive(false);

        // 激活指定的物体
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        isCountingDown = false;
    }
}
