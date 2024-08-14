
public interface IHitable
{
    public void OnHit(int hitDamage, AttackType attackType){ }

    public bool CanHit();
}

public enum AttackType
{
    None,
    NormalAttack,
    HeavyAttack
}

