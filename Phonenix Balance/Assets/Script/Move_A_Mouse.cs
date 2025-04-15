using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_A_Mouse : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb;
    private Camera mainCamera;

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
            HandleMouseDown();
        }

        // 检测鼠标左键松开
        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }

        // 拖动物体
        if (isDragging)
        {
            DragObject();
        }
    }

    private void HandleMouseDown()
    {
        // 获取鼠标位置
        Vector3 mousePosition = GetMouseWorldPosition();

        // 检测鼠标是否点击在当前物体上
        Collider2D collider = Physics2D.OverlapPoint(mousePosition);
        if (collider != null && collider.transform == transform)
        {
            isDragging = true;
            offset = transform.position - mousePosition;

            // 禁用刚体的重力，但保持物理模拟
            if (rb != null)
            {
                rb.gravityScale = 0;
            }
        }
    }

    private void HandleMouseUp()
    {
        isDragging = false;

        // 恢复刚体的重力
        if (rb != null)
        {
            rb.gravityScale = 1;
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
