using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    [Header("关卡设置")]
    public int currentLevel; // 当前关卡编号

    private void Start()
    {
        // 检查当前关卡是否已解锁
        if (PlayerPrefs.GetInt("Level" + currentLevel, 0) == 0)
        {
            // 如果未解锁，解锁当前关卡
            UnlockCurrentLevel();
        }
    }

    private void UnlockCurrentLevel()
    {
        // 解锁当前关卡
        PlayerPrefs.SetInt("Level" + currentLevel, 1);

        // 自动解锁下一关
        int nextLevel = currentLevel + 1;
        PlayerPrefs.SetInt("Level" + nextLevel, 1);

        // 保存解锁状态
        PlayerPrefs.Save();

        Debug.Log($"关卡 {currentLevel} 已解锁，关卡 {nextLevel} 已自动解锁！");
    }
}
