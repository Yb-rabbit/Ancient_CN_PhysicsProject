using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeavy : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float maxMass = 10f; // 最大质量
    public float minMass = 1f;  // 最小质量
    public float midMass = 5f;  // 中间质量

    // 指定需要检测的物体
    public GameObject massAObject; //水第一层
    public GameObject massBObject; //激活点
    public GameObject massCObject; //凤衡点

    private void Start()
    {
        // 获取刚体2D组件
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("未找到Rigidbody2D组件！");
        }

        // 检查目标物体是否已分配
        if (massAObject == null || massBObject == null || massCObject == null)
        {
            Debug.LogError("目标物体未正确分配！");
        }
    }

    private void Update()
    {
        // 如果 C 物体处于激活状态，直接将质量设置为中间值
        if (massCObject != null && massCObject.activeInHierarchy)
        {
            rb2D.mass = midMass;
        }
        // 如果 B 物体处于激活状态，直接将质量设置为最大
        else if (massBObject != null && massBObject.activeInHierarchy)
        {
            rb2D.mass = maxMass;
        }
        // 如果 A 物体处于激活状态，直接将质量设置为最小
        else if (massAObject != null && massAObject.activeInHierarchy)
        {
            rb2D.mass = minMass;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 检测是否与 A 或 B 物体碰撞
        if (collision.gameObject == massAObject && (massAObject == null || !massAObject.activeInHierarchy))
        {
            rb2D.mass = minMass; // 直接设置为最小质量
        }
        else if (collision.gameObject == massBObject && (massBObject == null || !massBObject.activeInHierarchy))
        {
            rb2D.mass = maxMass; // 直接设置为最大质量
        }
    }
}
