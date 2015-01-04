using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class ScoreKeeper : MonoBehaviour, IPauseable {

	public Text ScoreText;

	private int score = 0;

    public int timeMultiplier = 1000;

    public bool Paused { get; protected set; }

    void Start()
    {
        Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (Paused) { return; }

		score += (int) (Time.deltaTime * timeMultiplier);

        SetScoreText();
	}

    private void SetScoreText()
    {
        float normScore = score / (float) timeMultiplier;

        ScoreText.text = normScore.ToString("0.000", CultureInfo.InvariantCulture) + "\"";
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
