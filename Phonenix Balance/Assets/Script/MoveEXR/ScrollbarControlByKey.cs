using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarControlByKey : MonoBehaviour
{
    public Scrollbar scrollbar; // 绑定UI层的Scrollbar组件
    public int numberOfSteps = 4; // Scrollbar的步数（默认为4：北、东、南、西）

    void Start()
    {
        if (scrollbar == null)
        {
            Debug.LogError("Scrollbar组件未绑定！");
            return;
        }

        // 设置Scrollbar的步数
        scrollbar.numberOfSteps = numberOfSteps;
    }

    void Update()
    {
        // 检测键盘输入
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetScrollbarValue(0); // 北
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetScrollbarValue(1); // 西
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetScrollbarValue(2); // 南
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetScrollbarValue(3); // 东
        }
    }

    private void SetScrollbarValue(int stepIndex)
    {
        if (numberOfSteps > 0)
        {
            // 计算Scrollbar的值
            float value = (float)stepIndex / (numberOfSteps - 1);
            scrollbar.value = value;
        }
    }
}
