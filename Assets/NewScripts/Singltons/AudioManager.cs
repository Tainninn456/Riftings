using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        panelMove,
        popUp,
        popDown,
        ItemOk,
        ItemMiss,
        sceneMove,
        ResultSE
    }
    public enum SoundType
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
        for (int k = 0; k < 6; k++)
        {
            int ini = 600 + k * 100;
            int inc = 80;
            int[] resu = new int[8];
            resu[0] = ini;
            string[] dis = new string[8];
            dis[0] = resu[0].ToString();
            for (int i = 1; i < 8; i++)
            {
                resu[i] = resu[i - 1] + inc;
                inc += 80;
                dis[i] = resu[i].ToString();
            }
            for (int i = 0; i < 8; i++)
            {
                Debug.Log(string.Join(", ", dis));
            }
        }
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

    public void StopBGM()
    {
        bgm.Stop();
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
