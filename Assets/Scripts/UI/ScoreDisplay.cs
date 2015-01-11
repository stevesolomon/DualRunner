using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class ScoreDisplay : MonoBehaviour 
{
    public Text scoreText;

    public ScoreKeeper scoreKeeper;
    
    public Color flashColor = Color.yellow;

    private float lastScore;

    private float currScore;

	void Start () 
    {
        if (scoreText == null)
        {
            scoreText = GetComponent<Text>();
        }

        if (scoreKeeper == null)
        {
            scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        }

        scoreKeeper.OnScoreChanged += ScoreChanged;
	}

    public void ScoreChanged(int newScore, float timeMultiplier)
    {
        lastScore = currScore;
        currScore = newScore / timeMultiplier;
        SetScoreText(currScore);

        ScoreEffectChecks();
    }

    private void ScoreEffectChecks()
    {
        if (((int) lastScore) % 10 != 0 && ((int) currScore) % 10 == 0)
        {
            StartCoroutine(FlashScore());
        }
    }

    private IEnumerator FlashScore()
    {
        Color oldColor = scoreText.color;
        scoreText.color = flashColor;

        for (int i = 0; i < 8; i++)
        {
            scoreText.fontSize += 1;
            yield return new WaitForSeconds(0.015f);
        }
        
        for (int i = 0; i < 8; i++)
        {
            scoreText.fontSize -= 1;
            yield return new WaitForSeconds(0.015f);
        }

        scoreText.color = oldColor;

        yield return null;
    }

    private void SetScoreText(float newScore)
    {
        scoreText.text = newScore.ToString("0.000", CultureInfo.InvariantCulture) + "\"";
    }
}
