using UnityEngine;
using System.Collections;
using Soomla.Profile;
using Assets.Scripts.EventAggregator.Messages;
using Assets.Scripts.EventAggregator.Messages.Social;

public class SocialManager : MonoBehaviour, IListener<TweetScoreMessage>{

    public CanvasGroup restartCanvas;

    private float oldCanvasAlpha;

	// Use this for initialization
	void Start () {        
        SoomlaProfile.Initialize();
        ProfileEvents.OnTakeScreenshotStarted += OnTakeScreenshotStarted;
        ProfileEvents.OnTakeScreenshotFinished += OnTakeScreenshotFinished;
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
        SoomlaProfile.UploadCurrentScreenShot(this, Provider.TWITTER, "I just got a high scorein #DualRunner!", "Check out my high score in #DualRunner!");    
    }

    private void OnLoginFinished(UserProfile userProfile, string payload)
    {
        if (userProfile.Provider == Provider.TWITTER)
        {
            TweetScore();
        }
    }

    private void OnLoginFailed(Provider provider, string errorMessage, string payload)
    {
        //Error code 89 means we should logout and try again.
        if (provider == Provider.TWITTER && errorMessage.Contains("89"))
        {
            Debug.Log("Failed login, attempting to logout...");
            SoomlaProfile.Logout(provider);
            SoomlaProfile.Login(Provider.TWITTER);
        }
        else //We had some general login failure, tell the panel.
        {
            MessageBus.Instance.SendMessage(new TwitterLoginFailureMessage());
        }
    }

    private void OnTakeScreenshotStarted(Provider provider, string payload)
    {
        oldCanvasAlpha = restartCanvas.alpha;
        restartCanvas.alpha = 0f;
    }

    private void OnTakeScreenshotFinished(Provider arg1, string arg2)
    {
        restartCanvas.alpha = oldCanvasAlpha;
    }
}
