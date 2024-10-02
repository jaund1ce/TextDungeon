using System;
using System.Collections.Generic;
using TextDungeon;

class Program
{

    static Player player;
    static QuestManager questManager;
    public static Item[] ItemDb;

    static void Main(string[] args)
    {
        SetData();
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
    static void SetData() //상점물건등록
    {
        ItemDb = new Item[]
        {
            new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
            new Item("무쇠갑옷", 1, 9,"무쇠로 만들어져 튼튼한 갑옷입니다. ",2000),
            new Item("스파르타의 갑옷", 1, 15,"스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500),
            new Item("낡은 검", 0, 2,"쉽게 볼 수 있는 낡은 검 입니다. ",600),
            new Item("청동 도끼", 0, 5,"어디선가 사용됐던거 같은 도끼입니다. ",1500),
            new Item("스파르타의 창", 0, 7,"스파르타의 전사들이 사용했다는 전설의 창입니다. ",2500),
        };
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
            case 2:
                InventoryUI();
                break;
            case 3:
                ShopUI();
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
        Console.WriteLine($"보유물약: {player.Potion} 개");
        Console.WriteLine($"처치한 몬스터 수: {player.MonsterKills}");
        Console.WriteLine($"장비 장착 여부: {(player.IsEquipped ? "장착함" : "장착 안함")}");
        Console.WriteLine();
        Console.WriteLine("계속하려면 아무 키나 누르세요...");
        Console.ReadKey();
    }

    static void InventoryUI()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리합니다.");
        Console.WriteLine("[ 아이템 목록 ]");

        player.DisplayInventory(false);

        Console.WriteLine();
        Console.WriteLine("1.장착관리");
        Console.WriteLine("2.물약 사용");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int result = CheckInput(0, 2);

        switch (result)
        {
            case 0:
                DisplayMainUI();  // 나가기 선택 시 메인 화면으로 이동
                break;

            case 1:
                EquipUI();
                break;
            case 2:
                HealUI();
                InventoryUI();
                break;
            default:
                Console.WriteLine("다시 입력해주세요");
                Console.ReadLine();
                InventoryUI();
                break;
        }
    }

    static void EquipUI()
    {
        Console.Clear();
        Console.WriteLine("인벤토리 - 장착관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        player.DisplayInventory(true);

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, player.InventoryCount);

        switch (result)
        {
            case 0:
                InventoryUI();
                break;
            default:

                Item targetitem = player.getinventoryItem(result);
                player.EquipItem(targetitem);
                EquipUI();
                break;
        }
    }

    static void HealUI()
    {
        Console.Clear();
        Console.WriteLine("체력을 회복시키시겠습니까?");
        Console.WriteLine($"현재 물약 개수 {player.Potion} ");
        Console.WriteLine($"현재체력 : {player.Health}");
        Console.WriteLine("1.네");
        Console.WriteLine("2.아니요");

        int result = CheckInput(1, 2);

        switch (result)
        {
            case 1:
                if (player.Potion <= 0)
                {
                    player.Potion = 0;
                    Console.WriteLine();
                    Console.WriteLine("포션이 없습니다");
                    Console.ReadLine();
                }
                else
                {
                    player.Health += 30;
                    if (player.Health >= 100)
                    {
                        player.Health = 100;
                    }
                    player.Potion -= 1;
                    Console.Clear();
                    Console.WriteLine("체력이 회복되었습니다");
                    Console.WriteLine($"현재체력 : {player.Health}");
                    HealUI();
                }
                break;
            case 2:
                Console.WriteLine("뒤로 돌아갑니다.");
                Console.ReadLine();
                break;
            default:
                Console.WriteLine("잘못된 입력입니다."); 
                Console.ReadLine();
                HealUI();
                break;
        }
    } 
        static void ShopUI()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < ItemDb.Length; i++)
            {
                Item curItem = ItemDb[i];

                string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.itemPrice} G");
                Console.WriteLine($"- {i+1} . {curItem.ItemInfoText()}  |  {displayPrice}");
            }
        
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("구매하고 싶은 장비의 번호를 입력해주세요");
            Console.WriteLine("0. 나가기");
            Console.Write(">>");

            int result = CheckInput(0, ItemDb.Length);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                default:
                    int itemidx = result - 1;
                    Item targetItem = ItemDb[itemidx];
                    //이미 구매한 아이템이면
                    if (player.HasItem(targetItem))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        Console.WriteLine("Enter 를 눌러주세요.");
                        Console.ReadLine();
                    }
                    else //구매가 가능하면
                    {
                        //소지금이 충족될떄
                        if (player.Gold >= targetItem.itemPrice)
                        {
                            Console.WriteLine("구매완료");
                            player.BuyItem(targetItem);
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다");
                            Console.WriteLine("Enter 를 눌러주세요.");
                            Console.ReadLine();
                        }
                    }
                    ShopUI();
                    break;
            }
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
