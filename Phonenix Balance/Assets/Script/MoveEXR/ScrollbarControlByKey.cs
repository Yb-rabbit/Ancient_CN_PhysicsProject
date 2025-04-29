using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarControlByKey : MonoBehaviour
{
    public Scrollbar scrollbar; // ��UI���Scrollbar���
    public int numberOfSteps = 4; // Scrollbar�Ĳ�����Ĭ��Ϊ4�����������ϡ�����

    void Start()
    {
        if (scrollbar == null)
        {
            Debug.LogError("Scrollbar���δ�󶨣�");
            return;
        }

        // ����Scrollbar�Ĳ���
        scrollbar.numberOfSteps = numberOfSteps;
    }

    void Update()
    {
        // ����������
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetScrollbarValue(0); // ��
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetScrollbarValue(1); // ��
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetScrollbarValue(2); // ��
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetScrollbarValue(3); // ��
        }
    }

    private void SetScrollbarValue(int stepIndex)
    {
        if (numberOfSteps > 0)
        {
            // ����Scrollbar��ֵ
            float value = (float)stepIndex / (numberOfSteps - 1);
            scrollbar.value = value;
        }
    }
}
