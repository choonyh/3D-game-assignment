using UnityEngine;

public class EnemyTidy : MonoBehaviour
{

    int beenHit = 0;

    void Update()
    {
        if (beenHit >= 3)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Golf")
        {
            beenHit++;
        }
    }
}
