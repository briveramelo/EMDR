using UnityEngine;
using System.Collections;

public enum BlockSpot {
    TopLeft = 0,
    TopRight = 1,
    BottomLeft = 2,
    BottomRight = 3
}
public class BlockActivator : MonoBehaviour {

    [SerializeField] Block[] blocks;
    [SerializeField] BlockSpot lastActiveBlock;

    public float selectNewBlockTime;


    void Awake() {
        StartCoroutine (PulseNewBlock());
    }

    IEnumerator PulseNewBlock() {
        BlockSpot newSpot = FindNewBlock();
        blocks[(int)newSpot].StartPulse();
        yield return new WaitForSeconds(selectNewBlockTime);
        StartCoroutine (PulseNewBlock());
    }

    BlockSpot FindNewBlock() {
        BlockSpot potentialNextBlock= (BlockSpot)Random.Range(0, 4);
        if (blocks[(int)potentialNextBlock].isSelected) {
            return FindNewBlock();
        }
        Debug.Log("Selected Block: " + blocks[(int)potentialNextBlock].name);
        return potentialNextBlock;
    }
}
