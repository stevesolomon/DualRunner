using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

    public ParticleSystem deathParticles;

    void Start()
    {
        if (deathParticles == null)
        {
            deathParticles = GameObject.Find("DeathParticles").GetComponent<ParticleSystem>();
        }
    }

    public void HitHazard()
    {
        StartCoroutine(RunHitHazard());
    }

    private IEnumerator RunHitHazard()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.GetComponent<PlayerControlManager>().enabled = false;

        deathParticles.transform.position = this.transform.position;
        deathParticles.Play();

        yield return new WaitForSeconds(3f);

        Application.LoadLevel(0);
    }
}
