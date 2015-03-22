using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

    private GameObject[] players;

    public string playerGameObjectTag = "Player";

    public ParticleSystem deathParticles;
    
    private bool playerAlreadyHitHazard;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag(playerGameObjectTag);

        if (deathParticles == null) 
        {
            deathParticles = GameObject.Find("DeathParticles").GetComponent<ParticleSystem>();
        }

        playerAlreadyHitHazard = false;
	}

    public void PlayerHitHazard(GameObject playerThatHit)
    {
        if (playerAlreadyHitHazard) return;

        playerAlreadyHitHazard = true;
        foreach (var player in players)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, player.GetComponent<Rigidbody2D>().velocity.y);
            //player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            player.GetComponent<PlayerControlManager>().enabled = false;
        }

        playerThatHit.GetComponent<Renderer>().enabled = false;
        deathParticles.transform.position = playerThatHit.transform.position;
        deathParticles.Play();

        MessageBus.Instance.SendMessage(new PlayerDeathMessage() { Player = playerThatHit });
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
