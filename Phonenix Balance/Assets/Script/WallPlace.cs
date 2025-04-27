using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlace : MonoBehaviour
{
    // ����Ĭ����ɫ����ײʱ����ɫ
    public Color defaultColor = new Color(1, 1, 1, 1); // ��ɫ����ȫ��͸��
    public Color collisionColor = new Color(1, 0, 0, 1); // ��ɫ����ȫ��͸��

    // ָ����Ҫ������ײ����
    public GameObject targetObject;

    // �����ٶ�
    public float fadeOutSpeed = 2f;

    // ������Ⱦ��
    private SpriteRenderer spriteRenderer;

    // ��ǰĿ����ɫ
    private Color targetColor;

    void Start()
    {
        // ��ȡ��ǰ�����SpriteRenderer���
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ��ʼ��ΪĬ����ɫ
        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;
            targetColor = defaultColor;
        }
    }

    void Update()
    {
        // ƽ�����ɵ�Ŀ����ɫ
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, Time.deltaTime * fadeOutSpeed);
        }
    }

    // ��������ײ����봥����ʱ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�������Ƿ���ָ������
        if (collision.gameObject == targetObject && spriteRenderer != null)
        {
            // ��ײʱ������Ϊ��ײ��ɫ
            spriteRenderer.color = collisionColor;
            targetColor = collisionColor;
        }
    }

    // ��������ײ���˳�������ʱ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �����ײ�������Ƿ���ָ������
        if (collision.gameObject == targetObject && spriteRenderer != null)
        {
            // �뿪ʱĿ����ɫ����ΪĬ����ɫ��������
            targetColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0); // ����Ϊ͸��
        }
    }
}
