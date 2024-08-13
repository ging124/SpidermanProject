using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.MLAgents;
using UnityEngine;

[Category("CustomConditionTask")]
public class HaveTargetInRange : ConditionTask<Transform>
{
    public float range;
    public LayerMask targetLayer;

    protected override string info => $"have target in {range} m";


    //<summary>Override to do things when condition is enabled</summary>
    override protected void OnEnable() { }

    ///<summary>Override to do things when condition is disabled</summary>
    override protected void OnDisable() { }

    ///<summary>Override to return whether the condition is true or false. The result will be inverted if Invert is checked.</summary>
    override protected bool OnCheck()
    {
        Collider[] targetList = Physics.OverlapSphere(agent.transform.position, range, targetLayer);
        if (targetList.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
