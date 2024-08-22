using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessRewardManager : MonoBehaviour
{
    public Chess chessPrefab;
    public Transform chessTransformHolder;

    [ContextMenu("SpwanChess")]
    public void SpwanChess()
    {
        foreach (Transform transform in chessTransformHolder)
        {
            chessPrefab.Spawn(transform.position, Quaternion.identity, this.transform);
        }
    }
}
