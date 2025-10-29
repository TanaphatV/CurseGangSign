using UnityEngine;
using UnityEngine.Audio;
using AYellowpaper.SerializedCollections;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    [SerializedDictionary("Audio Key", "Audio Clip")]
    public SerializedDictionary<string, AudioClip> audioDictionary;

    public AudioSource globalSound;
    public AudioMixer mixer;

    const string MIXER_MASTER = "MASTERVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_BGM = "BGMVolume";

    public void AdjustMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }

    public void AdjustSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void AdjustBGMVolume(float value)
    {
        mixer.SetFloat(MIXER_BGM, Mathf.Log10(value) * 20);
    }

    public void PlayGlobalSound(string key, float volume)
    {
        if (!audioDictionary.ContainsKey(key))
        {
            Debug.Log("Audio key don't exist dumbass");
            return;
        }

        globalSound.clip = audioDictionary[key];
        globalSound.volume = volume;
        globalSound.Play();

        Debug.Log("Play Audio Clip: " + audioDictionary[key].ToString());
    }

    public void PlayOneShot(string key, float volume)
    {
        if (!audioDictionary.ContainsKey(key))
        {
            Debug.Log("Audio key don't exist dumbass");
            return;
        }

        globalSound.PlayOneShot(audioDictionary[key], volume);
        Debug.Log("Play Audio Clip: " + audioDictionary[key].ToString());
    }
}
