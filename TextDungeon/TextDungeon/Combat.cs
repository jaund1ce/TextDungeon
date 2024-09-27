using System;
using System.Collections.Generic;

public class Combat
{
    private Player player;
    private List<Monster> monsters;
    private List<string> combatLog;  // 전투 로그 리스트
    private const int maxLogSize = 5;  // 한 번에 출력할 최대 로그 개수
    private Action returnToStatUI;


    public Combat(Player player, Action returnToStatUI)
    {
        this.player = player;
        this.returnToStatUI = returnToStatUI;
        combatLog = new List<string>();  // 로그 리스트 초기화
        StartBattle();
    }

    private void StartBattle()
    {
        Console.Clear();
        Console.WriteLine("전투를 시작합니다!");

        // 1~4 마리의 몬스터를 랜덤하게 생성
        Random rand = new Random();
        int monsterCount = rand.Next(1, 5);
        monsters = new List<Monster>();

        for (int i = 0; i < monsterCount; i++)
        {
            monsters.Add(CreateRandomMonster(rand));
        }

        DisplayMonsters();
        PlayerAttackPhase();
    }

    private Monster CreateRandomMonster(Random rand)
    {
        int monsterType = rand.Next(1, 4);
        switch (monsterType)
        {
            case 1:
                return new Monster("Lv2 미니언", 2, 15, 5);
            case 2:
                return new Monster("Lv3 공허충", 3, 10, 9);
            case 3:
                return new Monster("Lv5 대포미니언", 5, 25, 8);
            default:
                return null;
        }
    }

    private void DisplayMonsters()
    {
        Console.WriteLine("몬스터들이 나타났습니다:");
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{i + 1}. Dead");
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {monsters[i].HP}, ATK: {monsters[i].Attack})");
            }
        }
        Console.ResetColor();
    }

    private void PlayerAttackPhase()
    {
        Console.Clear();

        // 전투 로그 출력
        DisplayCombatLog();

        Console.WriteLine("플레이어의 공격 차례입니다.");
        DisplayMonsters();

        Console.WriteLine("공격할 몬스터 번호를 선택해주세요.");
        Console.Write(">>");

        int result = CheckInput(1, monsters.Count);

        Monster selectedMonster = monsters[result - 1];
        if (selectedMonster.IsDead)
        {
            Console.WriteLine("잘못된 입력입니다. 이미 죽은 몬스터입니다.");
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();  // 사용자 입력 대기
            PlayerAttackPhase();
        }
        else
        {
            // 공격력 계산 (10% 오차)
            Random rand = new Random();
            int variance = (int)Math.Ceiling(player.Attack * 0.1);
            int finalAttack = player.Attack + rand.Next(-variance, variance + 1);
            selectedMonster.HP -= finalAttack;

            string attackLog = $"{selectedMonster.Name}에게 {finalAttack}의 데미지를 입혔습니다!";
            AddCombatLog(attackLog);  // 로그를 추가할 때 AddCombatLog 사용

            // 몬스터가 죽었을 경우 로그 추가
            if (selectedMonster.HP <= 0)
            {
                string deathLog = $"{selectedMonster.Name}이(가) 죽었습니다.";
                AddCombatLog(deathLog);  // 죽음 로그를 추가

                // 전투 종료 직전 모든 몬스터가 죽었는지 확인
                if (monsters.TrueForAll(m => m.IsDead))
                {
                    Console.Clear();
                    AddCombatLog("모든 몬스터를 처치했습니다! 승리!");  // 승리 로그 추가

                    // 최종 로그와 함께 전투 종료
                    DisplayCombatLog();
                    EndBattle();
                    return;
                }
            }

            EnemyPhase();
        }
    }

    private void EnemyPhase()
    {
        // 턴 시작 시 화면을 클리어
        Console.Clear();

        // 전투 로그 출력
        DisplayCombatLog();

        Console.WriteLine("몬스터들의 차례입니다.");
        DisplayMonsters();

        foreach (var monster in monsters)
        {
            if (!monster.IsDead)
            {
                player.Health -= monster.Attack;
                string monsterAttackLog = $"{monster.Name}이(가) {monster.Attack}의 데미지를 입혔습니다! 플레이어의 남은 체력: {player.Health}";
                Console.WriteLine(monsterAttackLog);
                combatLog.Add(monsterAttackLog);  // 로그에 추가
            }
        }

        if (player.Health <= 0)
        {
            Console.WriteLine("플레이어가 죽었습니다. 게임에서 패배했습니다.");
            EndBattle();  // 전투 종료 후 StatUI로 돌아감
            return;
        }

        PlayerAttackPhase();
    }

    private void EndBattle()
    {
        Console.WriteLine("전투가 끝났습니다. 계속하려면 아무 키나 누르세요...");
        Console.ReadKey();  // 사용자의 키 입력을 기다림
        returnToStatUI();  // StatUI로 돌아감
    }

    private int CheckInput(int min, int max)
    {
        int result;
        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber && result >= min && result <= max)
            {
                return result;  // 유효한 입력일 경우 바로 리턴
            }
            else
            {
                // 잘못된 입력일 때 메시지 출력 후 대기
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey();  // 사용자 입력 대기
            }
        }
    }

    // 전투 로그를 출력하는 함수
    private void DisplayCombatLog()
    {
        Console.WriteLine("===== 전투 로그 =====");

        // 로그 리스트에서 최신 maxLogSize개만 출력
        int start = Math.Max(0, combatLog.Count - maxLogSize);
        for (int i = start; i < combatLog.Count; i++)
        {
            Console.WriteLine(combatLog[i]);
        }

        Console.WriteLine("=====================");
        Console.WriteLine();
    }

    // 로그를 추가하는 함수
    private void AddCombatLog(string log)
    {
        combatLog.Add(log);

        // 로그 리스트가 maxLogSize를 초과하면 가장 오래된 로그 삭제
        if (combatLog.Count > maxLogSize)
        {
            combatLog.RemoveAt(0);  // 가장 오래된 로그 삭제
        }
    }
}
