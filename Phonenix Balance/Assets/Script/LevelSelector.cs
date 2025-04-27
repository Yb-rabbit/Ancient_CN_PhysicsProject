using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("关卡按钮设置")]
    public GameObject buttonPrefab; // 按钮的预制体
    public Transform buttonContainer; // 按钮的父容器
    public int totalLevels = 4; // 总关卡数

    void Start()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            // 动态生成按钮
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // 设置按钮上的文字
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Level " + i;
            }

            // 检查关卡是否解锁
            bool isUnlocked = PlayerPrefs.GetInt("Level" + i, 0) == 1;
            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.interactable = isUnlocked;

                // 为按钮添加点击事件
                int levelIndex = i; // 避免闭包问题
                buttonComponent.onClick.AddListener(() => LoadLevel(levelIndex));
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        // 加载指定关卡
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void UnlockLevel(int levelIndex)
    {
        // 解锁关卡
        PlayerPrefs.SetInt("Level" + levelIndex, 1);
    }
}
