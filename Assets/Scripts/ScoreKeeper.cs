using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public Text ScoreText;

	private int score = 0;
	
	// Update is called once per frame
	void Update () {
		score += (int) (Time.deltaTime * 100f);

		ScoreText.text = score.ToString();
	}
}
