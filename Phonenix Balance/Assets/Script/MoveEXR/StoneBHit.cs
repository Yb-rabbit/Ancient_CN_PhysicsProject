using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBHit : MonoBehaviour
{
    public GameObject targetObject; // ��ײ����Ŀ������
    public float moveDistance = 1.0f; // �ƶ��ľ���
    public float moveDuration = 0.5f; // ƽ���ƶ���ʱ��
    public int maxCollisionCount = 3; // �����ײ����
    public List<GameObject> objectsToActivate; // ��ײ�����ﵽ���ֵʱ����������б�

    private Vector3 originalPosition; // ԭʼλ��
    private int collisionCount = 0; // ��ǰ��ײ����
    private bool isMoving = false; // �Ƿ������ƶ�

    void Start()
    {
        // ��¼�����ԭʼλ��
        originalPosition = transform.position;

        // ����Ƿ�������Ŀ������
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

        // ���ѡ��һ�����������
        Vector3 randomDirection = GetRandomDirection();

        // ƽ���ƶ���Ŀ��λ��
        yield return MoveToPosition(originalPosition + randomDirection * moveDistance, moveDuration);

        // ƽ���ƶ���ԭʼλ��
        yield return MoveToPosition(originalPosition, moveDuration);

        isMoving = false;
    }

    // ��ȡ�����������ң�
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
