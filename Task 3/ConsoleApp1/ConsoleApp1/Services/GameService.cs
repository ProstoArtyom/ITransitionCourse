using System;
using System.Data;
using ConsoleApp1.HelperClasses;
using ConsoleTables;

namespace ConsoleApp1.Services;
public class GameService
{
    private readonly string[] _moves;

    private HMACHelper HMACHelper { get; } = new();
    private int ComputerMoveIndex { get; }
    private string ComputerMove { get; }
    private string ComputerMoveHMAC { get; }

    public GameService(string[] moves)
    {
        _moves = moves;

        Random random = new();
        ComputerMoveIndex = random.Next(_moves.Length);
        ComputerMove = _moves[ComputerMoveIndex];
        ComputerMoveHMAC = HMACHelper.GetHMAC(ComputerMove);
    }
    
    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine($"HMAC: {ComputerMoveHMAC}");

            Console.WriteLine("Available moves:");
            for (int i = 0; i < _moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {_moves[i]}");
            }

            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");

            Console.Write("Enter your move: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    return;
                case "?":
                    ShowHelpTable();
                    break;
                default:
                    if (!int.TryParse(input, out int index) || (1 > index || index > _moves.Length))
                    {
                        Console.WriteLine("Incorrect parameter. Try again.\n");
                        break;
                    }
                    index -= 1;

                    Console.WriteLine($"Your move: {_moves[index]}");
                    Console.WriteLine($"Computer move: {ComputerMove}");

                    // TODO: Implement making move logic
                    int a = index, b = ComputerMoveIndex;
                    int n = _moves.Length, p = n >> 1;
                    var result = Math.Sign((a - b + p + n) % n - p);

                    Console.WriteLine(result == -1
                        ? "You lose!"
                        : result == 1
                            ? "You win!"
                            : "Draw!");

                    Console.WriteLine($"HMAC key: {HMACHelper.AESKey}");

                    return;
            }
        }
    }

    private void ShowHelpTable()
    {
        List<string> columnNames = new()
        {
            "v PC\\User >"
        };
        foreach (var move in _moves)
        {
            columnNames.Add(move);
        }

        var table = new ConsoleTable(columnNames.ToArray());

        for (var i = 0; i < _moves.Length; i++)
        {
            List<string> rowList = new List<string>(_moves.Length + 1)
            {
                _moves[i]
            };

            for (var j = 0; j < _moves.Length; j++)
            {
                int a = j, b = i;
                int n = _moves.Length, p = n >> 1;
                var result = Math.Sign((a - b + p + n) % n - p);
                rowList.Add(result == -1
                    ? "Lose"
                    : result == 1
                        ? "Win"
                        : "Draw");
            }

            table.AddRow(rowList.ToArray());
        }

        table.Write(Format.Alternative);
    }
}
