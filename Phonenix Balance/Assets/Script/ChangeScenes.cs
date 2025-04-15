using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeScenes : MonoBehaviour
{
    public Button changeSceneButton; // ��ť����
#if UNITY_EDITOR
    public SceneAsset targetScene; // Ŀ�곡������
#endif
    public string targetSceneName; // Ŀ�곡������

    public Image displayImage; // ��Ҫչʾ��ͼƬ
    public float fadeDuration = 1f; // ���뵭����ʱ�䣨�룩
    public float displayDuration = 3f; // ͼƬչʾ��ʱ�䣨�룩

    void Start()
    {
#if UNITY_EDITOR
        if (targetScene != null)
        {
            targetSceneName = targetScene.name;
        }
#endif

        if (changeSceneButton != null)
        {
            changeSceneButton.onClick.AddListener(ActivateImageAndChangeScene);
        }
        else
        {
            Debug.LogError("Change Scene Button is not assigned!");
        }

        // ȷ��ͼƬ��ʼ״̬Ϊ����
        if (displayImage != null)
        {
            Color color = displayImage.color;
            color.a = 0f; // ����͸����Ϊ 0
            displayImage.color = color;
            displayImage.gameObject.SetActive(false);
        }
    }

    // ����ͼƬ���ӳ���ת����
    public void ActivateImageAndChangeScene()
    {
        if (displayImage != null)
        {
            displayImage.gameObject.SetActive(true); // ����ͼƬ
            StartCoroutine(FadeInAndChangeScene()); // ��ʼ���벢��ת����
        }
        else
        {
            Debug.LogError("Display Image is not assigned!");
        }
    }

    // Э�����õ���Ч������ת����
    private IEnumerator FadeInAndChangeScene()
    {
        // ����ͼƬ
        yield return StartCoroutine(FadeImage(0f, 1f, fadeDuration));

        // �ȴ�չʾʱ��
        yield return new WaitForSeconds(displayDuration);

        // ��ת����
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            if (Application.CanStreamedLevelBeLoaded(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.LogError($"Scene '{targetSceneName}' cannot be loaded. Please check the scene name and ensure it is added to the build settings.");
            }
        }
        else
        {
            Debug.LogError("Target scene name is null or empty!");
        }
    }

    // Э�̣�����ͼƬ͸���ȵĽ���
    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = displayImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            displayImage.color = color;
            yield return null;
        }

        // ȷ������͸���Ⱦ�ȷ����
        color.a = endAlpha;
        displayImage.color = color;
    }
}
