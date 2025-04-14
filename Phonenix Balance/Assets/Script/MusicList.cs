using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicList : MonoBehaviour
{
    [Header("��Ƶ����")]
    public List<AudioClip> musicClips; // ��Ƶ�����б�
    public List<AudioMixerGroup> mixerGroups; // ����������б�
    public float delayInSeconds = 5f; // �ӳ�����

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool firstClipPlayed = false; // ��־λ���ж��Ƿ��Ѿ������˵�һ����Ƶ

    void Start()
    {
        if (musicClips == null || musicClips.Count == 0)
        {
            Debug.LogWarning("��Ƶ�����б�Ϊ�գ��޷��������֣�");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(PlayFirstClipWithDelay());
    }

    void Update()
    {
        if (firstClipPlayed && !audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }


    IEnumerator PlayFirstClipWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        PlayCurrentClip();
        firstClipPlayed = true;
    }

    private void PlayCurrentClip()
    {
        if (currentClipIndex >= musicClips.Count)
        {
            Debug.LogError("��ǰ��Ƶ����������Χ��");
            return;
        }

        audioSource.clip = musicClips[currentClipIndex];
        if (currentClipIndex < mixerGroups.Count)
        {
            audioSource.outputAudioMixerGroup = mixerGroups[currentClipIndex];
        }
        audioSource.Play();
    }

    private void PlayNextClip()
    {
        currentClipIndex = (currentClipIndex + 1) % musicClips.Count;
        PlayCurrentClip();
    }
}
