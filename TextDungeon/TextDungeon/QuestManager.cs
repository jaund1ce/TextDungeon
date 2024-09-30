using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDungeon
{
    using System;
    using System.Collections.Generic;

    public class QuestManager
    {
        private List<Quest> quests;
        private Player player;

        public QuestManager(Player player)
        {
            this.player = player;
            quests = new List<Quest>();
        }

        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        public void DisplayQuestSelection()
        {
            Console.Clear();
            Console.WriteLine("퀘스트 목록:");
            for (int i = 0; i < quests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quests[i].Title} - {(quests[i].IsCompleted ? "완료됨" : "진행 중")}");
            }
            Console.WriteLine("\n0. 나가기");
            Console.Write(">> ");

            int choice = Program.CheckInput(0, quests.Count);

            if (choice == 0)
            {
                Program.DisplayMainUI();
                return;
            }
            else
            {
                DisplayQuestDetails(quests[choice - 1]);
            }
        }

        // 선택된 퀘스트의 세부사항 표시
        private void DisplayQuestDetails(Quest quest)
        {
            Console.Clear();
            quest.DisplayQuestInfo();

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
            DisplayQuestSelection(); // 퀘스트 선택 메뉴로 돌아감
        }

        public void CheckQuestCompletion()
        {
            foreach (var quest in quests)
            {
                if (!quest.IsCompleted && quest.CheckIfCompleted(player))
                {
                    Console.WriteLine($"퀘스트 완료: {quest.Title}");
                    Console.WriteLine($"{quest.RewardGold} 골드를 획득했습니다!");
                    player.AddGold(quest.RewardGold);
                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey();
                }
            }
        }
    }
}
