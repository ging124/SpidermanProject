using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackState : NormalState
{
    [SerializeField] private ParticleSystem _ultimateEffect;

    [SerializeField] private ClipTransition _webShooterAnim;
    [SerializeField] private float _timeToAttack = 0.15f;
    [SerializeField] private float _ultimateHit = 1;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_webShooterAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
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
                    var damage = RPGObject.RandomDamage(_blackboard.playerController.playerData.attackDamage * 3);

                    AttackType attackType = (i == _ultimateHit - 1) ? AttackType.HeavyAttack : AttackType.NormalAttack;
                    hitable.OnHit(damage, attackType);

                    _blackboard.playerController.damagePrefab.Spawn(hit.transform.position + Vector3.up, damage);
                    var hitEffect = _blackboard.playerController.attackHitEffect;
                    hitEffect.Spawn(hit.transform.position, Quaternion.identity, null);
                }
            }

            yield return new WaitForSeconds(_timeToAttack);
        }
    }

}
