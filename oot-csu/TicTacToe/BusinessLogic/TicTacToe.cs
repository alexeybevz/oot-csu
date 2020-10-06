using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToeGame.Domain;

namespace TicTacToeGame.BusinessLogic
{
    public class TicTacToe
    {
        private readonly int size;
        private readonly IWinnerSelector winnerSelector;
        private TypeStep? lastTypeStep;

        public List<Step> Steps { get; private set; }

        public TicTacToe(int size, IWinnerSelector winnerSelector)
        {
            this.size = size;
            this.winnerSelector = winnerSelector;
            StartNewGame();
        }

        public void StartNewGame()
        {
            if (Steps == null)
                Steps = new List<Step>(size * size);
            else
                Steps.Clear();
        }

        public void DoStep(Step step)
        {
            var winner = GetWinner();
            if (winner != null)
                throw new ArgumentException($"Нельзя сделать шаг, т.к. уже выявлен победитель. Это '{winner}'");

            if (Steps.Count == size * size)
                throw new ArgumentException("Не осталось свободных ячеек для хода");

            if (lastTypeStep != null && step.TypeStep == lastTypeStep)
                throw new ArgumentException("Нет чередования типа хода");

            if (step.Row < 1 || step.Row > size)
                throw new ArgumentException("Ход вне поля по строке");

            if (step.Col < 1 || step.Col > size)
                throw new ArgumentException("Ход вне поля по столбцу");
                
            bool isStepExists = Steps.Count(s => s.Row == step.Row && s.Col == step.Col) > 0;
            if (isStepExists)
                throw new ArgumentException("В эту клетку уже был сделан ход");

            lastTypeStep = step.TypeStep;

            Steps.Add(step);
        }

        public TypeStep? GetWinner()
        {
            var winner = winnerSelector.GetWinner(Steps);
            return winner;
        }

        public void RollbackLastStep()
        {
            if (Steps.Any())
                Steps.RemoveAt(Steps.Count - 1);
        }
    }
}