using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] scoreTxt;
    float lastScoreaddedposition = -6f;
    int scoreAddSpace = 2;
    private int coin = 0;
    private int score = 0;
    private int HighScore = 0;
    string scorePath;

    private void Update()
    {
        if (transform.position.x - lastScoreaddedposition >= scoreAddSpace)
        {
            addScore(1);
            lastScoreaddedposition = transform.position.x;
        }
    }
    private void Start()
    {
        scorePath = Application.persistentDataPath + "//Local.scores";
        if (!File.Exists(scorePath))
        {
            File.Create(scorePath);
        }
        HighScore = LoadHighScore();
        scoreTxt[1].text = HighScore.ToString();
    }
    public void SaveScore()
    {
        if(HighScore < score)
        {
            File.WriteAllText(scorePath, score.ToString());
        }
    }
    public int LoadHighScore()
    {
        if (int.TryParse(File.ReadAllText(scorePath), out int score1))
        {
            return score1;
        }
        else
        {
            return 0;
        }

    }
    public void addScore(int ammount)
    {
        score += ammount;
        scoreTxt[0].text = score.ToString();
        if (HighScore <= score)
        {
            scoreTxt[1].text = score.ToString();
        }

    }
    public void addCoin(int ammount)
    {
        coin += ammount;
    }
    public int getCoin()
    {
        return coin;
    }

}
