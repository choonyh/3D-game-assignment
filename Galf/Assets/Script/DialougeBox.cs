using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DialougeBox : MonoBehaviour
{
    public TextMeshProUGUI textComp;
    public string[] lines;
    public float txtsSpeed;
    [SerializeField]private int index = 0;

    public PlayableDirector timeline;
    public int[] stopPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComp.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (textComp.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComp.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(txtsSpeed);
        }
    }

    void NextLine()
    {

        foreach (int stop in stopPoints)
        {
            if (index == stop)
            {
                StopAllCoroutines();
                textComp.text = string.Empty;

                index++;
                timeline.Resume();
                gameObject.SetActive(false);
                return;                     
            }
        }

        if (index < lines.Length -1)
        {
            index++;
            textComp.text = string.Empty;
            StartCoroutine (TypeLine());
        }else
        {
            timeline.Resume();
            gameObject.SetActive(false);
        }
    }

    public void PauseTimeline()
    {
        timeline.Pause();
    }

    public void ShowDialogueBox()
    {
        gameObject.SetActive(true);
        textComp.text = string.Empty;
        StartDialogue();
    }

    public void LoadTutorial()
    {
        Debug.Log("This Runs");
        SceneManager.LoadScene(4);
        //StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(4);

    }
}
