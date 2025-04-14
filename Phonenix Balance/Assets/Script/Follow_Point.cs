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

    // 上一次目标位置
    private Vector3 lastTargetPosition;

    void Start()
    {
        if (target != null)
        {
            // 初始化上一次目标位置
            lastTargetPosition = target.position;
        }
    }

    void Update()
    {
        if (enableFollow && target != null)
        {
            // 检测目标位置是否发生变化
            if (target.position != lastTargetPosition)
            {
                // 平滑移动到目标位置
                transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);

                // 更新上一次目标位置
                lastTargetPosition = target.position;
            }
        }
        else if (target == null)
        {
            Debug.LogWarning("未指定目标物体！");
        }
    }
}
