using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeavy : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float maxMass = 10f; // �������
    public float minMass = 1f;  // ��С����
    public float midMass = 5f;  // �м�����

    // ָ����Ҫ��������
    public GameObject massAObject; //ˮ��һ��
    public GameObject massBObject; //�����
    public GameObject massCObject; //����

    private void Start()
    {
        // ��ȡ����2D���
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("δ�ҵ�Rigidbody2D�����");
        }

        // ���Ŀ�������Ƿ��ѷ���
        if (massAObject == null || massBObject == null || massCObject == null)
        {
            Debug.LogError("Ŀ������δ��ȷ���䣡");
        }
    }

    private void Update()
    {
        // ��� C ���崦�ڼ���״̬��ֱ�ӽ���������Ϊ�м�ֵ
        if (massCObject != null && massCObject.activeInHierarchy)
        {
            rb2D.mass = midMass;
        }
        // ��� B ���崦�ڼ���״̬��ֱ�ӽ���������Ϊ���
        else if (massBObject != null && massBObject.activeInHierarchy)
        {
            rb2D.mass = maxMass;
        }
        // ��� A ���崦�ڼ���״̬��ֱ�ӽ���������Ϊ��С
        else if (massAObject != null && massAObject.activeInHierarchy)
        {
            rb2D.mass = minMass;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ����Ƿ��� A �� B ������ײ
        if (collision.gameObject == massAObject && (massAObject == null || !massAObject.activeInHierarchy))
        {
            rb2D.mass = minMass; // ֱ������Ϊ��С����
        }
        else if (collision.gameObject == massBObject && (massBObject == null || !massBObject.activeInHierarchy))
        {
            rb2D.mass = maxMass; // ֱ������Ϊ�������
        }
    }
}
