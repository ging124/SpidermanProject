using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackState : ActionState
{
    [SerializeField] private ParticleSystem _ultimateEffect;

    [SerializeField] private ClipTransition _webShooterAnim;
    [SerializeField] private float _timeToAttack = 0.15f;
    [SerializeField] private float _ultimateHit = 0.15f;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_webShooterAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.normalActionState);
        };
        
    }
    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _ultimateEffect.gameObject.SetActive(false);
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }

    public void UltimateSkill()
    {
        _ultimateEffect.gameObject.SetActive(true);
        StartCoroutine(UseUltimateSkill());
    }

    public IEnumerator UseUltimateSkill()
    {
        for (int i = 0; i < _ultimateHit; i++)
        {
            Collider[] ultimateTarget = Physics.OverlapSphere(_blackboard.playerController.transform.position, _blackboard.playerController.ultimateRange, ~LayerMask.NameToLayer("Player"));

            foreach (Collider hit in ultimateTarget)
            {
                if (hit == _blackboard.playerController.GetComponent<Collider>()) continue;

                IHitable hitable;
                if (hit.TryGetComponent<IHitable>(out hitable))
                {
                    var damage = _blackboard.playerController.playerData.RandomDamage(_blackboard.playerController.playerData.attackDamage);
                    hitable.OnHit(damage * 3, AttackType.HeavyAttack);
                }
            }

            yield return new WaitForSeconds(_timeToAttack);
        }
    }

}
