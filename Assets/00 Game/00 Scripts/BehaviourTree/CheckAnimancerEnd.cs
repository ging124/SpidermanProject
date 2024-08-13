using Animancer;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

[Category("CustomConditionTask")]
public class CheckAnimancerEnd : ConditionTask<AnimancerComponent>
{
    public AnimancerState _state;

    protected override string info => "Animancer End";


    //<summary>Override to do things when condition is enabled</summary>
    override protected void OnEnable() { }

    ///<summary>Override to do things when condition is disabled</summary>
    override protected void OnDisable() { }

    ///<summary>Override to return whether the condition is true or false. The result will be inverted if Invert is checked.</summary>
    override protected bool OnCheck() 
    {
        _state = agent.States.Current;
        if(_state.NormalizedTime >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
