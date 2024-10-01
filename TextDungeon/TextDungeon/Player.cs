using System;
using System.Collections.Generic;  // List를 사용하기 위한 네임스페이스
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
        public int Gold { get; set; }  // 골드는 외부에서 증가시키기 위해 set 변경
        public int MonsterKills { get; set; }  // 처치한 몬스터 수
        public bool IsEquipped { get; set; }

        // 인벤토리와 장비 리스트
        private static List<Item> Inventory = new List<Item>();
        private static List<Item> EquipList = new List<Item>();

        // 인벤토리 카운트 프로퍼티
        public int InventoryCount
        {
            get
            {
                return Inventory.Count;
            }
        }

        // 인벤토리 출력 메서드
        public void DisplayInventory(bool showldx)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Item targetItem = Inventory[i];
                string displayEquipped = IsEquipItem(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayEquipped} {targetItem.ItemInfoText()}");
            }
        }

        // 아이템 장착/해제 메서드
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

        // 아이템 장착 여부 확인 메서드
        public bool IsEquipItem(Item item)
        {
            return EquipList.Contains(item);
        }

        // 아이템 보유 여부 확인 메서드
        public bool HasItem(Item item)
        {
            return Inventory.Contains(item);
        }

        // 아이템 구매 메서드
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

        // 생성자: 플레이어의 기본 정보 설정
        public Player(string name, string job)
        {
            Name = name;
            Job = job;
            Level = 1;
            MonsterKills = 0;
            IsEquipped = false;
            Gold = 50000;  // 초기 골드 설정
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

        // 골드 추가 메서드
        public void AddGold(int amount)
        {
            Gold += amount;
        }

        // 골드 감소 메서드
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

        // 아이템 추가 메서드 (보상으로 아이템을 받을 때 사용)
        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{item.itemName}이(가) 인벤토리에 추가되었습니다.");
        }
    }
}
