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
        AudioManager.Instance.PlaySE(AudioManager.SE.popUp);
        switch (soundType)
        {
            case SoundType.BGM:
                AudioManager.Instance.BGMVolumeChange(AudioManager.VolumeInstruction.up);
                break;
            case SoundType.SE:
                AudioManager.Instance.SEVolumeChange(AudioManager.VolumeInstruction.up);
                break;
        }
    }

    //対象のAudioSourceのボリュームを下げる関数
    public void VolumeDown(SoundType soundType)
    {
        AudioManager.Instance.PlaySE(AudioManager.SE.popDown);
        switch (soundType)
        {
            case SoundType.BGM:
                AudioManager.Instance.BGMVolumeChange(AudioManager.VolumeInstruction.down);
                break;
            case SoundType.SE:
                AudioManager.Instance.SEVolumeChange(AudioManager.VolumeInstruction.down);
                break;
        }
    }
}