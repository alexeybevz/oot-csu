using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToeGame.Domain;

namespace TicTacToeGame.BusinessLogic
{
    public class WinnerSelector : IWinnerSelector
    {
        private readonly int size;

        public WinnerSelector(int size)
        {
            this.size = size;
        }

        public TypeStep? GetWinner(List<Step> steps)
        {
            TypeStep? winner;

            for (int i = 1; i <= size; i++)
            {
                bool isWinnerByRow = TryGetWinner(steps, s => s.Row == i, out winner);
                if (isWinnerByRow)
                    return winner;

                bool isWinnerByCol = TryGetWinner(steps, s => s.Col == i, out winner);
                if (isWinnerByCol)
                    return winner;
            }

            bool isWinnerByBackDiagonal = TryGetWinner(steps, s => (s.Row + s.Col) == size + 1, out winner);
            if (isWinnerByBackDiagonal)
                return winner;

            bool isWinnerByDiagonal = TryGetWinner(steps, s => s.Row == s.Col, out winner);
            if (isWinnerByDiagonal)
                return winner;
            
            return winner;
        }

        private bool TryGetWinner(List<Step> steps, Func<Step, bool> predicate, out TypeStep? winner)
        {
            var stepsByDirection = GetSteps(steps, predicate);
            var uniqueStepsByDirection = GetUniqueSteps(stepsByDirection);

            if (IsWinner(stepsByDirection, uniqueStepsByDirection))
            {
                winner = uniqueStepsByDirection.First();
                return true;
            }

            winner = null;
            return false;
        }

        /// <summary>
        /// Игрок является победителем, если количество сделанных шагов по строке/столбцу/диагонали равно
        /// размерности поля и сделанные шаги одного типа.
        /// </summary>
        /// <param name="stepsByDirection">Шаги, сделанные по строке/столбцу/диагонали</param>
        /// <param name="uniqueStepsByDirection">Уникальные шаги, сделанные по строке/столбцу/диагонали</param>
        /// <returns></returns>
        private bool IsWinner(List<TypeStep> stepsByDirection, List<TypeStep> uniqueStepsByDirection)
        {
            return stepsByDirection.Count() == size && uniqueStepsByDirection.Count() == 1;
        }

        private List<TypeStep> GetSteps(List<Step> steps, Func<Step, bool> predicate)
        {
            return steps.Where(predicate).Select(s => s.TypeStep).ToList();
        }

        private List<TypeStep> GetUniqueSteps(List<TypeStep> steps)
        {
            return steps.Distinct().ToList();
        }
    }
}