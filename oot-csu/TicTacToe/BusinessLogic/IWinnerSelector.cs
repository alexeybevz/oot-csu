using System.Collections.Generic;
using TicTacToeGame.Domain;

namespace TicTacToeGame.BusinessLogic
{
    public interface IWinnerSelector
    {
        TypeStep? GetWinner(List<Step> steps);
    }
}