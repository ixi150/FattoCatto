using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{
    [SerializeField]
    AudioClip[] soundClips;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandom()
    {
        if (soundClips == null || soundClips.Length <= 0) return;
        audioSource.clip = soundClips[Random.Range(0, soundClips.Length)];
        audioSource.Play();
    }

    public void StartFadingOut(float duration)
    {
        StartCoroutine(StartFadingOutCorutine(duration));
    }

    IEnumerator StartFadingOutCorutine(float duration)
    {
        float timer = 0;
        float initialVolume = audioSource.volume;
        while (true)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, 0, timer / duration);
            if (timer > duration) break;
            yield return new WaitForEndOfFrame();
        }
    }
}
