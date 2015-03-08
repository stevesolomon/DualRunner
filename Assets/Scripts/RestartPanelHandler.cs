using UnityEngine;
using System.Collections;

public class RestartPanelHandler : MonoBehaviour, IListener<PlayerDeathMessage> {
    
    public string gameOverAnimatorPropName = "GameOverTrigger";

	// Use this for initialization
	void Start () {
        MessageBus.Instance.Subscribe<PlayerDeathMessage>(this);
	}

    public void HandleMessage(PlayerDeathMessage message)
    {
        this.GetComponent<Animator>().SetBool(gameOverAnimatorPropName, true);
    }
}
