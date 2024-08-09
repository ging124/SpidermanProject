using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffectController : ItemWorld
{
    private void Start()
    {
        var main = this.GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        itemData.Despawn(this.gameObject);
    }
}
