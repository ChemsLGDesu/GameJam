using UnityEngine;

public interface IAttack
{
    public void Attack();
}

public interface IDamage
{
    public void ReceiveDamage(int damage);
}