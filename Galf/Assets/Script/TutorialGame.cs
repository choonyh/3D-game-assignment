using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGame : MonoBehaviour
{
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public AnimationStateController aSC;
    bool levelLoading = false;
    public GameObject lumberjack;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FreezePlayer();
        animator = lumberjack.GetComponent<Animator>();
        animator.Play("Lumbering");
    }
                        
    void Update()
    {
        if (lumberjack == null && !levelLoading)
        {
            levelLoading = true;
            LoadLevel1();
        }
    }


    public void FreezePlayer()
    {
        playerMovement.isTutorial = true;
        aSC.isTutorial = true;
    }

    public void UnfreezePlayer()
    {
        playerMovement.isTutorial = false;
        aSC.isTutorial = false;
    }
    public void LoadLevel1()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(0);

    }


}
