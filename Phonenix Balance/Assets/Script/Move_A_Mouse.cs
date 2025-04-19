using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_A_Mouse : MonoBehaviour
{
    private bool isDragging = false; // 是否正在拖拽
    private Vector3 offset; // 鼠标与物体的偏移量
    private Rigidbody2D rb; // 刚体组件
    private Camera mainCamera; // 主摄像机

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // 缓存主摄像机引用，避免每帧调用 Camera.main
    }

    void Update()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            ToggleDragging();
        }

        // 拖动物体
        if (isDragging)
        {
            DragObject();
        }
    }

    private void ToggleDragging()
    {
        // 切换拖拽状态
        isDragging = !isDragging;

        if (isDragging)
        {
            // 开始拖拽时，计算鼠标与物体的偏移量
            Vector3 mousePosition = GetMouseWorldPosition();
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            if (collider != null && collider.transform == transform)
            {
                offset = transform.position - mousePosition;

                // 禁用刚体的重力，但保持物理模拟
                if (rb != null)
                {
                    rb.gravityScale = 0;
                }
            }
            else
            {
                // 如果鼠标未点击在物体上，取消拖拽
                isDragging = false;
            }
        }
        else
        {
            // 停止拖拽时，恢复刚体的重力
            if (rb != null)
            {
                rb.gravityScale = 1;
            }
        }
    }

    private void DragObject()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 targetPosition = mousePosition + offset;

        // 使用 Rigidbody2D 的 MovePosition 方法移动物体
        if (rb != null)
        {
            rb.MovePosition(targetPosition);
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 确保 z 坐标为 0
        return mousePosition;
    }
}
