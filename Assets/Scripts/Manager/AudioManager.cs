using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    [Header("SFX")]
    public AudioClip footstep;

    private bool isFootstepPlaying = false;

    private void Start()
    {
        //PlayMusic(musicClip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, bool isPitchShifting, float offset)
    {
        sfxSource.pitch = isPitchShifting ? Random.Range(1f - offset, 1f + offset) : 1f ;
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void PlayFootStep(float interval)
    {
        if (isFootstepPlaying)
            return;

        StartCoroutine(FootStepCoroutine(interval));
    }

    public void StopFootStep()
    {
        StopAllCoroutines();
        isFootstepPlaying = false;
    }

    private IEnumerator FootStepCoroutine(float interval)
    {
        isFootstepPlaying = true;

        while (true)
        {
            PlaySFX(footstep, true, 0.2f);
            yield return new WaitForSeconds(interval);
        }
    }
}
