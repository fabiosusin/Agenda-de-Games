using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaGames
{
    class Program
    {
        public static HashTable Table = new HashTable(500);
        public static Node Ant;
        public static List<LetterIndex> Letters;

        static void Main()
        {
            var key = 1;
            var header = new Node
            {
                Next = null,
                Prev = null
            };

            Ant = header;

            var stop = false;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("1 - Inserir jogo");
                Console.WriteLine("2 - Remover jogo");
                Console.WriteLine("3 - Consulta jogo");
                Console.WriteLine("4 - Consulta todos");
                Console.WriteLine("5 - Consulta por data");
                Console.WriteLine("6 - Consulta na mesma posição");
                Console.WriteLine("7 - Lista todos");
                Console.WriteLine("8 - Sair");
                var action = Console.ReadLine();
                Console.WriteLine("");
                if (!int.TryParse(action, out var n))
                {
                    Console.WriteLine("Ação incorreta digitada");
                    continue;
                }


                switch (n)
                {
                    case 1:
                        InsertGame(key, GetGame());
                        break;
                    case 2:
                        Console.WriteLine("Nome do jogo a ser removido");
                        var gameToRemove = Console.ReadLine();
                        if (string.IsNullOrEmpty(gameToRemove))
                        {
                            Console.WriteLine("Informe o nome do jogo corretamente");
                            continue;
                        }

                        RemoveGame(header, gameToRemove);
                        break;
                    case 3:
                        Console.WriteLine("Nome do jogo a ser consultado");
                        var nameGameToInfo = Console.ReadLine();
                        if (string.IsNullOrEmpty(nameGameToInfo))
                        {
                            Console.WriteLine("Informe o nome do jogo corretamente");
                            continue;
                        }

                        GetGameInfo(header, nameGameToInfo);
                        break;
                    case 4:
                        List(header, SortListType.Name);
                        break;
                    case 5:
                        List(header, SortListType.Date);
                        break;
                    case 6:
                        Console.WriteLine("Código do jogo a ser consultado");
                        var code = Console.ReadLine();
                        if (string.IsNullOrEmpty(code))
                        {
                            Console.WriteLine("Informe o código do jogo corretamente");
                            continue;
                        }

                        PrintGameInfo(Table.GetValue(code));
                        break;
                    case 7:
                        ListAll(header);
                        break;
                    case 8:
                        stop = true;
                        break;
                }

                key++;
            }
            while (!stop);
        }

        public static void InsertGame(int key, Game game)
        {
            Table.Insert(key.ToString(), game);
            var node = new Node
            {
                Data = game,
                Prev = Ant
            };

            Ant.Next = node;
            Ant = node;
        }

        public static void RemoveGame(Node head, string value)
        {
            if (head == null)
                return;

            if (head.Data?.Name == value)
            {
                head = head.Next;
                return;
            }

            var n = head;
            while (n.Next != null)
            {
                if (n.Next.Data?.Name == value)
                {
                    n.Next = n.Next.Next;
                    return;
                }

                n = n.Next;
            }
        }

        public static void GetGameInfo(Node head, string value)
        {
            Game game = null;

            if (head == null)
                return;

            if (head.Data?.Name == value)
                game = head.Data;

            var n = head;
            while (n.Next != null && game == null)
            {
                if (n.Next.Data?.Name == value)
                {
                    game = n.Next.Data;
                    break;
                }

                n = n.Next;
            }

            if (game == null)
                return;

            PrintGameInfo(game);
        }

        public static void List(Node head, SortListType sort)
        {
            if (sort == SortListType.Unknown)
                return;

            bool wasChanged, currentIsBigger;

            do
            {
                Node current = head;
                Node previous = null;
                Node next = head.Next;
                wasChanged = false;
                while (next != null)
                {
                    currentIsBigger = sort == SortListType.Name ? current.Data?.LetterIndex > next.Data?.LetterIndex : current.Data?.Date > next.Data?.Date;
                    if (currentIsBigger)
                    {
                        wasChanged = true;

                        if (previous != null)
                        {
                            Node sig = next.Next;

                            previous.Next = next;
                            next.Next = current;
                            current.Next = sig;
                        }
                        else
                        {
                            Node sig = next.Next;

                            head = next;
                            next.Next = current;
                            current.Next = sig;
                        }

                        previous = next;
                        next = current.Next;
                    }
                    else
                    {
                        previous = current;
                        current = next;
                        next = next.Next;
                    }
                }
            } while (wasChanged);

            ListAll(head);
        }

        public static void ListAll(Node head)
        {
            if (head == null)
                return;

            var n = head;
            while (n.Next != null)
            {
                if (!string.IsNullOrEmpty(n.Next.Data?.Name))
                    PrintGameInfo(n.Next.Data);

                n = n.Next;
            }
        }

        private static void PrintGameInfo(Game game)
        {
            if (game == null)
                return;

            Console.WriteLine($"Nome: {game.Name}, Categoria: {game.Category}, Link: {game.Link}, Data: {game.Date:dd/MM/yyyy}, Hour: {game.Hour}");
        }

        private static Game GetGame()
        {
            string name, category, link, dateStr, hour;
            var date = DateTime.MinValue;
            var allValid = false;

            do
            {
                Console.WriteLine("Digite o nome do jogo");
                name = Console.ReadLine();

                Console.WriteLine("Digite a categoria do jogo");
                category = Console.ReadLine();

                Console.WriteLine("Digite o link do jogo");
                link = Console.ReadLine();

                Console.WriteLine("Digite a data do jogo");
                dateStr = Console.ReadLine();

                Console.WriteLine("Digite a hora do jogo");
                hour = Console.ReadLine();


                allValid = new List<string> { name, category, link, hour }.FirstOrDefault(x => string.IsNullOrEmpty(x)) == null;

                if (!DateTime.TryParse(dateStr, out date))
                {
                    Console.WriteLine("Data informada não estava em um formato válido!");
                    allValid = false;
                }

                if (!allValid)
                    Console.WriteLine("Informe todos os campos corretamente!");
            }
            while (!allValid);

            var idx = GetAlphabet().FirstOrDefault(x => x.Letter == name.Substring(0, 1).ToLower())?.Index ?? 0;
            return new Game(idx, name, category, link, date, hour);
        }

        private static List<LetterIndex> GetAlphabet()
        {
            if (Letters == null)
                Letters = new List<LetterIndex> {
                new LetterIndex(0, "a"), new LetterIndex(1, "b"), new LetterIndex(2, "c"), new LetterIndex(3, "d"), new LetterIndex(4, "e"), new LetterIndex(5, "f"), new LetterIndex(6, "g"),
                new LetterIndex(7, "h"), new LetterIndex(8, "i"), new LetterIndex(9, "j"), new LetterIndex(10, "k"), new LetterIndex(11, "l"), new LetterIndex(12, "m"), new LetterIndex(13, "n"),
                new LetterIndex(14, "o"), new LetterIndex(15, "p"), new LetterIndex(16, "q"), new LetterIndex(17, "r"), new LetterIndex(18, "s"), new LetterIndex(19, "t"), new LetterIndex(20, "u"),
                new LetterIndex(21, "v"), new LetterIndex(22, "x"), new LetterIndex(23, "y"), new LetterIndex(24, "w"), new LetterIndex(25, "z")
            };

            return Letters;
        }

    }


    public class Game
    {
        public Game(int idx, string name, string category, string link, DateTime date, string hour)
        {
            LetterIndex = idx;
            Name = name;
            Category = category;
            Link = link;
            Date = date;
            Hour = hour;
        }

        public int LetterIndex { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
    }

    public class HashTable
    {
        private Node[] universe;
        private readonly int tableSize;

        public HashTable(int maxTableSize)
        {
            tableSize = maxTableSize;
            universe = new Node[tableSize];
        }

        private int HashFuncation(string key)
        {
            int index = 7;
            for (int i = 0; i < key.Length; i++)
            {
                var asciiVal = key[i] * i;
                index = index * 31 + asciiVal;
            }
            return index % tableSize;
        }

        public void Insert(string key, Game value)
        {
            int genIndex = HashFuncation(key);
            Node node = universe[genIndex];

            if (node == null)
            {
                universe[genIndex] = new Node() { Key = key, Data = value };
                return;
            }

            if (node.Key == key)
                throw new Exception("Can't use same key!");

            while (node.Next != null)
            {
                node = node.Next;
                if (node.Key == key)
                    throw new Exception("Can't use same key!");
            }

            Node newNode = new Node() { Key = key, Data = value, Prev = node, Next = null };
            node.Next = newNode;
        }

        public Game GetValue(string key)
        {
            int genIndex = HashFuncation(key);
            Node node = universe[genIndex];
            while (node != null)
            {
                if (node.Key == key)
                {
                    return node.Data;
                }
                node = node.Next;
            }

            return null;
        }
    }

    public class Node
    {
        public string Key { get; set; }
        public Game Data;
        public Node Next;
        public Node Prev;
    };

    public class LetterIndex
    {
        public LetterIndex(int idx, string let)
        {
            Index = idx;
            Letter = let;
        }

        public int Index { get; set; }
        public string Letter { get; set; }
    }

    public enum SortListType
    {
        Unknown,
        Name,
        Date
    }
}