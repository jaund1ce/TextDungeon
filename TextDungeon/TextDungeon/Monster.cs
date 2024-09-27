using System;

public class Monster
{
    public string Name { get; }
    public int Health { get; private set; }
    public int AttackPower { get; }
    public int Level { get; }

    public Monster(string name, int health, int attackPower, int level)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        Level = level;
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public int Attack()
    {
        Random rand = new Random();
        return rand.Next(AttackPower - 1, AttackPower + 2); // 공격력 10% 오차
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}
