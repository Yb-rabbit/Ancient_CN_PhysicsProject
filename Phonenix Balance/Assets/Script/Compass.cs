using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform playerTransform; // ���
    public RectTransform compassUI;   // ָ����

    private void Update()
    {
        if (playerTransform != null && compassUI != null)
        {
            // ��ҵ�ǰ������
            Vector2 playerForward = -playerTransform.up;

            // ������ҳ���ĽǶ�
            float angle = Mathf.Atan2(playerForward.y, playerForward.x) * Mathf.Rad2Deg;

            // ����ָ���� UI ����ת
            compassUI.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        }
    }
}
