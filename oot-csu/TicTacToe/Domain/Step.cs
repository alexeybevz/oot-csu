namespace TicTacToeGame.Domain
{
    public class Step
    {
        public int Row { get; }
        public int Col { get; }
        public TypeStep TypeStep { get; }

        public Step(int row, int col, TypeStep typeStep)
        {
            Row = row;
            Col = col;
            TypeStep = typeStep;
        }
    }
}