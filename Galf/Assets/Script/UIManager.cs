using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class UIManager : MonoBehaviour
{
    private int levelToLoad;
    [SerializeField] private AudioClip beep;

    public void QuitBtnClicked()
    {
        levelToLoad = 1;
        playBtnSound();
        Application.Quit();
    }

    void transition()
    {
        playBtnSound();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(levelToLoad);

    }

    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = beep;
        audio.Play();
    }

    public void PlayBtnClicked()
    {
        levelToLoad = 0;
        transition();
    }

    public void SettingsClicked()
    {
        levelToLoad = 2;
        transition();
    }

    public void BackClicked()
    {
        levelToLoad = 1;
        transition();
    }

}
