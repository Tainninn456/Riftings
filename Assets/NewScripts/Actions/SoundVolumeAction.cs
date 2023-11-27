using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolumeAction : MonoBehaviour
{
    public enum SoundType
    {
        BGM,
        SE
    }

    //対象のAudioSourceのボリュームを上げる関数
    public void VolumeUp(SoundType soundType)
    {
        AudioManager.instance.PlaySE(AudioManager.SE.popUp);
        switch (soundType)
        {
            case SoundType.BGM:
                AudioManager.instance.BGMVolumeChange(AudioManager.VolumeInstruction.up);
                break;
            case SoundType.SE:
                AudioManager.instance.SEVolumeChange(AudioManager.VolumeInstruction.up);
                break;
        }
    }

    //対象のAudioSourceのボリュームを下げる関数
    public void VolumeDown(SoundType soundType)
    {
        AudioManager.instance.PlaySE(AudioManager.SE.popDown);
        switch (soundType)
        {
            case SoundType.BGM:
                AudioManager.instance.BGMVolumeChange(AudioManager.VolumeInstruction.down);
                break;
            case SoundType.SE:
                AudioManager.instance.SEVolumeChange(AudioManager.VolumeInstruction.down);
                break;
        }
    }
}