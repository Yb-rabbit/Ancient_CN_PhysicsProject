using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateQK : MonoBehaviour
{
    public Scrollbar scrollbar; // 绑定UI层的Scrollbar组件
    public Transform targetObject; // 需要旋转的非UI层物体
    public float rotationSpeed = 2.0f; // 旋转速度

    private float targetAngle = 0f; // 目标角度
    private float currentAngle = 0f; // 当前角度

    void Start()
    {
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

        // 监听 Scrollbar 的 OnValueChanged 事件
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    void Update()
    {
        // 平滑旋转（加速度缓入缓出）
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        targetObject.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void OnScrollbarValueChanged(float value)
    {
        // 获取 Scrollbar 的 numberOfSteps
        int numberOfSteps = scrollbar.numberOfSteps;

        // 计算目标角度（北=0°，西=90°，南=180°，东=270°）
        if (numberOfSteps > 0)
        {
            int stepIndex = Mathf.RoundToInt(value * (numberOfSteps - 1));
            targetAngle = stepIndex * (360f / numberOfSteps);
        }
    }
}
