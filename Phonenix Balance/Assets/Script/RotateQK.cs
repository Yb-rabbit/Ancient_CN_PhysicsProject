using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateQK : MonoBehaviour
{
    public Scrollbar scrollbar; // ��UI���Scrollbar���
    public Transform targetObject; // ��Ҫ��ת�ķ�UI������
    public float rotationSpeed = 2.0f; // ��ת�ٶ�

    private float targetAngle = 0f; // Ŀ��Ƕ�
    private float currentAngle = 0f; // ��ǰ�Ƕ�

    void Start()
    {
        if (scrollbar == null)
        {
            Debug.LogError("Scrollbar���δ�󶨣�");
            return;
        }

        if (targetObject == null)
        {
            Debug.LogError("Ŀ������δ�󶨣�");
            return;
        }

        // ���� Scrollbar �� OnValueChanged �¼�
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    void Update()
    {
        // ƽ����ת�����ٶȻ��뻺����
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        targetObject.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void OnScrollbarValueChanged(float value)
    {
        // ��ȡ Scrollbar �� numberOfSteps
        int numberOfSteps = scrollbar.numberOfSteps;

        // ����Ŀ��Ƕȣ���=0�㣬��=90�㣬��=180�㣬��=270�㣩
        if (numberOfSteps > 0)
        {
            int stepIndex = Mathf.RoundToInt(value * (numberOfSteps - 1));
            targetAngle = stepIndex * (360f / numberOfSteps);
        }
    }
}
