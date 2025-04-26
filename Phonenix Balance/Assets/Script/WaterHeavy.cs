using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeavy : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float maxMass = 10f; // �������
    public float minMass = 1f;  // ��С����
    public float massChangeRate = 1f; // �����仯����

    private void Start()
    {
        // ��ȡ����2D���
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("δ�ҵ�Rigidbody2D�����");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // ֱ�Ӽ����ײ���������
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
