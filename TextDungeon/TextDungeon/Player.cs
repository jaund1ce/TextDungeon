using System;

public class Player
{
    public string Name { get; private set; }
    public string Job { get; private set; }
    public int Level { get; set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Health { get; set; }
    public int Gold { get; private set; }
    public int MonsterKills { get; set; }
    public bool IsEquipped { get; set; }

    // 두 개의 인수를 받는 생성자 추가
    public Player(string name, string job)
    {
        Name = name;
        Job = job;
        Level = 1;
        MonsterKills = 0;
        IsEquipped = false;
        Gold = 50;
        Health = 100;

        // 직업에 따른 스탯 설정
        switch (job)
        {
            case "전사":
                Attack = 15;
                Defense = 10;
                break;
            case "도적":
                Attack = 12;
                Defense = 8;
                break;
            case "궁수":
                Attack = 10;
                Defense = 5;
                break;
            default:
                Attack = 10;
                Defense = 5;
                break;
        }
    }

    // Gold를 증가시키는 메서드
    public void AddGold(int amount)
    {
        Gold += amount;
    }

    // Gold를 감소시키는 메서드
    public bool SubtractGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            return true;
        }
        else
        {
            return false; // 골드가 부족할 경우
        }
    }
}
