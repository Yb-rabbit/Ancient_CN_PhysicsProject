using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeScenes : MonoBehaviour
{
    public Button changeSceneButton; // 按钮引用
#if UNITY_EDITOR
    public SceneAsset targetScene; // 目标场景引用
#endif
    public string targetSceneName; // 目标场景名称

    public Image displayImage; // 需要展示的图片
    public float fadeDuration = 1f; // 淡入淡出的时间（秒）
    public float displayDuration = 3f; // 图片展示的时间（秒）

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

        // 确保图片初始状态为隐藏
        if (displayImage != null)
        {
            Color color = displayImage.color;
            color.a = 0f; // 设置透明度为 0
            displayImage.color = color;
            displayImage.gameObject.SetActive(false);
        }
    }

    // 激活图片并延迟跳转场景
    public void ActivateImageAndChangeScene()
    {
        if (displayImage != null)
        {
            displayImage.gameObject.SetActive(true); // 激活图片
            StartCoroutine(FadeInAndChangeScene()); // 开始淡入并跳转场景
        }
        else
        {
            Debug.LogError("Display Image is not assigned!");
        }
    }

    // 协程启用淡入效果并跳转场景
    private IEnumerator FadeInAndChangeScene()
    {
        // 淡入图片
        yield return StartCoroutine(FadeImage(0f, 1f, fadeDuration));

        // 等待展示时间
        yield return new WaitForSeconds(displayDuration);

        // 跳转场景
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

    // 协程：控制图片透明度的渐变
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

        // 确保最终透明度精确设置
        color.a = endAlpha;
        displayImage.color = color;
    }
}
