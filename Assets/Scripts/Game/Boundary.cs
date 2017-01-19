using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    [SerializeField] SpriteRenderer mySprite;

    void Awake() {
        minScale = transform.localScale;
        maxScale = new Vector3(minScale.x * 3, minScale.y, minScale.z);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == Tag.ball) {
            Flash();

            if (isPulsing && canBurst) {
                int streak = ScoreBoard.Instance.GetStreak();
                EffectsManager.Instance.Explode(streak);
                ScoreBoard.Instance.IncreaseScore();
                StartCoroutine(WaitForBurst());
            }
        }
    }

    void Flash() {
        StartCoroutine(FlashCoroutine());
    }
    bool canBurst = true;
    IEnumerator WaitForBurst() {
        canBurst = false;
        yield return new WaitForSeconds(.5f);
        canBurst = true;
    }
    Color targetColor;
    IEnumerator FlashCoroutine() {
        mySprite.color = Colors.GetRandomColor(Color.white);
        while (Colors.GetColorDifference(mySprite.color, Color.white) > .05f) {
            mySprite.color = Color.Lerp(mySprite.color, Color.white, 0.06f);
            yield return null;
        }
        mySprite.color = Color.white;
    }

    void Update() {
        if (tag == Tag.wall) {
            float dist = transform.position.x - GameObject.FindGameObjectWithTag(Tag.ball).transform.position.x;
            if ((Input.GetKeyDown(KeyCode.LeftControl) && dist < 0) || (Input.GetKeyDown(KeyCode.RightControl) && dist > 0)) {
                if (Mathf.Abs(dist) < 3f) {
                    if (!isPulsing) {
                        Pulse();
                    }
                }
            }
        }
    }

    void Pulse(){
        StartCoroutine(PulseCoroutine());
    }

    Vector3 maxScale;
    Vector3 minScale;
    bool isPulsing;
    IEnumerator PulseCoroutine() {
        isPulsing = true;
        while (transform.localScale.x < maxScale.x-.01f) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, maxScale, 5f);
            yield return null;
        }
        while (transform.localScale.x > minScale.x+0.1f) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, minScale, 1.5f);
            yield return null;
        }
        transform.localScale = minScale;
        isPulsing = false;
    }
}
