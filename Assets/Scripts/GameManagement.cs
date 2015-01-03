using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

    private GameObject[] players;

    public string playerGameObjectTag = "Player";

    public ParticleSystem deathParticles;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag(playerGameObjectTag);

        if (deathParticles == null) 
        {
            deathParticles = GameObject.Find("DeathParticles").GetComponent<ParticleSystem>();
        }
	}

    public void PlayerHitHazard(GameObject playerThatHit)
    {
        foreach (var player in players)
        {
            player.rigidbody2D.velocity = Vector2.zero;
            player.GetComponent<PlayerControlManager>().enabled = false;
        }

        StartCoroutine(OnPlayerHitHazard(playerThatHit));
    }

    private IEnumerator OnPlayerHitHazard(GameObject playerThatHit) 
    {
        playerThatHit.renderer.enabled = false;
        deathParticles.transform.position = playerThatHit.transform.position;
        deathParticles.Play();

        yield return new WaitForSeconds(2.5f);
        Application.LoadLevel(Application.loadedLevel);
    }
}
