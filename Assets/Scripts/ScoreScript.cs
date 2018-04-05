using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    Text ScoreText;

    public int Score;
    //public int DfltScore;

    //public int SubstractScoreValue;
    //public float TimeToLowerSecs;

    //bool IsRunningCoroutine = false;

    private void Start()
    {
        ScoreText = GetComponent<Text>();

        ScoreText.text = "Score: " + Score.ToString();
    }

    private void Update() 
    {
        Score = (Score < 0) ? 0 : Score;

        ScoreText.text = "Score: " + Score.ToString();
    }

    //private void TimeLowerScore()
    //{
    //    Score -= SubstractScoreValue;
    //}

    //private IEnumerator TimeLowerScore()
    //{
    //    IsRunningCoroutine = true;

    //    yield return new WaitForSeconds(TimeToLowerSecs);

    //    Score -= SubstractScoreValue;

    //    IsRunningCoroutine = false;
    //}
}
