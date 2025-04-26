using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveX : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar; // ��UI���Scrollbar���
    [SerializeField] private Transform targetObject; // ��Ҫ�ƶ��ķ�UI������
    [SerializeField] private float maxMoveDistance = 400f; // ÿ���ƶ���������
    [SerializeField] private float smoothSpeed = 5f; // ƽ���ƶ��ٶ�

    private float initialX; // ��ʼ��xֵ
    private float targetX; // Ŀ���xֵ

    void Start()
    {
        // ����Ҫ����Ƿ��
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

        // ��ʼ����ʼxֵ
        initialX = targetObject.position.x;
        targetX = initialX;

        // ���� Scrollbar �� numberOfSteps Ϊ 3
        scrollbar.numberOfSteps = 3;

        // ���� Scrollbar �� OnValueChanged �¼�
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    void Update()
    {
        // ƽ���ƶ�Ŀ�������xֵ
        Vector3 position = targetObject.position;
        position.x = Mathf.Lerp(position.x, targetX, smoothSpeed * Time.deltaTime);
        targetObject.position = position;
    }

    private void OnScrollbarValueChanged(float value)
    {
        // ��ȡ��ǰ step
        int step = Mathf.RoundToInt(value * (scrollbar.numberOfSteps - 1));

        switch (step)
        {
            case 0: // ��һ�� step�������ƶ�
                targetX = Mathf.Max(initialX - maxMoveDistance, initialX - 400f);
                break;

            case 1: // �ڶ��� step���ص���ʼλ��
                targetX = initialX;
                break;

            case 2: // ������ step�������ƶ������Ӷ������
                float basePosition = Mathf.Min(initialX + maxMoveDistance, initialX + 400f);
                targetX = basePosition + 2f; // �ڵ����� step �Ļ���������2f
                break;

            default:
                Debug.LogWarning("δ�����stepֵ��" + step);
                break;
        }
    }

    void OnDestroy()
    {
        // �Ƴ��¼�������
        scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
    }
}
