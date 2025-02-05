using System;
using System.Collections.Generic;

namespace TXT_Dungeon
{
    class Program
    {
        // 플레이어 능력치
        static int level = 1;
        static string name = "Chad";
        static string job = "전사";
        static int attack = 10;
        static int defense = 5;
        static int health = 100;
        static int gold = 1500;

        // 소유한 아이템 목록
        static List<Item> inventory = new List<Item>();

        // 상점 아이템 목록 
        static List<Item> storeItems = new List<Item>()
        {
            new Item("수련자 갑옷", "방어력 +5 | 수련에 도움을 주는 갑옷입니다.", 1000, false, 5, 0),
            new Item("무쇠갑옷", "방어력 +9 | 무쇠로 만들어져 튼튼한 갑옷입니다.", 0, false, 9, 0),
            new Item("스파르타의 갑옷", "방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, 15, 0),
            new Item("낡은 검", "공격력 +2 | 쉽게 볼 수 있는 낡은 검 입니다.", 600, false, 0, 2),
            new Item("청동 도끼", "공격력 +5 | 어디선가 사용됐던거 같은 도끼입니다.", 1500, false, 0, 5),
            new Item("스파르타의 창", "공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다.", 0, false, 0, 7) 
        };

        static void Main(string[] args)
        {
            while (true) // 
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분을 환영합니다");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.Write("\n원하시는 행동을 입력해주세요\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowStatus();
                        break;
                    case "2":
                        Inventory();
                        break;
                    case "3":
                        Store();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defense}");
            Console.WriteLine($"체 력 : {health}");
            Console.WriteLine($"Gold : {gold} G");

            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();
            if (input == "0") return;
        }

        static void Inventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                if (inventory.Count == 0)
                {
                    Console.WriteLine("보유한 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        var item = inventory[i]; 
                        string equipped = item.IsEquipped ? "[E] " : ""; // item.IsEquipped가 true라면 "[E] "라는 문자열을 equipped에 저장하고, false라면 빈 문자열("")을 저장
                        Console.WriteLine($"{i + 1}. {equipped}{item.Name} | {item.Description}");
                    }
                }

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1") EquipManagement();
                else if (input == "0") return;
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void EquipManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                if (inventory.Count == 0)
                {
                    Console.WriteLine("장착할 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        var item = inventory[i];
                        string equipped = item.IsEquipped ? "[E] " : "";
                        Console.WriteLine($"{i + 1}. {equipped}{item.Name} | {item.Description}");
                    }
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n장착할 아이템 번호를 입력해주세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "0") return;

                if (int.TryParse(input, out int itemIndex) && itemIndex >= 1 && itemIndex <= inventory.Count)//입력한 값 숫자로 변환하고 올바른 아이템 번호인지 확인
                {
                    var selectedItem = inventory[itemIndex - 1];

                    if (selectedItem.IsEquipped)
                    {
                               // 아이템 장착 해제하면 부여된 능력치 감소
                        selectedItem.IsEquipped = false;
                        attack -= selectedItem.AttackBonus;
                        defense -= selectedItem.DefenseBonus;
                        Console.WriteLine($"{selectedItem.Name}을(를) 해제했습니다.");
                    }
                    else
                    {
                               //아이템 장착 되면 부여된 능력치 증가
                        selectedItem.IsEquipped = true;
                        attack += selectedItem.AttackBonus;
                        defense += selectedItem.DefenseBonus;
                        Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다!");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

                Console.ReadLine();
            }
        }

        static void Store() 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점\n");
                Console.WriteLine($"[보유 골드] {gold} G\n");

                for (int i = 0; i < storeItems.Count; i++)
                {
                    var item = storeItems[i];
                    string priceDisplay = item.IsOwned ? "구매완료" : $"{item.Price} G"; //아이템이 구매된 경우 구매완료라고 뜨고 아니면 가격 뜨게 하기
                    Console.WriteLine($"{i + 1}. {item.Name} | {item.Description} | {priceDisplay}");
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1") BuyItem();
                else if (input == "0") return;
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void BuyItem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < storeItems.Count; i++)
                {
                    var item = storeItems[i];
                    string priceDisplay = item.IsOwned ? "구매완료" : $"{item.Price} G"; 
                    Console.WriteLine($"{i + 1}. {item.Name} | {item.Description} | {priceDisplay}");
                }

                Console.Write("\n구매할 아이템 번호를 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") return;

                if (int.TryParse(input, out int itemIndex) && itemIndex >= 1 && itemIndex <= storeItems.Count)//입력한 값 숫자로 변환하고 올바른 아이템 번호인지 확인
                {
                    var selectedItem = storeItems[itemIndex - 1]; //리스트는 0부터 시작해서 -1 적어둠

                    if (selectedItem.IsOwned) Console.WriteLine("이미 구매한 아이템입니다.");
                    else if (gold >= selectedItem.Price)//소지하고 있는 골드가 충분하면 구매 가능
                    {
                        gold -= selectedItem.Price;
                        selectedItem.IsOwned = true;//아이템을 구매 상태로 변경
                        inventory.Add(selectedItem);//인벤토리 추가
                        Console.WriteLine($"{selectedItem.Name}구매를 완료했습니다.");
                    }
                    else Console.WriteLine("Gold가 부족합니다.");
                }
                else Console.WriteLine("잘못된 입력입니다.");

                Console.ReadLine();
            }
        }
    }

    class Item
    {
        public string Name, Description;
        public int Price, DefenseBonus, AttackBonus; 
        public bool IsOwned, IsEquipped; //아이템 소유, 장착 여부
        public Item(string name, string desc, int price, bool owned, int def, int atk)
        { Name = name; Description = desc; Price = price; IsOwned = owned; DefenseBonus = def; AttackBonus = atk; }
    }
}
