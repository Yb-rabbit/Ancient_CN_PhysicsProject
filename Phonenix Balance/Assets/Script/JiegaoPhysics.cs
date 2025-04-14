using UnityEngine;

public class JiegaoPhysics : MonoBehaviour
{
    [Header("�ϷŰ�")]
    [Tooltip("���ܸ˶����ϵ�����")]
    public Rigidbody2D leverArm;  // �ܸ˱۶���

    [Header("��������")]
    [Range(10, 60)]
    public float swingAngle = 30f; // ���ưڶ�����

    [Tooltip("�ָ���ʼ״̬���ٶ�")]
    public float recoverySpeed = 2f;

    private new HingeJoint2D hingeJoint;
    private Quaternion initialRotation; // ��ʼ��ת

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

        // ��¼��ʼ��ת
        initialRotation = leverArm.transform.rotation;

        // ʼ������ HingeJoint2D
        hingeJoint.enabled = true;
    }

    void Update()
    {
        // �ܸ�ʼ�ձ��ְڶ�����������ײ
        leverArm.transform.rotation = Quaternion.Lerp(
            leverArm.transform.rotation,
            initialRotation,
            recoverySpeed * Time.deltaTime
        );
    }
}
