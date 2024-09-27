using System;

public class Player
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; } = 1; 
    public int Attack { get; set; } = 10;
    public int Defense { get; set; } = 5;
    public int Health { get; set; } = 100;
    public int Gold { get; set; } = 1500;

    public Player(string name, string job, int level, int attack, int defense, int health, int gold)
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
