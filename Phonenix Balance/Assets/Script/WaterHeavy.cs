using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeavy : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float maxMass = 10f; // 最大质量
    public float minMass = 1f;  // 最小质量

    // 指定需要检测的物体
    public GameObject massAObject;
    public GameObject massBObject;

    private void Start()
    {
        // 获取刚体2D组件
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("未找到Rigidbody2D组件！");
        }

        // 检查目标物体是否已分配
        if (massAObject == null || massBObject == null)
        {
            Debug.LogError("目标物体未正确分配！");
        }
    }

    private void Update()
    {
        // 根据激活状态直接设置质量
        if (massBObject != null && massBObject.activeInHierarchy)
        {
            rb2D.mass = maxMass;
        }
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
