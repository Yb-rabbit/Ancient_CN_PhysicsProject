using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision_Move : MonoBehaviour
{
    private bool isOverlapping = false; // 是否与指定物体的精灵遮罩重合
    private bool isMoving = false; // 是否正在运动
    private Rigidbody2D targetRb; // 目标物体的刚体

    // 指定的精灵遮罩物体
    [SerializeField]
    private GameObject spriteMaskObject;

    // 需要控制运动的目标物体
    [SerializeField]
    private GameObject targetObject;

    // 运动速度
    [SerializeField]
    private float moveSpeed = 5f;

    // 减速系数
    [SerializeField]
    private float deceleration = 2f;

    private void Awake()
    {
        // 缓存目标物体的刚体引用
        if (targetObject != null)
        {
            targetRb = targetObject.GetComponent<Rigidbody2D>();
            if (targetRb == null)
            {
                Debug.LogError("目标物体缺少 Rigidbody2D 组件！");
            }
        }
        else
        {
            Debug.LogError("未指定目标物体！");
        }

        // 检查主摄像机是否存在
        if (Camera.main == null)
        {
            Debug.LogError("场景中没有主摄像机！");
        }
    }

    private void Update()
    {
        // 检测鼠标左键按下和松开
        if (isOverlapping)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMoving = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (targetRb == null) return;

        if (isMoving)
        {
            // 按前向方向移动目标物体
            Vector2 moveDirection = transform.up; // 2D 前向为 transform.up
            targetRb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // 逐渐停止运动
            targetRb.velocity = Vector2.Lerp(targetRb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查是否与指定的精灵遮罩物体重合
        if (collision.gameObject == spriteMaskObject)
        {
            isOverlapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 检查是否离开指定的精灵遮罩物体
        if (collision.gameObject == spriteMaskObject)
        {
            isOverlapping = false;
            isMoving = false; // 停止运动
        }
    }
}
