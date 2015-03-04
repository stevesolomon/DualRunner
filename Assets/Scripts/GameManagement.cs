using UnityEngine;
using System.Collections;

public delegate void PlayerDeathDelegate(GameObject player);

public class GameManagement : MonoBehaviour {

    private GameObject[] players;

    public string playerGameObjectTag = "Player";

    public ParticleSystem deathParticles;

    public ScoreKeeper scoreKeeper;

    public event PlayerDeathDelegate OnPlayerDeath;

    private bool playerAlreadyHitHazard;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag(playerGameObjectTag);

        if (deathParticles == null) 
        {
            deathParticles = GameObject.Find("DeathParticles").GetComponent<ParticleSystem>();
        }

        if (scoreKeeper == null)
        {
            scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        }

        playerAlreadyHitHazard = false;
	}

    public void PlayerHitHazard(GameObject playerThatHit)
    {
        if (playerAlreadyHitHazard) return;

        playerAlreadyHitHazard = true;
        foreach (var player in players)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            player.GetComponent<PlayerControlManager>().enabled = false;
        }

        StartCoroutine(OnPlayerHitHazard(playerThatHit));

        if (OnPlayerDeath != null)
        {
            OnPlayerDeath(playerThatHit);
        }
    }

    private IEnumerator OnPlayerHitHazard(GameObject playerThatHit) 
    {
        playerThatHit.GetComponent<Renderer>().enabled = false;
        deathParticles.transform.position = playerThatHit.transform.position;
        deathParticles.Play();

        scoreKeeper.Pause();

        yield return new WaitForSeconds(1.5f);
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
