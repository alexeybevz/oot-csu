using System;
using System.Collections.Generic;
using TicTacToeGame.BusinessLogic;
using TicTacToeGame.Domain;
using Xunit;

namespace TicTacToeGame
{
    public class UnitTests
    {
        private const int FieldSize = 3;

        private static TicTacToe NewGame()
        {
            return new TicTacToe(FieldSize, new WinnerSelector(FieldSize));
        }

        [Fact]
        public void NoWinnerIfNoSteps()
        {
            var winnerSelector = new WinnerSelector(FieldSize);
            Assert.Equal(null, winnerSelector.GetWinner(new List<Step>()));
        }

        [Fact]
        public void GetWinnerByRow()
        {
            /* X-X-X
               O- - 
               O- -
             */

            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.X));
            game.DoStep(new Step(2, 1, TypeStep.O));
            game.DoStep(new Step(1, 2, TypeStep.X));
            game.DoStep(new Step(3, 1, TypeStep.O));
            game.DoStep(new Step(1, 3, TypeStep.X));

            Assert.Equal(TypeStep.X, game.GetWinner());
        }
        
        [Fact]
        public void GetWinnerByCol()
        {
            /* X-O-
               X-O- 
               X- -
             */

            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.X));
            game.DoStep(new Step(1, 2, TypeStep.O));
            game.DoStep(new Step(2, 1, TypeStep.X));
            game.DoStep(new Step(2, 2, TypeStep.O));
            game.DoStep(new Step(3, 1, TypeStep.X));

            Assert.Equal(TypeStep.X, game.GetWinner());
        }

        [Fact]
        public void GetWinnerByDiagonal()
        {
            /* O-X-
               X-O-
                - -O
             */

            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.O));
            game.DoStep(new Step(2, 1, TypeStep.X));
            game.DoStep(new Step(2, 2, TypeStep.O));
            game.DoStep(new Step(1, 2, TypeStep.X));
            game.DoStep(new Step(3, 3, TypeStep.O));

            Assert.Equal(TypeStep.O, game.GetWinner());
        }

        [Fact]
        public void GetWinnerByBackDiagonal()
        {
            /* X-X-O
               X-O- 
               O- -
             */

            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.X));
            game.DoStep(new Step(2, 2, TypeStep.O));
            game.DoStep(new Step(1, 2, TypeStep.X));
            game.DoStep(new Step(1, 3, TypeStep.O));
            game.DoStep(new Step(2, 1, TypeStep.X));
            game.DoStep(new Step(3, 1, TypeStep.O));

            Assert.Equal(TypeStep.O, game.GetWinner());
        }

        [Fact]
        public void DenyDoStepIfExistsWinner()
        {
            /* X-O-
               X-O- 
               X-O-
             */

            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.X));
            game.DoStep(new Step(1, 2, TypeStep.O));
            game.DoStep(new Step(2, 1, TypeStep.X));
            game.DoStep(new Step(2, 2, TypeStep.O));
            game.DoStep(new Step(3, 1, TypeStep.X));

            Assert.Throws<ArgumentException>(() => game.DoStep(new Step(2, 3, TypeStep.O)));
        }

        [Fact]
        public void AllowOnlyInterchangeTypeSteps()
        {
            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.X));

            Assert.Throws<ArgumentException>(() => game.DoStep(new Step(1, 2, TypeStep.X)));
        }

        [Fact]
        public void DenyRepeatingStep()
        {
            var game = NewGame();
            game.DoStep(new Step(1, 1, TypeStep.X));

            Assert.Throws<ArgumentException>(() => game.DoStep(new Step(1, 1, TypeStep.O)));
        }

        [Fact]
        public void StepOffField()
        {
            var game = NewGame();
            Assert.Throws<ArgumentException>(() => game.DoStep(new Step(0, 0, TypeStep.X)));
        }
    }
}