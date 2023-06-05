using System;
using System.Collections.Generic;
using System.Threading;

class Player
{
    public string Username { get; set; }
    public int Points { get; set; }
}

class CardGame
{
    private static List<Player> players = new List<Player>();

    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Welcome to the Card Game!");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Signup");
            Console.WriteLine("3. Exit");

            Console.Write("Please choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Signup();
                    break;
                case "3":
                    isRunning = false;
                    Console.WriteLine("Thank you for playing the Card Game. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void Login()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();

        Player player = players.Find(p => p.Username == username);

        if (player != null)
        {
            Console.WriteLine($"Welcome back, {player.Username}!");
            PlayGame(player);
        }
        else
        {
            Console.WriteLine("User not found. Please signup or try again.");
        }
    }

    static void Signup()
    {
        Console.Write("Enter your desired username: ");
        string username = Console.ReadLine();

        Player player = players.Find(p => p.Username == username);

        if (player == null)
        {
            player = new Player { Username = username, Points = 0 };
            players.Add(player);
            Console.WriteLine($"Signup successful. Welcome, {player.Username}!");
            PlayGame(player);
        }
        else
        {
            Console.WriteLine("Username already exists. Please choose a different username.");
        }
    }

    static void PlayGame(Player player)
    {
        Console.WriteLine($"You currently have {player.Points} points.");

        // Memory game logic
        Console.WriteLine("Starting Memory Game...");

        // Generate a sequence of random numbers
        List<int> sequence = GenerateRandomSequence(3);

        // Display the sequence to the player
        Console.WriteLine("Memorize the sequence:");
        DisplaySequence(sequence);
        Thread.Sleep(3000); // Pause for 3 seconds

        // Clear the console
        Console.Clear();

        // Get player input and check if it matches the sequence
        bool isCorrect = GetPlayerInputAndCheck(sequence);

        if (isCorrect)
        {
            Console.WriteLine("Congratulations! You remembered the sequence correctly.");
            player.Points += 10;
        }
        else
        {
            Console.WriteLine("Sorry, you made a mistake. Better luck next time!");
        }

        Console.WriteLine($"Total points: {player.Points}");
    }

    static List<int> GenerateRandomSequence(int length)
    {
        List<int> sequence = new List<int>();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            sequence.Add(random.Next(1, 3));
        }

        return sequence;
    }

    static void DisplaySequence(List<int> sequence)
    {
        foreach (int number in sequence)
        {
            Console.Write(number + " ");
            Thread.Sleep(1000); // Pause for 1 second between numbers
        }
        Console.WriteLine();
    }

    static bool GetPlayerInputAndCheck(List<int> sequence)
    {
        Console.WriteLine("Enter the numbers in the sequence (separated by spaces):");
        string input = Console.ReadLine();
        string[] inputArray = input.Split(' ');

        if (inputArray.Length != sequence.Count)
        {
            return false;
        }

        for (int i = 0; i < inputArray.Length; i++)
        {
            if (!int.TryParse(inputArray[i], out int number) || number != sequence[i])
            {
                return false;
            }
        }

        return true;
    }
}
