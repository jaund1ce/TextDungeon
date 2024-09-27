﻿using System;
using System.Security.Principal;

class Program
{

    // 화면 만들기 - 메인화면
    static Player player;
    static void Main(string[] args)
    {
        DisplayMainUI();
    }
    static void DisplayMainUI()
    {
        Console.Clear();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int result = CheckInput(1, 3);

        switch (result)
        {
            case 1:
                DisplayStatUI();
                break;

            case 2:
                break;

            case 3:
                break;
        }
    }
    static void DisplayStatUI()
    {
        player = new Player("StartPlayer");
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"Chad {player.Job}");
        Console.WriteLine($"공격력 : {player.Attack}");
        Console.WriteLine($"방어력 : {player.Defense}");
        Console.WriteLine($"체 력 : {player.Health}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        int result = CheckInput(0, 0);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;
        }
    }
    static int CheckInput(int min, int max)
    {
        int result;
        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                    return result;
            }
            Console.WriteLine("잘못된 입력입니다!!!!");
        }
    }
}