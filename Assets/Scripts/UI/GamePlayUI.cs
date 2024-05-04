using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    private static readonly int SCORE_FACTOR = 10;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;
    [SerializeField] private TextMeshProUGUI gameOverLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighestScoreString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighestScoreString();
    }

    private string GetScoreString(){
        return (GameManager.Instance.GetScore() * SCORE_FACTOR).ToString();
    }

    private string GetHighestScoreString(){
        return (GameManager.Instance.GetHighestScore() * SCORE_FACTOR).ToString();
    }

    public void SetGameOverText(string text){
        gameOverLabel.text = text;
    }
}
