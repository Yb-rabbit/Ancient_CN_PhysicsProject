using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform playerTransform; // 玩家
    public RectTransform compassUI;   // 指南针

    private void Update()
    {
        if (playerTransform != null && compassUI != null)
        {
            // 玩家的前向向量
            Vector2 playerForward = -playerTransform.up;

            // 计算玩家朝向的角度
            float angle = Mathf.Atan2(playerForward.y, playerForward.x) * Mathf.Rad2Deg;

            // 设置指南针 UI 的旋转
            compassUI.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        }
    }
}
