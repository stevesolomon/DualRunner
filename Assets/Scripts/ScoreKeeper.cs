using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class ScoreKeeper : MonoBehaviour, IPauseable {

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
        Play();
    }
	
	void Update () {

        if (Paused) { return; }

        UpdateScore();
	}

    private void UpdateScore()
    {
        Score += (int)(Time.deltaTime * timeMultiplier);
    }

    public void Pause()
    {
        Paused = true;
    }

    public void Play()
    {
        Paused = false;
    }
}
