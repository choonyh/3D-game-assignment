using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    public AudioMixer mixer;

    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    public float cameraSensitivity = 10f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
    }

    public void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        cameraSensitivity = PlayerPrefs.GetFloat("CamSensitivity", 10f);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("CamSensitivity", cameraSensitivity);
    }

    public void ApplySettingOnStart()
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(SettingManager.instance.musicVolume) * 20);
        mixer.SetFloat("SfxVolume", Mathf.Log10(SettingManager.instance.sfxVolume) * 20);

    }
}

