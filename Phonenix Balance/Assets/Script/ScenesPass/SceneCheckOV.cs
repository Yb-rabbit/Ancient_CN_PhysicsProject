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
    private static SceneCheckOV instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // �����ظ���ʵ��
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

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

    private IEnumerator ShowImageAndLoadScene()
    {
        if (transitionImage == null)
        {
            GameObject imageObject = GameObject.Find("TransitionImage");
            if (imageObject != null)
            {
                transitionImage = imageObject.GetComponent<Image>();
            }

            if (transitionImage == null)
            {
                Debug.LogWarning("Transition image not found. Creating a new one.");
                GameObject canvasObject = new GameObject("TransitionCanvas");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                DontDestroyOnLoad(canvasObject);

                GameObject imageObjectNew = new GameObject("TransitionImage");
                imageObjectNew.transform.SetParent(canvasObject.transform);
                transitionImage = imageObjectNew.AddComponent<Image>();
                transitionImage.color = new Color(0, 0, 0, 0); // ��ʼΪ͸��
                RectTransform rectTransform = transitionImage.GetComponent<RectTransform>();
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.sizeDelta = Vector2.zero;
            }
        }

        transitionImage.gameObject.SetActive(true);
        yield return StartCoroutine(FadeImage(0, 1, fadeDuration));

        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(targetScene);
    }

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
