using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionWithObject : MonoBehaviour
{
    public string targetScene; // 目标场景名称
    public Image transitionImage; // 用于过渡的图片
    public GameObject targetObject; // 包含按钮的目标物体（图片及其按钮）
    public Button buttonA; // 按钮 A
    public Button buttonB; // 按钮 B
    public float fadeDuration = 1.0f; // 图片渐变时间
    public float delayBeforeLoad = 0.5f; // 跳转前的延迟时间

    private Coroutine transitionCoroutine; // 用于跟踪当前的协程

    void Start()
    {
        // 检查是否正确设置了按钮和目标物体
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned. Please assign it in the Inspector.");
            return;
        }

        if (transitionImage == null)
        {
            Debug.LogError("Transition image is not assigned. Please assign it in the Inspector.");
            return;
        }

        if (buttonA == null || buttonB == null)
        {
            Debug.LogError("Buttons A and B are not assigned. Please assign them in the Inspector.");
            return;
        }

        // 绑定按钮事件
        buttonA.onClick.AddListener(OnButtonAClicked);
        buttonB.onClick.AddListener(OnButtonBClicked);

        // 确保目标物体初始状态为未激活
        targetObject.SetActive(false);
    }

    // 激活目标物体（例如通过其他按钮触发）
    public void ActivateTargetObject()
    {
        targetObject.SetActive(true);
    }

    // 按下按钮 A 时启动协程
    private void OnButtonAClicked()
    {
        if (transitionCoroutine == null)
        {
            transitionCoroutine = StartCoroutine(PerformSceneTransition());
        }
    }

    // 按下按钮 B 时将目标物体设为未激活状态
    private void OnButtonBClicked()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
            transitionCoroutine = null;
        }

        targetObject.SetActive(false);
    }

    // 场景过渡协程
    private IEnumerator PerformSceneTransition()
    {
        // 确保图片可见
        transitionImage.gameObject.SetActive(true);

        // 渐变显示图片
        yield return StartCoroutine(FadeImage(0, 1, fadeDuration));

        // 等待指定时间
        yield return new WaitForSeconds(delayBeforeLoad);

        // 跳转到目标场景
        SceneManager.LoadScene(targetScene);
    }

    // 图片渐变效果
    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        Color color = transitionImage.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            transitionImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        transitionImage.color = color;
    }
}
