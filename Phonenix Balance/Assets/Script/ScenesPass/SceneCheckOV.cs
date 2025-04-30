using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneCheckOV : MonoBehaviour
{
    public List<string> requiredScenes = new List<string>(); // ������ʵĳ����б�
    public string targetScene; // �����г���������ɺ���ת��Ŀ�곡��
    public Image transitionImage; // ����չʾ��ͼƬ��UI Image��
    public float fadeDuration = 1.0f; // ͼƬ����ʱ��
    public float delayBeforeLoad = 2.0f; // ��תǰ���ӳ�ʱ��

    private HashSet<string> visitedScenes = new HashSet<string>(); // �ѷ��ʵĳ�������

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

        // ȷ����ǰ�����ڳ����л�ʱ��������
        DontDestroyOnLoad(gameObject);

        if (transitionImage != null)
        {
            DontDestroyOnLoad(transitionImage.gameObject); // ȷ��ͼƬ�ڳ����л�ʱ����
            transitionImage.gameObject.SetActive(false); // ȷ��ͼƬ��ʼ״̬Ϊ����
        }

        // �Զ���ǵ�ǰ����Ϊ�ѷ���
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (requiredScenes.Contains(currentSceneName))
        {
            visitedScenes.Add(currentSceneName);
            Debug.Log($"Current scene '{currentSceneName}' automatically marked as visited.");
        }
    }

    // ��¼��������
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

    // ����Ƿ������ת��Ŀ�곡��
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

    // ��ʾͼƬ���ӳ���ת����
    private IEnumerator ShowImageAndLoadScene()
    {
        if (transitionImage != null)
        {
            transitionImage.gameObject.SetActive(true); // ��ʾͼƬ
            yield return StartCoroutine(FadeImage(0, 1, fadeDuration)); // ������ʾͼƬ
        }

        yield return new WaitForSeconds(delayBeforeLoad); // �ȴ�ָ��ʱ��

        SceneManager.LoadScene(targetScene); // ��ת��Ŀ�곡��
    }

    // ͼƬ����Ч��
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

    // ����Ƿ�����ָ���������ѷ���
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
