using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBHit : MonoBehaviour
{
    public GameObject targetObject; // 碰撞检测的目标物体
    public float moveDistance = 1.0f; // 移动的距离
    public float moveDuration = 0.5f; // 平滑移动的时间
    public int maxCollisionCount = 3; // 最大碰撞次数
    public List<GameObject> objectsToActivate; // 碰撞次数达到最大值时激活的物体列表

    private Vector3 originalPosition; // 原始位置
    private int collisionCount = 0; // 当前碰撞次数
    private bool isMoving = false; // 是否正在移动

    void Start()
    {
        // 记录物体的原始位置
        originalPosition = transform.position;

        // 检查是否设置了目标物体
        if (targetObject == null)
        {
            Debug.LogWarning("Target object is not assigned.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == targetObject && !isMoving)
        {
            collisionCount++;
            if (collisionCount >= maxCollisionCount)
            {
                ActivateObjects();
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(HandleMovement());
            }
        }
    }

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null && !obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
    }

    private IEnumerator HandleMovement()
    {
        isMoving = true;

        // 随机选择一个方向：左或右
        Vector3 randomDirection = GetRandomDirection();

        // 平滑移动到目标位置
        yield return MoveToPosition(originalPosition + randomDirection * moveDistance, moveDuration);

        // 平滑移动回原始位置
        yield return MoveToPosition(originalPosition, moveDuration);

        isMoving = false;
    }

    // 获取随机方向（左或右）
    private Vector3 GetRandomDirection()
    {
        return Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
