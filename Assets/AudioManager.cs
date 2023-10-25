using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioClip[] bgmClips;
    [SerializeField]
    private AudioClip[] seClips;

    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioSource se;
    public enum BGM
    {
        menu,
        play
    }
    public enum SE
    {
        tap,
        change,
        kick
    }
    public enum SountType
    {
        BGM,
        SE
    }
    public enum VolumeInstruction
    {
        up,
        down
    }
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayBGM(BGM.menu);
    }

    public void PlayBGM(BGM bgmName)
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
        bgm.clip = bgmClips[(int)bgmName];
        bgm.Play();
    }

    public void PlaySE(SE seName)
    {
        se.PlayOneShot(seClips[(int)seName]);
    }

    public void BGMVolumeChange(VolumeInstruction inst)
    {
        if(inst == VolumeInstruction.up)
        {
            bgm.volume += 0.2f;
        }
        else if(inst == VolumeInstruction.down)
        {
            bgm.volume -= 0.2f;
        }
    }

    public void SEVolumeChange(VolumeInstruction inst)
    {
        if(inst == VolumeInstruction.up)
        {
            se.volume += 0.2f;
        }
        else if(inst == VolumeInstruction.down)
        {
            se.volume -= 0.2f;
        }
    }
}
