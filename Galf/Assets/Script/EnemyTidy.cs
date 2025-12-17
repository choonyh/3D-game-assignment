using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EnemyTidy : MonoBehaviour
{

    public int beenHit = 0;
    AudioSource audioSource;
    public AudioClip audioClip;
    bool isDead = false;
    public ParticleSystem dieEffect;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (isDead) { return; }

        if (collision.gameObject.name == "Golf")
        {
            beenHit++;

            if (beenHit >= 3)
            {
                Die();
            }
        }

    }

    void Die()
    {
        isDead = true;

        transform.rotation = Quaternion.Euler(75f, 0f, 0f);

        audioSource.clip = audioClip;
        dieEffect.Play();
        audioSource.Play();

        StartCoroutine(DestroyAfterSound());
    }

    IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(audioClip.length);
        Destroy(gameObject);
    }
}
