using UnityEngine;
using System.Collections;
using Assets.Scripts.EventAggregator.Messages.Social;
using UnityEngine.UI;

public class RestartPanelHandler : MonoBehaviour, IListener<PlayerDeathMessage>, IListener<TwitterLoginFailureMessage>
{
    public float waitTime = 0.5f;
    public float appearTime = 0.75f;

    public CanvasGroup restartCanvasGroup;

    public Animator twitterLoginFailedAnimator;

	void Start () 
    {
        MessageBus.Instance.Subscribe<PlayerDeathMessage>(this);
        MessageBus.Instance.Subscribe<TwitterLoginFailureMessage>(this);

        if (restartCanvasGroup == null)
        {
            restartCanvasGroup = this.GetComponent<CanvasGroup>();
        }

        if (twitterLoginFailedAnimator == null)
        {
            twitterLoginFailedAnimator = this.transform.Find("TwitterLoginFailedText").GetComponent<Animator>();
        }

        #if (!UNITY_IOS && !UNITY_ANDROID) 
        var twitterButton = this.transform.Find("TweetButton").GetComponent<Button>();
        twitterButton.onClick.AddListener(DisabledTweetButtonClick);
        this.transform.Find("TwitterLoginFailedText").GetComponent<Text>().text = "Sorry, Tweeting scores only works in iOS/Android!";
        #endif
	}

    #if (!UNITY_IOS && !UNITY_ANDROID) 
    private void DisabledTweetButtonClick()
    {
        twitterLoginFailedAnimator.SetTrigger("showTwitterLoginError");
    }
    #endif

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

    public void HandleMessage(TwitterLoginFailureMessage message)
    {
        twitterLoginFailedAnimator.SetTrigger("showTwitterLoginError");
    }

}
