using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour //FIX THIS;
{
    public GameObject Score;
    ScoreScript ScoreScript;

    public GameObject Camera;

    Vector3 HelpVector;

	// Use this for initialization
	void Start ()
    {
        ScoreScript = Score.GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update ()
    {
		if (ScoreScript.Score <= 0)
        {
            GlobalVariables.IsGameOver = true;

            GetComponent<SpriteRenderer>().enabled = true;

            HelpVector = Camera.transform.position;
            HelpVector.z = 0;

            transform.position = HelpVector;
        }
	}
}
