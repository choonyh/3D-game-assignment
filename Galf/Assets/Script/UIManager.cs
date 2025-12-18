using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIManager : MonoBehaviour
{
    private int levelToLoad;
    [SerializeField] private AudioClip beep;
    public GameObject pause;
    public GameObject defeat;
    //[SerializeField] private Button[] button;
    //[SerializeField] private TextMeshProUGUI text;
    //[SerializeField] private Image background;

    public void QuitBtnClicked()
    {
        levelToLoad = 1;
        playBtnSound();
        Application.Quit();
    }

    void transition()
    {
        playBtnSound();
        Time.timeScale = 1.0f;
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
        Debug.LogError("Hello");
        levelToLoad = 3;
        transition();
    }

    public void SettingsClicked()
    {
        levelToLoad = 2;
        transition();
    }

    public void BackClicked()
    {
        
        Debug.LogError("Hello");
        levelToLoad = 1;
        transition();
    }

    public void ResumeClicked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pause.SetActive(false);
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        
    }

    public void Paused()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pause.SetActive(true);
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
    }

    public void LoadLoseScene()
    {
        Cursor.lockState = CursorLockMode.Confined;
        defeat.SetActive(true);
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
    }    
}
