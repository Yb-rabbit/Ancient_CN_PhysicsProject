using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("�ؿ���ť����")]
    public GameObject buttonPrefab; // ��ť��Ԥ����
    public Transform buttonContainer; // ��ť�ĸ�����
    public int totalLevels = 4; // �ܹؿ���

    void Start()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            // ��̬���ɰ�ť
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // ���ð�ť�ϵ�����
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Level " + i;
            }

            // ���ؿ��Ƿ����
            bool isUnlocked = PlayerPrefs.GetInt("Level" + i, 0) == 1;
            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.interactable = isUnlocked;

                // Ϊ��ť��ӵ���¼�
                int levelIndex = i; // ����հ�����
                buttonComponent.onClick.AddListener(() => LoadLevel(levelIndex));
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        // ����ָ���ؿ�
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void UnlockLevel(int levelIndex)
    {
        // �����ؿ�
        PlayerPrefs.SetInt("Level" + levelIndex, 1);
    }
}
