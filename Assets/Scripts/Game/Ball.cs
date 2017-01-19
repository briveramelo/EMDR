using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class Tag {
    public static readonly string wall = "Wall";
    public static readonly string floor = "Floor";
    public static readonly string ball = "Ball";
}

public static class Colors {

    public static List<Color> colors = new List<Color>() {
        Color.red, Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow
    };
    public static Color GetRandomColor(Color currentColor) {
        int guess = Random.Range(0, colors.Count);
        while (colors[guess] == currentColor) {
            guess = Random.Range(0, colors.Count);
        }
        return colors[guess];
    }

    public static float GetColorDifference(Color color1, Color color2) {
        float dif = Mathf.Abs(color1.a - color2.a);
        dif += Mathf.Abs(color1.r - color2.r);
        dif += Mathf.Abs(color1.g - color2.g);
        dif += Mathf.Abs(color1.b - color2.b);
        return dif;
    }
}

public class Ball : MonoBehaviour {

    [SerializeField] Rigidbody2D rigbod;
    [SerializeField] AudioClip bilateralClip;
    [SerializeField, Range(20, 50)] float moveSpeed;
    [SerializeField] SpriteRenderer mySprite;

    void Start() {
        rigbod.velocity = GetRandomStartVelocity();
        targetColor = Colors.GetRandomColor(Color.white);
    }

    Vector2 GetRandomStartVelocity() {
        float y = Random.Range(.2f, 0.5f);
        float x = Random.Range(0.5f, 1f);
        Vector2 vel = new Vector2(x, y).normalized;
        //return vel * moveSpeed;
        return new Vector2(0.85f, 0.5f).normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == Tag.wall && !isWaitingToHitWall) {
            HitWall();   
            StartCoroutine(WaitToHitWall());
        }
        if (col.tag == Tag.floor && !isWaitingToHitFloor) {
            HitFloor();
            StartCoroutine(WaitToHitFloor());
        }        
    }

    bool isWaitingToHitWall;
    bool isWaitingToHitFloor;
    IEnumerator WaitToHitWall() {
        isWaitingToHitWall = true;
        yield return new WaitForSeconds(.5f);
        isWaitingToHitWall = false;
    }
    IEnumerator WaitToHitFloor() {
        isWaitingToHitFloor = true;
        yield return new WaitForSeconds(.5f);
        isWaitingToHitFloor = false;
    }

    void HitWall() {
        rigbod.velocity = new Vector2(-rigbod.velocity.x, rigbod.velocity.y);
        Vector3 addedPosition = -(rigbod.velocity.x > 0 ? 1 : -1) * Vector3.right * 5;
        Hit(ref addedPosition);
    }

    void HitFloor() {
        rigbod.velocity = new Vector2(rigbod.velocity.x, -rigbod.velocity.y);
        Vector3 addedPosition = -(rigbod.velocity.y > 0 ? 1 : -1) * Vector3.up * 10;
        Hit(ref addedPosition);
    }

    void Hit(ref Vector3 addedPosition) {
        Vector3 clipLocation = Camera.main.transform.position + addedPosition;
        AudioSource.PlayClipAtPoint(bilateralClip, clipLocation);
        mySprite.color = Colors.GetRandomColor(mySprite.color);
        targetColor = Colors.GetRandomColor(targetColor);
    }

    Color targetColor;
    void Update() {
        mySprite.color = Color.Lerp(mySprite.color, targetColor, 0.06f);
        if (Colors.GetColorDifference(mySprite.color, targetColor) < .05f) {
            targetColor = Colors.GetRandomColor(targetColor);
        }
    }
}
