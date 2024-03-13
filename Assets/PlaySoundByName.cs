using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundByName : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipWithVolume 
    {
        public string name;
        public AudioClip audioClip;
        public float volume;
    }

    public List<AudioClipWithVolume> namedAudioClips;

    public void PlayAudioClipByName(string name)
    {
        AudioClipWithVolume a = namedAudioClips.Find(x => x.name == name);

        if (a != null)
        {
            Audio2DSingleton.instance.audioSource.PlayOneShot(a.audioClip, a.volume);
        }
        else 
        {
            Debug.LogError($"audio clip with name \"{name}\" not found!");
        }
    }
}
