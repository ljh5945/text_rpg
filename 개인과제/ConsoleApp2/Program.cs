using System.Text;

namespace SpartaDungeon
{
    internal class Program
    {
        private static Character player;
        private static ItemShop shop;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Heon", "전사", 1, 10, 5, 100, 1500);
            shop = new ItemShop();

            // 아이템 정보 세팅

            shop.InputItem(new Item("수련자 갑옷", Item.ItemType.Armor, 0, 5, "쉽게 볼 수 있는 낡은 검입니다.", 1000));
            shop.InputItem(new Item("무쇠갑옷", Item.ItemType.Armor, 0, 9, "보통의 검입니다.", 2000));
            shop.InputItem(new Item("스파르타의 갑옷", Item.ItemType.Armor, 0, 15, "종이로 만들어진 갑옷입니다.", 3500));
            shop.InputItem(new Item("낡은 검", Item.ItemType.Weapon, 2, 0, "신성한 힘이 깃든 검입니다.", 600));
            shop.InputItem(new Item("청동 도끼", Item.ItemType.Weapon, 5, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 900));
            shop.InputItem(new Item("스파르타의 창", Item.ItemType.Weapon, 7, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500));
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;

            }
        }

        static void DisplayShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            shop.ShowItems();
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    BuyItem(0);
                    break;
            }
        }

        private static void BuyItem(int Message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            shop.ShowItems();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            switch (Message)
            {
                case 1://이미 구매
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.ResetColor();
                    break;
                case 2://Gold 부족
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Gold가 부족합니다.");
                    Console.ResetColor();
                    break;
                case 3://구매 완료
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("구매를 완료했습니다.");
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine();
            int input = CheckValidInput(0, shop.items.Count);
            switch (input)
            {
                case 0:
                    DisplayShop();
                    break;
                default:
                    int result = shop.BuyItem(player, input - 1);
                    BuyItem(result);
                    break;
            }

        }

        static void DisplayMyInfo()
        {
            StringBuilder sb = new StringBuilder();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");

            sb.AppendFormat($"공격력 : {player.Atk}");
            string str = player.itemAtk > 0 ? $" (+{player.itemAtk})" : "";
            sb.AppendFormat(str);
            Console.WriteLine(sb.ToString());

            sb.Clear();
            sb.AppendFormat($"방어력 : {player.Def}");
            str = player.itemDef > 0 ? $" (+{player.itemDef})" : "";
            sb.AppendFormat(str);
            Console.WriteLine(sb.ToString());

            Console.WriteLine($"체 력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }
        
        static void DisplayInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowItems();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    ManageEquipped();
                    break;
            }

        }
        // 인벤토리 - 장착관리
        static void ManageEquipped()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            player.inventory.ShowItems();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckValidInput(0, player.inventory.sortedList.Count);
            if (input == 0)
                DisplayGameIntro();
            else if (player.isEquipped(input - 1))
            {
                player.unEquipItem(input - 1);
            }
            else
            {
                player.EquipItem(input - 1);
            }
            ManageEquipped();

        }
        static int CheckValidInput(int min, int max)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

    }

    public class ItemShop
    {
        public List<Item> items;

        public ItemShop()
        {
            items = new List<Item>();

        }

        public bool InputItem(Item itm)
        {
            items.Add(itm);
            return true;
        }

        public void ShowItems()
        {
            int i = 0;

            foreach (Item item in items)
            {
                Console.Write("- " + (i++ + 1) + " ");
                if (item.isEquipped)
                {
                    Console.Write('[');
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('E');
                    Console.ResetColor();
                    Console.Write(']');
                    Console.Write(PadRightForMixedText(item.Name, 12));
                }
                else
                    Console.Write(PadRightForMixedText(item.Name, 15));

                if (item.Atk > 0) { Console.Write(PadRightForMixedText("| 공격력 +" + item.Atk.ToString(), 13)); }
                if (item.Def > 0) { Console.Write(PadRightForMixedText("| 방어력 +" + item.Def.ToString(), 13)); }
                Console.Write(PadRightForMixedText("| " + item.description, 40));
                Console.WriteLine($"| {(item.isOwned ? "구매완료" : $"{item.Price} G")} ");
            }
        }

        public int BuyItem(Character p, int idx)
        {
            if (items[idx].isOwned == true)
                return 1;
            if (p.Gold - items[idx].Price < 0)
                return 2;
            items[idx].isOwned = true;
            p.Gold -= items[idx].Price;
            p.inventory.InputItem(items[idx]);
            return 3;
        }
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

    }

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public float Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public int itemAtk;     //장착 아이템으로 오르는 공격력
        public int itemDef;     //장착 아이템으로 오르는 방어력
        public Inventory inventory;
        public int ClearCnt;
        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            ClearCnt = 0;
            inventory = new Inventory();
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
        
        
        public void EquipItem(int idx)
        {
            Item curItem = inventory.sortedList[idx];

            //같은 타입의 아이템 장착 해제
            
            curItem.isEquipped = true;

            if (curItem.Atk > 0)
            {
                itemAtk += curItem.Atk;
                Atk += curItem.Atk;
            }
            if (curItem.Def > 0)
            {
                itemDef += curItem.Def;
                Def += curItem.Def;
            }
        }
        public void unEquipItem(int idx)
        {
            Item curItem = inventory.sortedList[idx];
            curItem.isEquipped = false;
            if (curItem.Atk > 0)
            {
                itemAtk -= curItem.Atk;
                Atk -= curItem.Atk;
            }
            if (curItem.Def > 0)
            {
                itemDef -= curItem.Def;
                Def -= curItem.Def;
            }
        }
        public bool isEquipped(int idx)
        {
            Item curItem = inventory.sortedList[idx];
            return curItem.isEquipped;
        }

        
    }
    public class Item
    {
        public enum ItemType { Weapon, Armor };
        public ItemType itemType;
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public bool isEquipped { get; set; }    //장착 여부
        public bool isOwned { get; set; }     //보유 여부
        public string description;
        public int Price { get; }


        public Item(string name, ItemType type, int atk, int def, string description, int price, bool equipped = false, bool isOwned = false)
        {
            itemType = type;
            Name = name;
            Atk = atk;
            Def = def;
            Price = price;
            this.description = description;
            this.isOwned = isOwned;
        }
        public int CompareLength(Item itm)
        {
            if (Name.Length > itm.Name.Length)
                return 1;
            else if (Name.Length == itm.Name.Length)
                return 0;
            else
                return -1;
        }
    }
    
    public class Inventory
    {
        public List<Item> itemList;
        public List<Item> sortedList;
        public Inventory()
        {
            itemList = new List<Item>();
            sortedList = new List<Item>();
        }
        public bool InputItem(Item itm)
        {
            itemList.Add(itm);
            sortedList.Add(itm);
            return true;
        }
        public bool DeleteItem(Item itm)
        {
            itemList.Remove(itm);
            return true;
        }
        public void ShowItems()
        {
            int i = 0;

            foreach (Item item in sortedList)
            {
                Console.Write("- " + (i++ + 1) + " ");
                if (item.isEquipped)
                {
                    Console.Write('[');
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('E');
                    Console.ResetColor();
                    Console.Write(']');
                    Console.Write(PadRightForMixedText(item.Name, 12));
                }
                else
                    Console.Write(PadRightForMixedText(item.Name, 15));

                if (item.Atk > 0) { Console.Write(PadRightForMixedText("| 공격력 +" + item.Atk.ToString(), 13)); }
                if (item.Def > 0) { Console.Write(PadRightForMixedText("| 방어력 +" + item.Def.ToString(), 13)); }
                Console.Write(PadRightForMixedText("| " + item.description, 40));
                Console.WriteLine($"| {item.Price} G");
            }
        }
        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }
    }
}