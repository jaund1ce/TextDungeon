using System;

public class Player
{
    public string Name { get; private set; }
    public string Job { get; private set; }
    public int Level { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Health { get; set; }
    public int Gold { get; private set; }

    public Player(string name, string job, int level = 1, int attack = 10, int defense = 5, int health = 100, int gold = 1500)
    {
        Name = name;
        Job = job;
        Level = level;
        Attack = attack;
        Defense = defense;
        Health = health;
        Gold = gold;
    }
}