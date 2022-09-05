using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int score; // total score
    [SerializeField] private int currentscore; // current score
    public int Score { get => score; set { score = value; } }
    [SerializeField] private Text scoreText; //UI to show text score

    private float timeOfChange = 0.1f;
    private void Awake()
    {
        currentscore = score;
    }
    public IEnumerator ChangeScore() // change a score by one point every timeOfChange and add one point to current score
    {
        scoreText.text = "" + currentscore++;
        yield return new WaitForSeconds(timeOfChange);
    }
    // Update is called once per frame
    void Update()
    {
        // call ChangeScore method when currentscore is less then actual one
        if (currentscore<=score) StartCoroutine(ChangeScore());
    }
}
