using System;
using TextDungeon;

using System;

class Program
{
    static Player player;
    static QuestManager questManager;
    static void Main(string[] args)
    {
        StartUI();
        questManager = new QuestManager(player);

        // 퀘스트 추가
        questManager.AddQuest(new Quest("마을을 위협하는 미니언 처치", "마을을 위협하는 미니언을 처치하세요.", 100, (p) => p.MonsterKills >= 1));
        questManager.AddQuest(new Quest("장비를 장착해보자", "새로운 장비를 장착해보세요.", 50, (p) => p.IsEquipped));
        questManager.AddQuest(new Quest("더욱 더 강해지기!", "레벨을 올려 더욱 강해지세요.", 200, (p) => p.Level >= 2));

        // 상태 표시 및 메인 UI 호출
        DisplayStatUI();
        DisplayMainUI();
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
        string playerJob = "";

        switch (result)
        {
            case 1:
                playerJob = "전사";
                break;
            case 2:
                playerJob = "도적";
                break;
            case 3:
                playerJob = "궁수";
                break;
        }

        player = new Player(playerName, playerJob);
    }

    public static void DisplayMainUI()
    {
        questManager.CheckQuestCompletion(); // 퀘스트 완료 여부 확인

        Console.Clear();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 전투 시작");
        Console.WriteLine("5. 퀘스트 확인");
        Console.WriteLine("6. 종료");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int result = CheckInput(1, 6);

        switch (result)
        {
            case 1:
                DisplayStatUI();
                DisplayMainUI(); // 메인 UI로 돌아가기
                break;
            case 4:
                // Combat 클래스에 전투 종료 후 메인 화면으로 돌아가는 델리게이트 전달
                Combat combat = new Combat(player, DisplayMainUI);
                break;
            case 5:
                questManager.DisplayQuestSelection();
                break;
            case 6:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("잘못된 선택입니다.");
                Console.ReadKey();
                DisplayMainUI();
                break;
        }
    }

    static void DisplayStatUI()
    {
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"이름: {player.Name}");
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"직업: {player.Job}");
        Console.WriteLine($"공격력: {player.Attack}");
        Console.WriteLine($"방어력: {player.Defense}");
        Console.WriteLine($"체력: {player.Health}");
        Console.WriteLine($"골드: {player.Gold} G");
        Console.WriteLine($"처치한 몬스터 수: {player.MonsterKills}");
        Console.WriteLine($"장비 장착 여부: {(player.IsEquipped ? "장착함" : "장착 안함")}");
        Console.WriteLine();
        Console.WriteLine("계속하려면 아무 키나 누르세요...");
        Console.ReadKey();
    }

    public static int CheckInput(int min, int max)
    {
        int result;
        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber && result >= min && result <= max)
                return result;

            Console.WriteLine("잘못된 입력입니다!!!!");
        }
    }
}

