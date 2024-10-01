using System;
using System.Xml.Linq;
namespace TextDungeon
{

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


        private static List<Item> Inventory = new List<Item>();
        private static List<Item> EquipList = new List<Item>();
        public int InventoryCount
        {
            get
            {
                return Inventory.Count;
            }
        }
        public void DisplayInventory(bool showldx)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Item targetItem = Inventory[i];

                string displayEquipped = IsEquipItem(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayEquipped} {targetItem.ItemInfoText()}");
            }
        }


        public void EquipItem(Item item)
        {
            if (IsEquipItem(item))
            {
                EquipList.Remove(item);
                if (item.itemType == 0)
                    Attack -= item.itemValue; // 공격 아이템 해제
                else
                    Defense -= item.itemValue; // 방어 아이템 해제
            }
            else
            {
                EquipList.Add(item);
                if (item.itemType == 0)
                    Attack += item.itemValue; // 공격 아이템 장착
                else
                    Defense += item.itemValue; // 방어 아이템 장착
            }
        }

        public bool IsEquipItem(Item item)
        {
            return EquipList.Contains(item);
        }

        public bool HasItem(Item item)
        {
            return Inventory.Contains(item);
        }

        // BuyItem 메서드 수정: bool 반환하고, 아이템 구매 여부를 반환.
        public bool BuyItem(Item item)
        {
            if (Gold >= item.itemPrice)
            {
                Gold -= item.itemPrice;
                Inventory.Add(item);
                return true; // 성공적으로 구매했을 경우
            }
            else
            {
                return false; // 골드가 부족한 경우
            }
        }

        // 두 개의 인수를 받는 생성자 추가
        public Player(string name, string job)
        {
            Name = name;
            Job = job;
            Level = 1;
            MonsterKills = 0;
            IsEquipped = false;
            Gold = 50000;
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
}