using UnityEngine;

public class InGameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject spawnPoint;
    public int hp = 100;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
       player.transform.position = spawnPoint.transform.position;
        
    }

}
