using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Point : MonoBehaviour
{
    // 目标物体
    public Transform target;

    // 是否启用位置同步
    public bool enableFollow = true;

    // 平滑移动速度
    public float smoothSpeed = 5f;

    // 最大偏离半径
    public float maxRadius = 10f;

    // 当前速度（由 SmoothDamp 动态调整）
    private Vector3 currentVelocity = Vector3.zero;

    // 上一次目标位置
    private Vector3 lastTargetPosition;

    // 是否已经记录了未指定目标的警告
    private bool hasLoggedWarning = false;

    // 刚体组件（用于清除动能）
    private Rigidbody2D rb;

    void Start()
    {
        if (target != null)
        {
            // 初始化上一次目标位置
            lastTargetPosition = target.position;
        }

        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (enableFollow && target != null)
        {
            FollowTarget();
            hasLoggedWarning = false; // 重置警告标志
        }
        else if (target == null && !hasLoggedWarning)
        {
            Debug.LogWarning("未指定目标物体！");
            hasLoggedWarning = true; // 避免重复输出警告
        }
    }

    private void FollowTarget()
    {
        // 检测目标位置是否发生变化
        if (target.position != lastTargetPosition)
        {
            // 计算目标位置与当前物体位置的方向和距离
            Vector3 directionToTarget = target.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            // 如果距离超过最大半径，立刻追踪目标位置并清除动能
            if (distanceToTarget > maxRadius)
            {
                // 直接将物体位置设置为目标位置，但保持 Z 值不变
                transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

                // 清除动能
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }
            }
            else
            {
                // 在限制范围内，使用 SmoothDamp 平滑移动到目标位置，但保持 Z 值不变
                Vector3 targetPosition = Vector3.SmoothDamp(
                    transform.position,
                    target.position,
                    ref currentVelocity,
                    1f / smoothSpeed // 平滑时间，值越小移动越快
                );

                // 保持 Z 值不变
                transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            }

            // 更新上一次目标位置
            lastTargetPosition = target.position;
        }
    }
}
