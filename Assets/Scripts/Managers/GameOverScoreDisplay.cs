using UnityEngine;
using UnityEngine.UI;

public class GameOverScoreDisplay : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Final Score: " + finalScore.ToString();
    }
}
