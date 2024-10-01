using System;
namespace TextDungeon
{
    public class Monster
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int HP { get; set; }
        public int Attack { get; private set; }
        public bool IsDead => HP <= 0;

        public Monster(string name, int level, int hp, int attack)
        {
            Name = name;
            Level = level;
            HP = hp;
            Attack = attack;
        }
    }
}