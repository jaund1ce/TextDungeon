using System;

class Program
{
    static Player player;

    static void Main(string[] args)
    {
        StartUI();
    }

    static void StartUI()
    {
        Console.Clear();
        Console.WriteLine("원하시는 이름을 입력해주세요.");
        Console.Write(">>");
        string playerName = Console.ReadLine();
        Console.WriteLine("직업을 선택하세요");
        Console.WriteLine();
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 도적");
        Console.WriteLine("3. 궁수");
        Console.WriteLine();
        Console.WriteLine("원하시는 직업을 선택해주세요.");
        Console.Write(">> ");
        int result = CheckInput(1, 3);
        switch (result)
        {
            case 1:
                string playerJob1 = result.ToString("전사");
                player = new Player(playerName, playerJob1);
                DisplayStatUI();
                break;
            case 2:
                string playerJob2 = result.ToString("도적");
                player = new Player(playerName, playerJob2);
                DisplayStatUI();
                break;
            case 3:
                string playerJob3 = result.ToString("궁수");
                player = new Player(playerName, playerJob3);
                DisplayStatUI();
                break;
        }
    }

    static void DisplayMainUI()
    { 
        Console.Clear();
        Console.WriteLine($"{player.Name}님! 스파르타 마을에 오신걸 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 전투 시작");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int result = CheckInput(1, 4);

        switch (result)
        {
            case 1:
                DisplayStatUI();
                break;
            case 4:
                // Combat 클래스에 전투 종료 후 메인 화면으로 돌아가는 델리게이트 전달
                Combat combat = new Combat(player, DisplayMainUI);
                break;
        }
    }

    static void DisplayStatUI()
    {
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"플레이어 : {player.Name}");
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
                DisplayMainUI();  // 나가기 선택 시 메인 화면으로 이동
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
