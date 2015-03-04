using UnityEngine;
using System.Collections;

public class RestartPanelHandler : MonoBehaviour {

    public GameManagement gameManager;

    public string gameOverAnimatorPropName = "GameOverTrigger";

	// Use this for initialization
	void Start () {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagement>();
        }

        gameManager.OnPlayerDeath += OnPlayerDeath;
	}

    void OnPlayerDeath(GameObject player)
    {
        this.GetComponent<Animator>().SetBool(gameOverAnimatorPropName, true);
    }
}
