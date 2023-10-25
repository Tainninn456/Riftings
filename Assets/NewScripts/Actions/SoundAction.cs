using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAction : MonoBehaviour
{
    public enum SoundType
    {
        BGM,
        SE
    }

    public void VolumeUp(SoundType soundType)
    {
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

    public void VolumeDown(SoundType soundType)
    {
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