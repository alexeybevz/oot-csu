using System;
using System.Collections.Generic;
using TicTacToeGame.Domain;

namespace TicTacToeGame.BusinessLogic
{
    public class FieldConsoleOutput : IFieldOutput
    {
        private readonly int size;
        private readonly List<Step> steps;

        public FieldConsoleOutput(int size, List<Step> steps)
        {
            this.size = size;
            this.steps = steps;
        }

        public void Show()
        {
            TypeStep?[,] cells = new TypeStep?[size, size];
            steps.ForEach(step => { cells[step.Row - 1, step.Col - 1] = step.TypeStep; });

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(TypeStepToString(cells[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static string TypeStepToString(TypeStep? typeStep)
        {
            switch (typeStep)
            {
                case TypeStep.X:
                    return "X";
                case TypeStep.O:
                    return "O";
                default:
                    return "-";
            }
        }
    }
}