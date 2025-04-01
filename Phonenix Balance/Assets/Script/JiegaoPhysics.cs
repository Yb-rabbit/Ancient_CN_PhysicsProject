using UnityEngine;

public class JiegaoPhysics : MonoBehaviour
{
    [Header("拖放绑定")]
    [Tooltip("将杠杆对象拖到这里")]
    public Rigidbody2D leverArm;  // 杠杆臂对象，改为 Rigidbody2D

    [Header("参数设置")]
    [Range(10, 60)]
    public float swingAngle = 30f; // 控制摆动幅度

    [Space]
    public float jumpForce = 3f;   // 弹跳力度

    private new HingeJoint2D hingeJoint;

    void Start()
    {
        // 确保杠杆对象有 HingeJoint2D 组件
        hingeJoint = leverArm.GetComponent<HingeJoint2D>();
        if (hingeJoint == null)
        {
            hingeJoint = leverArm.gameObject.AddComponent<HingeJoint2D>();
        }

        // 设置 HingeJoint2D 的属性
        hingeJoint.useLimits = true;
        JointAngleLimits2D limits = hingeJoint.limits;
        limits.min = -swingAngle;
        limits.max = swingAngle;
        hingeJoint.limits = limits;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        // 当有物体碰撞时，施加力
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            Vector2 force = collision.relativeVelocity * rb.mass;
            leverArm.AddForceAtPosition(force, collision.contacts[0].point);
        }
    }
}
