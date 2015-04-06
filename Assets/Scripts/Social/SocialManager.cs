using UnityEngine;
using System.Collections;
using Soomla.Profile;
using Assets.Scripts.EventAggregator.Messages;

public class SocialManager : MonoBehaviour, IListener<TweetScoreMessage> {

    public CanvasGroup restartCanvas;

    private float oldCanvasAlpha;

	// Use this for initialization
	void Start () {        
        SoomlaProfile.Initialize();
        ProfileEvents.OnScreenshotCaptured += OnScreenshotCaptured;
        MessageBus.Instance.Subscribe<TweetScoreMessage>(this);

        if (restartCanvas == null)
        {
            restartCanvas = GameObject.Find("RestartPanel").GetComponent<CanvasGroup>();
        }
	}    
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleMessage(TweetScoreMessage message)
    {
        TweetScore();
    }

    public void TweetScore()
    {
        if (!SoomlaProfile.IsLoggedIn(Provider.TWITTER))
        {
            SoomlaProfile.Login(Provider.TWITTER);
        }
        else
        {
            oldCanvasAlpha = restartCanvas.alpha;
            restartCanvas.alpha = 0f;
            SoomlaProfile.UploadCurrentScreenShot(this, Provider.TWITTER, "I just got a high scorein #DualRunner!", "Check out my high score in #DualRunner!");            
        }
    }

    private void OnScreenshotCaptured(Provider arg1, string arg2)
    {
        restartCanvas.alpha = oldCanvasAlpha;
    } 
}
