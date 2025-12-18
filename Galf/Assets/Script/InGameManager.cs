using UnityEngine;

public class InGameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject spawnPoint;
    public int hp = 100;
    public UIManager uiManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.LogError("EscapeKeyPressed!");
            uiManager.Paused();

        }
    }

    void Spawn()
    {
       player.transform.position = spawnPoint.transform.position;
       
    }

}
