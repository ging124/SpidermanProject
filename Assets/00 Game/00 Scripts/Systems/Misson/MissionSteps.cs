using System.Collections.Generic;
using UnityEngine;

public class MissionSteps : ScriptableObject
{
    [Header("ChangeMissionEvent")]
    public GameEvent completeStep;

    public virtual bool CheckCompleteStep()
    {
        return true;
    }

    public virtual bool CheckCompleteStep(int objectCount)
    {
        return true;
    }

    public virtual bool CheckFailedStep()
    {
        return false;
    }

    public virtual void InstantiateStep(Vector3 position, Transform parent)
    {
    }

    public virtual void UpdateStep(Transform parent)
    {

    }
}
