using UnityEngine;
using System.Collections.Generic;

public class EffectsManager : MonoBehaviour {

    public static EffectsManager Instance;

    [SerializeField] List<GameObject> effects;
    Transform ball;
    void Awake() {
        Instance = this;
        ball = GameObject.FindGameObjectWithTag(Tag.ball).transform;
    }

    public void Explode(int streakCount) {
        if (streakCount >= effects.Count) {
            streakCount = effects.Count-1;
            //add bonus
        }
        GameObject effect = Instantiate(effects[streakCount], ball.position, Quaternion.identity, ball) as GameObject;
        Destroy(effect, 1f);
    }
}
