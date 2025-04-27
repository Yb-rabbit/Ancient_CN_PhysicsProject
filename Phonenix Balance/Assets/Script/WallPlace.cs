using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlace : MonoBehaviour
{
    // 设置默认颜色和碰撞时的颜色
    public Color defaultColor = new Color(1, 1, 1, 1); // 白色，完全不透明
    public Color collisionColor = new Color(1, 0, 0, 1); // 红色，完全不透明

    // 指定需要检测的碰撞对象
    public GameObject targetObject;

    // 淡出速度
    public float fadeOutSpeed = 2f;

    // 精灵渲染器
    private SpriteRenderer spriteRenderer;

    // 当前目标颜色
    private Color targetColor;

    void Start()
    {
        // 获取当前物体的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初始化为默认颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;
            targetColor = defaultColor;
        }
    }

    void Update()
    {
        // 平滑过渡到目标颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, Time.deltaTime * fadeOutSpeed);
        }
    }

    // 当其他碰撞体进入触发器时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是指定对象
        if (collision.gameObject == targetObject && spriteRenderer != null)
        {
            // 碰撞时立即变为碰撞颜色
            spriteRenderer.color = collisionColor;
            targetColor = collisionColor;
        }
    }

    // 当其他碰撞体退出触发器时调用
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 检查碰撞的物体是否是指定对象
        if (collision.gameObject == targetObject && spriteRenderer != null)
        {
            // 离开时目标颜色设置为默认颜色（淡出）
            targetColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0); // 设置为透明
        }
    }
}
