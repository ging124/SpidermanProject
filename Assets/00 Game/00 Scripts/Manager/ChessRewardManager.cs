using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessRewardManager : MonoBehaviour
{
    public Chest chessPrefab;
    public Transform chessTransformHolder;
    public UI_SelectButton uiSelectButton;


    [ContextMenu("SpwanChess")]
    public void SpwanChess()
    {
        foreach (Transform transform in chessTransformHolder)
        {
            var chestController = chessPrefab.Spawn(transform.position, Quaternion.identity, this.transform).GetComponent<ChestController>();
            chestController.uiSelectButton = uiSelectButton;
        }
    }
}
