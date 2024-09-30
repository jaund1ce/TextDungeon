using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDungeon
{
    public class Quest
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public int RewardGold { get; private set; }
        private Func<Player, bool> completionCondition;

        public Quest(string title, string description, int rewardGold, Func<Player, bool> completionCondition)
        {
            Title = title;
            Description = description;
            RewardGold = rewardGold;
            this.completionCondition = completionCondition;
            IsCompleted = false;
        }

        public bool CheckIfCompleted(Player player)
        {
            if (!IsCompleted && completionCondition(player))
            {
                IsCompleted = true;
                return true;
            }
            return false;
        }

        public void DisplayQuestInfo()
        {
            Console.WriteLine($"퀘스트: {Title}");
            Console.WriteLine($"설명: {Description}");
            Console.WriteLine($"보상: {RewardGold} 골드");
            Console.WriteLine($"상태: {(IsCompleted ? "완료됨" : "진행 중")}");
        }
    }
}
