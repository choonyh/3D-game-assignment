using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProperties : MonoBehaviour
{
    public int monei;
    public int trees;
    public int healthMax = 100;
    public int currentHP;
    public GameObject powerUp;

    public bool isLost = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = healthMax;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= Mathf.Max(currentHP - damage,0);
        if (currentHP <= 0)
        {
            isLost = true;
            StartCoroutine(Wait());
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(5);//Lose scene
    }
}
