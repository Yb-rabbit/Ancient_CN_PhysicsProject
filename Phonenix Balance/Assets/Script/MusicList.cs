using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicList : MonoBehaviour
{
    [Header("音频设置")]
    public List<AudioClip> musicClips; // 音频剪辑列表
    public List<AudioMixerGroup> mixerGroups; // 混音器轨道列表
    public float delayInSeconds = 5f; // 延迟秒数

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool firstClipPlayed = false; // 标志位，判断是否已经播放了第一个音频

    void Start()
    {
        if (musicClips == null || musicClips.Count == 0)
        {
            Debug.LogWarning("音频剪辑列表为空，无法播放音乐！");
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
            Debug.LogError("当前音频索引超出范围！");
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
