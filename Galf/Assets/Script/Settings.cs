using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider musicScrollbar;
    [SerializeField] Slider sfxScrollbar;
    [SerializeField] Slider camSenScrollbar;
    [SerializeField] public TextMeshProUGUI musicText;
    [SerializeField] public TextMeshProUGUI sfxText;
    [SerializeField] public TextMeshProUGUI camText;

    void Start()
    {
        SettingManager.instance.LoadSettings();

        musicScrollbar.value = SettingManager.instance.musicVolume;
        sfxScrollbar.value = SettingManager.instance.sfxVolume;
        camSenScrollbar.value = SettingManager.instance.cameraSensitivity;

        musicText.text = Mathf.Round(SettingManager.instance.musicVolume*100).ToString();
        sfxText.text = Mathf.Round(SettingManager.instance.sfxVolume*100).ToString();
        camText.text = Mathf.Round(SettingManager.instance.cameraSensitivity * 100).ToString();

        musicScrollbar.onValueChanged.AddListener(SetMusicVolume);
        sfxScrollbar.onValueChanged.AddListener(SetSfxVolume);
        camSenScrollbar.onValueChanged.AddListener(SetCamSen);
    }

    void SetMusicVolume(float volume)
    {
     
        SettingManager.instance.mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        SettingManager.instance.musicVolume = volume;
        musicText.text = Mathf.RoundToInt(volume * 100).ToString();
        SettingManager.instance.SaveSettings();
    }

    void SetSfxVolume(float volume)
    {
        SettingManager.instance.mixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
        SettingManager.instance.sfxVolume = volume;
        sfxText.text = Mathf.RoundToInt(volume*100).ToString();
        SettingManager.instance.SaveSettings();
    }

    void SetCamSen(float value)
    {
        SettingManager.instance.cameraSensitivity = value;
        camText.text = (value*100).ToString();
        SettingManager.instance.SaveSettings();
    }
   
}
