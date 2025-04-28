using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Vector3 centerPosition = Vector3.zero; // ��ת���ĵ�
    public float radius = 5f; // ��ת�뾶
    public float speed = 60f; // ÿ����ת�ĽǶ�

    private Vector3 r; // �����ĵ㵽����ĳ�ʼ����

    void Awake()
    {
        r = transform.position - centerPosition; // ��ʼ������
    }

    void Update()
    {
        // ÿ֡��ת����
        r = Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward) * r;
        // ���������λ��
        transform.position = centerPosition + r;
    }
}