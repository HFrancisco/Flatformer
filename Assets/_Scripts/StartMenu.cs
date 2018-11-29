using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	public Text highScoreText;
	private float score = 0;
	private float prevScore = 0;
    private string isFinish = "False";

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		prevScore = score;
		score = PlayerPrefs.GetFloat ("Time", 0);

        isFinish = PlayerPrefs.GetString("Finish");

        if (isFinish.Equals("True"))
        {
            if (score == 0)
            {
                highScoreText.text = "Current Fastest Time: NA";
            }
            else if (score <= prevScore)
            {
                highScoreText.text = "Current Fastest Time: " + score.ToString("F2");
            }
        }

	}

	public void Change(){
		SceneManager.LoadScene("Level1");
	}
}
