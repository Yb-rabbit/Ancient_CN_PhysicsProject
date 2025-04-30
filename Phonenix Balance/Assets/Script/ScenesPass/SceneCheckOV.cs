using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneCheckOV : MonoBehaviour
{
    public List<string> requiredScenes = new List<string>(); // 必须访问的场景列表
    public string targetScene; // 当所有场景访问完成后跳转的目标场景
    public Image transitionImage; // 用于展示的图片（UI Image）
    public float fadeDuration = 1.0f; // 图片渐变时间
    public float delayBeforeLoad = 2.0f; // 跳转前的延迟时间

    private HashSet<string> visitedScenes = new HashSet<string>(); // 已访问的场景集合

    void Start()
    {
        if (requiredScenes == null || requiredScenes.Count == 0)
        {
            Debug.LogError("requiredScenes list is empty. Please populate it in the Inspector.");
            return;
        }

        if (string.IsNullOrEmpty(targetScene))
        {
            Debug.LogError("Target scene is not set. Please specify it in the Inspector.");
        }

        // 确保当前对象在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);

        if (transitionImage != null)
        {
            DontDestroyOnLoad(transitionImage.gameObject); // 确保图片在场景切换时保留
            transitionImage.gameObject.SetActive(false); // 确保图片初始状态为隐藏
        }

        // 自动标记当前场景为已访问
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (requiredScenes.Contains(currentSceneName))
        {
            visitedScenes.Add(currentSceneName);
            Debug.Log($"Current scene '{currentSceneName}' automatically marked as visited.");
        }
    }

    // 记录场景访问
    public void MarkSceneAsVisited(string sceneName)
    {
        if (requiredScenes.Contains(sceneName))
        {
            visitedScenes.Add(sceneName);
            Debug.Log($"Scene '{sceneName}' marked as visited.");
        }
        else
        {
            Debug.LogWarning($"Scene '{sceneName}' is not in the requiredScenes list.");
        }
    }

    // 检查是否可以跳转到目标场景
    public void TryLoadTargetScene()
    {
        if (AllRequiredScenesVisited())
        {
            Debug.Log($"All required scenes visited. Preparing to load target scene: {targetScene}");
            StartCoroutine(ShowImageAndLoadScene());
        }
        else
        {
            Debug.Log("Not all required scenes have been visited. Cannot load target scene.");
        }
    }

    // 显示图片并延迟跳转场景
    private IEnumerator ShowImageAndLoadScene()
    {
        if (transitionImage != null)
        {
            transitionImage.gameObject.SetActive(true); // 显示图片
            yield return StartCoroutine(FadeImage(0, 1, fadeDuration)); // 渐变显示图片
        }

        yield return new WaitForSeconds(delayBeforeLoad); // 等待指定时间

        SceneManager.LoadScene(targetScene); // 跳转到目标场景
    }

    // 图片渐变效果
    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        if (transitionImage == null)
        {
            yield break;
        }

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

    // 检查是否所有指定场景都已访问
    private bool AllRequiredScenesVisited()
    {
        foreach (string scene in requiredScenes)
        {
            if (!visitedScenes.Contains(scene))
            {
                return false;
            }
        }
        return true;
    }
}
