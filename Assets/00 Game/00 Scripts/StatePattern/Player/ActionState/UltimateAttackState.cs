using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackState : NormalState
{
    [SerializeField] private ParticleSystem _ultimateEffect;

    [SerializeField] private ClipTransition _ultimateAnim;
    [SerializeField] private float _timeToAttack = 0.15f;
    [SerializeField] private float _ultimateHit = 1;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.SetMovementDirection(Vector3.zero);
        _normalBodyLayer.Play(_ultimateAnim).Events.OnEnd = () =>
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

        MissAttack();

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
            Collider[] ultimateTarget = Physics.OverlapSphere(_blackboard.playerController.transform.position, _blackboard.playerController.ultimateRange, ~_blackboard.playerController.friendLayer);

            foreach (Collider hit in ultimateTarget)
            {
                if (hit.gameObject == _blackboard.playerController.gameObject) continue;

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

    public void MissAttack()
    {
        if (_blackboard.playerController.hitDamage != 0)
        {
            _blackboard.playerController.missPrefab.Spawn(_blackboard.playerController.transform.position + Vector3.up, _blackboard.playerController.hitDamage);
        }
    }

}
