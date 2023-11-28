using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    /*public static AudioSource SEAudio; public static AudioSource BGMAudio;
    public AudioClip[] Inclips = new AudioClip[12];
    public static AudioClip[] clips = new AudioClip[12];
    public static bool playSESound = true; public static bool playBGMSound = true;
    public static int soundindex = 1;
    void Start()
    {
        SEAudio = Main.panels[0].transform.GetChild(6).gameObject.GetComponent<AudioSource>();
        BGMAudio = Main.panels[0].transform.GetChild(7).gameObject.GetComponent<AudioSource>();
        SEAudio.volume = (Main.memory.SEVolume + 1) * 0.2f;
        BGMAudio.volume = (Main.memory.BGMVolume + 1) * 0.2f;
        for (int i = 0; i < 12; i++)
        {
            clips[i] = Inclips[i];
        }
    }
    public static void SEplay(int index)
    {
        switch (index)
        {
            case 0:
                SEAudio.PlayOneShot(clips[0]);
                break;
            case 3:
                SEAudio.PlayOneShot(clips[3]);
                break;
            case 4:
                SEAudio.PlayOneShot(clips[4]);
                break;
            case 5:
                SEAudio.PlayOneShot(clips[5]);
                break;
            case 6:
                SEAudio.PlayOneShot(clips[6]);
                break;
            case 7:
                SEAudio.PlayOneShot(clips[7]);
                break;
            case 8:
                SEAudio.PlayOneShot(clips[8]);
                break;
            case 9:
                SEAudio.PlayOneShot(clips[9]);
                break;
            case 10:
                SEAudio.PlayOneShot(clips[10]);
                break;
            case 11:
                SEAudio.PlayOneShot(clips[11]);
                break;
            default:
                break;
        }
    }
    public static void BGMplay(int index)
    {
        switch (index)
        {
            case 1:
                BGMAudio.PlayOneShot(clips[1]);
                break;
            case 2:
                BGMAudio.PlayOneShot(clips[2]);
                break;
            default:
                break;
        }
    }*/
}