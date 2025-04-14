using UnityEngine;

public class JiegaoPhysics : MonoBehaviour
{
    [Header("拖放绑定")]
    [Tooltip("将杠杆对象拖到这里")]
    public Rigidbody2D leverArm;  // 杠杆臂对象

    [Header("参数设置")]
    [Range(10, 60)]
    public float swingAngle = 30f; // 控制摆动幅度

    [Tooltip("恢复初始状态的速度")]
    public float recoverySpeed = 2f;

    private new HingeJoint2D hingeJoint;
    private Quaternion initialRotation; // 初始旋转

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

        // 记录初始旋转
        initialRotation = leverArm.transform.rotation;

        // 始终启用 HingeJoint2D
        hingeJoint.enabled = true;
    }

    void Update()
    {
        // 杠杆始终保持摆动，无需检测碰撞
        leverArm.transform.rotation = Quaternion.Lerp(
            leverArm.transform.rotation,
            initialRotation,
            recoverySpeed * Time.deltaTime
        );
    }
}
