using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    public static ScoreBoard Instance;
    [SerializeField] TextMesh scoreTextMesh;

    int score;
    int streak;
    int basicHit=100;

    void Awake() {
        Instance = this;
    }

    public void IncreaseScore() {
        streak += 1;
        score += streak * basicHit;
        scoreTextMesh.text = score.ToString();
    }

    public void ReportMiss() {
        streak = 0;
    }

}
