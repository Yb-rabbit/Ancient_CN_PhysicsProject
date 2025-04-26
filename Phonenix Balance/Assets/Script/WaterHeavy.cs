using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeavy : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float maxMass = 10f; // 最大质量
    public float minMass = 1f;  // 最小质量
    public float massChangeRate = 1f; // 质量变化速率

    private void Start()
    {
        // 获取刚体2D组件
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("未找到Rigidbody2D组件！");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 直接检测碰撞物体的名称
        if (collision.gameObject.name == "MassA")
        {
            rb2D.mass = Mathf.Min(rb2D.mass + massChangeRate * Time.deltaTime, maxMass);
        }
        else if (collision.gameObject.name == "MassB")
        {
            rb2D.mass = Mathf.Max(rb2D.mass - massChangeRate * Time.deltaTime, minMass);
        }
    }
}
