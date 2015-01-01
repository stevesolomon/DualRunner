using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class ScoreKeeper : MonoBehaviour {

	public Text ScoreText;

	private int score = 0;

    public int timeMultiplier = 1000;
	
	// Update is called once per frame
	void Update () {
		score += (int) (Time.deltaTime * timeMultiplier);

        SetScoreText();
	}

    private void SetScoreText()
    {
        float normScore = score / (float) timeMultiplier;

        ScoreText.text = normScore.ToString("0.000", CultureInfo.InvariantCulture) + "\"";
    }
}
