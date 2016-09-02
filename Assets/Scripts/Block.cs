using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    [SerializeField] SpriteRenderer mySpriteRenderer;
    public float startScale, endScale;
    public float pulseRate, holdTime;
    public bool isClickable, isSelected;

    public void StartPulse() {
        StopAllCoroutines();
        StartCoroutine(Pulse());
    }

    void Awake() {

    }

    IEnumerator Pulse() {
        isSelected = true;
        isClickable = true;
        mySpriteRenderer.color = Color.red;
        yield return StartCoroutine(Scale(endScale));
        yield return new WaitForSeconds(holdTime);
        isSelected = false;
        isClickable = false;
        ScoreBoard.Instance.ReportMiss();
        mySpriteRenderer.color = Color.white;
        yield return StartCoroutine(Scale(startScale));
    }

    IEnumerator Scale(float targetScale) {
        while (Mathf.Abs(transform.localScale.x - targetScale) > 0.2f) {
            transform.localScale = Vector3.Slerp(transform.lossyScale, Vector3.one * targetScale, pulseRate);
            yield return null;
        }
    }

    void OnMouseDown() {
        if (isClickable) {
            StopAllCoroutines();
            StartCoroutine(Scale(startScale));
            mySpriteRenderer.color = Color.white;
            ScoreBoard.Instance.IncreaseScore();
            //Explode();
            isClickable = false;
            isSelected = false;
        }
    }

    void Explode() {

    }
}
