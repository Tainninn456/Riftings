using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// サウンドに関するクラス、シングルトンでどのクラスからも呼び出すことができる
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGMとSEの内容物を保持")]
    [SerializeField]
    private AudioClip[] bgmClips;
    [SerializeField]
    private AudioClip[] seClips;

    [Header("BGMとSE用のAudioSourceを保持")]
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
        ResultSE,
        kick,
        coin,
        wall
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
    }

    //BGMを再生する関数
    public void PlayBGM(BGM bgmName)
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
        bgm.clip = bgmClips[(int)bgmName];
        bgm.Play();
    }

    //BGMを停止する関数
    public void StopBGM()
    {
        bgm.Stop();
    }

    //SEを再生する関数
    public void PlaySE(SE seName)
    {
        se.PlayOneShot(seClips[(int)seName]);
    }

    //BGMのボリュームを変更する関数
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

    //SEのボリュームを変更する関数
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
