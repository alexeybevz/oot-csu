using System;
using TicTacToeGame.BusinessLogic;
using TicTacToeGame.Domain;

namespace TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 3;
            var game = new TicTacToe(size, new WinnerSelector(size));
            var fieldConsoleOutput = new FieldConsoleOutput(size, game.Steps);

            Console.WriteLine("Новая игра");

            game.DoStep(new Step(1, 1, TypeStep.X));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(2, 2, TypeStep.O));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(1, 2, TypeStep.X));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(1, 3, TypeStep.O));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(2, 1, TypeStep.X));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(3, 1, TypeStep.O));
            fieldConsoleOutput.Show();

            Console.WriteLine($"Победитель: {game.GetWinner()}");
            Console.WriteLine();

            Console.WriteLine("Новая игра");
            game.StartNewGame();

            game.DoStep(new Step(1, 1, TypeStep.X));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(2, 1, TypeStep.O));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(1, 2, TypeStep.X));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(1, 3, TypeStep.O));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(2, 2, TypeStep.X));
            fieldConsoleOutput.Show();

            game.DoStep(new Step(3, 2, TypeStep.O));
            fieldConsoleOutput.Show();

            Console.WriteLine($"Победитель: {game.GetWinner()}");
            Console.WriteLine();

            game.DoStep(new Step(3, 3, TypeStep.X));
            fieldConsoleOutput.Show();

            Console.WriteLine($"Победитель: {game.GetWinner()}");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}