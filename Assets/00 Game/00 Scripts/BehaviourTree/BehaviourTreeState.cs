using NodeCanvas.Framework;
using ParadoxNotion.Design;

[Category("GeneralAction")]
[Description("Run a state")]
public class BehaviourTreeState : ActionTask<StateManager>
{

    public BaseState state;

    //Use for initialization. This is called only once in the lifetime of the task.
    //Return null if init was successfull. Return an error string otherwise
    protected override string OnInit()
    {
        return null;
    }

    protected override string info => state.ToString();


    //This is called once each _time the task is enabled.
    //Call EndAction() to mark the action as finished, either in success or failure.
    //EndAction can be called from anywhere.
    protected override void OnExecute()
    {
        agent.ChangeState(state, true);
    }

    //Called once per frame while the action is active.
    protected override void OnUpdate()
    {
        StateStatus result = agent.OnUpdate();
        if (result == StateStatus.Success)
        {
            EndAction(true);
        }
        else if (result == StateStatus.Failure)
        {
            EndAction(false);
        }
    }

    //Called when the task is disabled.
    protected override void OnStop()
    {

    }

    //Called when the task is paused.
    protected override void OnPause()
    {

    }
}
