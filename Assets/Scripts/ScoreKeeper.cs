using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class ScoreKeeper : MonoBehaviour, IListener<PlayerDeathMessage> {

    public delegate void ScoreChangedDelegate(int newScore, float timeMultiplier);

    public event ScoreChangedDelegate OnScoreChanged;

    private int score;
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;

            if (OnScoreChanged != null)
            {
                OnScoreChanged(score, timeMultiplier);
            }
        }
    }

    public float timeMultiplier = 1000f;

    public bool Paused { get; protected set; }

    void Start()
    {
        Score = 0;
        Paused = false;

        MessageBus.Instance.Subscribe<PlayerDeathMessage>(this);
    }
	
	void Update () {

        if (Paused) { return; }

        UpdateScore();
	}

    private void UpdateScore()
    {
        Score += (int)(Time.deltaTime * timeMultiplier);
    }

    public void HandleMessage(PlayerDeathMessage message)
    {
        Paused = !Paused;
    }
}
