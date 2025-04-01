using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_A_Mouse : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标位置
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // 检测鼠标是否点击在当前物体上
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (collider != null && collider.transform == transform)
            {
                isDragging = true;
                offset = transform.position - mousePosition;
            }
        }

        // 检测鼠标左键松开
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 拖动物体
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }
}
