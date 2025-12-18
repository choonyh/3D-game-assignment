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
    public HealthBar hpBar;
    public UIManager manager;

    public bool isLost = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        currentHP = healthMax;
        hpBar.SetMaxHealth(healthMax);
        hpBar.SetHealth(currentHP);
    }

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(currentHP - damage,0);
        hpBar.SetHealth(currentHP);
        if (currentHP <= 0)
        {
            isLost = true;
            StartCoroutine(Wait());
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        manager.LoadLoseScene();
    }
}
