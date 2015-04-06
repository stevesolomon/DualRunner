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
        ProfileEvents.OnLoginFailed += OnLoginFailed;
        ProfileEvents.OnLoginFinished += OnLoginFinished;
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
        StartTweetScore();
    }

    public void StartTweetScore()
    {
        if (!SoomlaProfile.IsLoggedIn(Provider.TWITTER))
        {
            SoomlaProfile.Login(Provider.TWITTER);
        }
        else
        {
            TweetScore();        
        }
    }

    //If not logged in:
    //   - Log in, subscribe to onLogin events.
    //   - If successful, tweet score
    //Else tweet score

    private void TweetScore()
    {
        oldCanvasAlpha = restartCanvas.alpha;
        restartCanvas.alpha = 0f;
        SoomlaProfile.UploadCurrentScreenShot(this, Provider.TWITTER, "I just got a high scorein #DualRunner!", "Check out my high score in #DualRunner!");    
    }

    private void OnLoginFinished(UserProfile userProfile, string payload)
    {
        if (userProfile.Provider == Provider.TWITTER)
        {
            TweetScore();
        }
    }

    private void OnLoginFailed(Provider provider, string arg2, string arg3)
    {
        //TODO: Add small UI element that says login failed.
    } 

    private void OnScreenshotCaptured(Provider arg1, string arg2)
    {
        restartCanvas.alpha = oldCanvasAlpha;
    } 
}
