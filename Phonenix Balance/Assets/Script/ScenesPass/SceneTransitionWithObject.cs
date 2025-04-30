using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionWithObject : MonoBehaviour
{
    public string targetScene; // Ŀ�곡������
    public Image transitionImage; // ���ڹ��ɵ�ͼƬ
    public GameObject targetObject; // ������ť��Ŀ�����壨ͼƬ���䰴ť��
    public Button buttonA; // ��ť A
    public Button buttonB; // ��ť B
    public float fadeDuration = 1.0f; // ͼƬ����ʱ��
    public float delayBeforeLoad = 0.5f; // ��תǰ���ӳ�ʱ��

    private Coroutine transitionCoroutine; // ���ڸ��ٵ�ǰ��Э��

    void Start()
    {
        // ����Ƿ���ȷ�����˰�ť��Ŀ������
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

        // �󶨰�ť�¼�
        buttonA.onClick.AddListener(OnButtonAClicked);
        buttonB.onClick.AddListener(OnButtonBClicked);

        // ȷ��Ŀ�������ʼ״̬Ϊδ����
        targetObject.SetActive(false);
    }

    // ����Ŀ�����壨����ͨ��������ť������
    public void ActivateTargetObject()
    {
        targetObject.SetActive(true);
    }

    // ���°�ť A ʱ����Э��
    private void OnButtonAClicked()
    {
        if (transitionCoroutine == null)
        {
            transitionCoroutine = StartCoroutine(PerformSceneTransition());
        }
    }

    // ���°�ť B ʱ��Ŀ��������Ϊδ����״̬
    private void OnButtonBClicked()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
            transitionCoroutine = null;
        }

        targetObject.SetActive(false);
    }

    // ��������Э��
    private IEnumerator PerformSceneTransition()
    {
        // ȷ��ͼƬ�ɼ�
        transitionImage.gameObject.SetActive(true);

        // ������ʾͼƬ
        yield return StartCoroutine(FadeImage(0, 1, fadeDuration));

        // �ȴ�ָ��ʱ��
        yield return new WaitForSeconds(delayBeforeLoad);

        // ��ת��Ŀ�곡��
        SceneManager.LoadScene(targetScene);
    }

    // ͼƬ����Ч��
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
