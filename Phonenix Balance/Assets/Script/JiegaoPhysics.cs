using UnityEngine;

public class JiegaoPhysics : MonoBehaviour
{
    [Header("�ϷŰ�")]
    [Tooltip("���ܸ˶����ϵ�����")]
    public Rigidbody2D leverArm;  // �ܸ˱۶��󣬸�Ϊ Rigidbody2D

    [Header("��������")]
    [Range(10, 60)]
    public float swingAngle = 30f; // ���ưڶ�����

    [Space]
    public float jumpForce = 3f;   // ��������

    private new HingeJoint2D hingeJoint;

    void Start()
    {
        // ȷ���ܸ˶����� HingeJoint2D ���
        hingeJoint = leverArm.GetComponent<HingeJoint2D>();
        if (hingeJoint == null)
        {
            hingeJoint = leverArm.gameObject.AddComponent<HingeJoint2D>();
        }

        // ���� HingeJoint2D ������
        hingeJoint.useLimits = true;
        JointAngleLimits2D limits = hingeJoint.limits;
        limits.min = -swingAngle;
        limits.max = swingAngle;
        hingeJoint.limits = limits;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        // ����������ײʱ��ʩ����
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            Vector2 force = collision.relativeVelocity * rb.mass;
            leverArm.AddForceAtPosition(force, collision.contacts[0].point);
        }
    }
}
