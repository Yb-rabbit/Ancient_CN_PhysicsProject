using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    [Header("�ؿ�����")]
    public int currentLevel; // ��ǰ�ؿ����

    private void Start()
    {
        // ��鵱ǰ�ؿ��Ƿ��ѽ���
        if (PlayerPrefs.GetInt("Level" + currentLevel, 0) == 0)
        {
            // ���δ������������ǰ�ؿ�
            UnlockCurrentLevel();
        }
    }

    private void UnlockCurrentLevel()
    {
        // ������ǰ�ؿ�
        PlayerPrefs.SetInt("Level" + currentLevel, 1);

        // �Զ�������һ��
        int nextLevel = currentLevel + 1;
        PlayerPrefs.SetInt("Level" + nextLevel, 1);

        // �������״̬
        PlayerPrefs.Save();

        Debug.Log($"�ؿ� {currentLevel} �ѽ������ؿ� {nextLevel} ���Զ�������");
    }
}
