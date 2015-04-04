using UnityEngine;
using System.Collections;
using Soomla.Profile;
using Assets.Scripts.EventAggregator.Messages;

public class SocialManager : MonoBehaviour, IListener<TweetScoreMessage> {

	// Use this for initialization
	void Start () {        
        SoomlaProfile.Initialize();
        MessageBus.Instance.Subscribe<TweetScoreMessage>(this);
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
            SoomlaProfile.UpdateStory(Provider.TWITTER, "I just got a high score in #DualRunner!", "#DualRunner high score", "My high score", "dual_runner_score", "http://twitter.com", null, null);
        }
    }
}
