using UnityEngine;
using System.Collections;

public class RestartPanelHandler : MonoBehaviour, IListener<PlayerDeathMessage> 
{
    public float waitTime = 0.5f;
    public float appearTime = 0.75f;

    public CanvasGroup restartCanvasGroup;

	void Start () 
    {
        MessageBus.Instance.Subscribe<PlayerDeathMessage>(this);

        if (restartCanvasGroup == null)
        {
            restartCanvasGroup = this.GetComponent<CanvasGroup>();
        }
	}

    public void HandleMessage(PlayerDeathMessage message)
    {
        StartCoroutine(ShowRestartPanel());
    }

    public IEnumerator ShowRestartPanel()
    {
        yield return new WaitForSeconds(waitTime);
        
        restartCanvasGroup.interactable = true;

        var totalTime = 0f;

        while (totalTime < appearTime)
        {
            totalTime += Time.deltaTime;
            restartCanvasGroup.alpha = Mathf.Lerp(0, 1, totalTime / appearTime);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
