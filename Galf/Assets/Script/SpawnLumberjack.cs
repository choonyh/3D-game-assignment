using UnityEngine;

public class SpawnLumberjack : MonoBehaviour
{
    public GameObject lumberjack;
    public Transform[] spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform t in spawnPoints)
        {
            Instantiate(lumberjack,t.position,t.rotation);
        }
    }

}
