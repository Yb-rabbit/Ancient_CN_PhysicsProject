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

            // 禁用刚体状态
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void HandleMouseUp()
    {
        isDragging = false;

        // 重新启用刚体状态
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    private void DragObject()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        transform.position = mousePosition + offset;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 确保 z 坐标为 0
        return mousePosition;
    }
}
