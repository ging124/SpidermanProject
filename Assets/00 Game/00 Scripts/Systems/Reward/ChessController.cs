using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessController : ItemWorld
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private ClipTransition openChessAnim;

    private void Awake()
    {
        animancer = this.GetComponent<AnimancerComponent>();
    }
}
