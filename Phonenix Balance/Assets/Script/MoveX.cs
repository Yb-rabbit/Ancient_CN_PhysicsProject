using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveX : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar; // 绑定UI层的Scrollbar组件
    [SerializeField] private Transform targetObject; // 需要移动的非UI层物体
    [SerializeField] private float maxMoveDistance = 400f; // 每次移动的最大距离
    [SerializeField] private float smoothSpeed = 5f; // 平滑移动速度

    private float initialX; // 初始的x值
    private float targetX; // 目标的x值

    void Start()
    {
        // 检查必要组件是否绑定
        if (scrollbar == null)
        {
            Debug.LogError("Scrollbar组件未绑定！");
            return;
        }

        if (targetObject == null)
        {
            Debug.LogError("目标物体未绑定！");
            return;
        }

        // 初始化初始x值
        initialX = targetObject.position.x;
        targetX = initialX;

        // 设置 Scrollbar 的 numberOfSteps 为 3
        scrollbar.numberOfSteps = 3;

        // 监听 Scrollbar 的 OnValueChanged 事件
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    void Update()
    {
        // 平滑移动目标物体的x值
        Vector3 position = targetObject.position;
        position.x = Mathf.Lerp(position.x, targetX, smoothSpeed * Time.deltaTime);
        targetObject.position = position;
    }

    private void OnScrollbarValueChanged(float value)
    {
        // 获取当前 step
        int step = Mathf.RoundToInt(value * (scrollbar.numberOfSteps - 1));

        switch (step)
        {
            case 0: // 第一个 step：向左移动
                targetX = Mathf.Max(initialX - maxMoveDistance, initialX - 400f);
                break;

            case 1: // 第二个 step：回到初始位置
                targetX = initialX;
                break;

            case 2: // 第三个 step：向右移动并增加额外距离
                float basePosition = Mathf.Min(initialX + maxMoveDistance, initialX + 400f);
                targetX = basePosition + 2f; // 在第三个 step 的基础上增加2f
                break;

            default:
                Debug.LogWarning("未定义的step值：" + step);
                break;
        }
    }

    void OnDestroy()
    {
        // 移除事件监听器
        scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
    }
}
