using UnityEngine;

public class Creature
{
    public string Name { get; protected set; }
    public int Hp { get; protected set; }
    public int Atk { get; protected set; }
    public bool IsDead => Hp <= 0;

    public Creature(string name, int hp, int atk)
    {
        Name = name;
        Hp = hp;
        Atk = atk;
    }

    public virtual void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0) Hp = 0;
    }

    public virtual int GetCalculateDamage()
    {
        int minDamage = Mathf.FloorToInt(Atk * 0.8f);
        int maxDamage = Mathf.FloorToInt(Atk * 1.2f);
        return Random.Range(minDamage, maxDamage + 1);
    }
}

public class Player : Creature
{
    public int MaxHp { get; private set; }

    public Player(string name, int hp, int atk) : base(name, hp, atk)
    {
        MaxHp = hp;
    }

    public void Heal(int amount)
    {
        Hp += amount;
        if (Hp > MaxHp) Hp = MaxHp;
    }
}

public class Monster : Creature
{
    public Monster(string name, int hp, int atk) : base(name, hp, atk)
    {
    }
}