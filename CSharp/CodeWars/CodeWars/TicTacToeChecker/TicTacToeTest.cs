using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodeWars.TicTacToeChecker
{
    /*
    If we were to set up a Tic-Tac-Toe game, we would want to know whether the board's current state is solved, wouldn't we? Our goal is to create a function that will check that for us!

    Assume that the board comes in the form of a 3x3 array, where the value is 0 if a spot is empty, 1 if it is an X, or 2 if it is an O, like so:

    [[0,0,1],
     [0,1,2],
     [2,1,0]]
    We want our function to return -1 if the board is not solved yet, 1 if X won, 2 if O won, or 0 if it's a cat's game (i.e. a draw).

    You may assume that the board passed in is valid in the context of a game of Tic-Tac-Toe.
    */

    [TestFixture]
    public class TicTacToeTest
    {
        private readonly TicTacToe _tictactoe = new TicTacToe();

        [Test]
        public void AcceptanceTest()
        {
            var board = new [,] { { 1, 1, 1 }, { 0, 2, 2 }, { 0, 0, 0 } };
            Assert.AreEqual(1, _tictactoe.IsSolved(board));
        }

        [Test]
        public void AcceptanceTest2()
        {
            var board = new [,] { { 2, 1, 2 }, { 2, 1, 1 }, { 1, 2, 1 } };
            Assert.AreEqual(0, _tictactoe.IsSolved(board));
        }
    }

    internal class TicTacToe
    {
        public double IsSolved(int[,] board)
        {
            var grid = GridFrom(board);

            var xWins = grid.ExistsCombinationWithAll(CellValue.X);
            var oWins = grid.ExistsCombinationWithAll(CellValue.O);
            
            if (xWins && oWins)
                return 0;
            if (xWins)
                return 1;
            if (oWins)
                return 2;
            if (grid.BoardFull())
                return 0;
            return -1;
        }

        private Grid GridFrom(int[,] board)
        {
            var grid = 
                Enumerable.Range(0, board.GetLength(0))
                    .SelectMany(r =>
                        Enumerable.Range(0, board.GetLength(1))
                            .Select(c => 
                                new GridElement
                                {
                                    Coordinate = new Coordinate {Row = r, Column = c},
                                    Value = (CellValue) board[r, c]
                                }))
                    .ToList();

            return new Grid(grid);
        }
    }

    public enum CellValue
    {
        Empty = 0,
        X = 1,
        O = 2
    }

    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public bool Equals(Coordinate other)
        {
            return Row == other.Row && Column == other.Column;
        }
    }

    public class GridElement
    {
        public Coordinate Coordinate { get; set; }
        public CellValue Value { get; set; }
    }

    internal class Grid
    {
        private readonly List<GridElement> _elements;

        public Grid(List<GridElement> elements)
        {
            _elements = elements;
        }

        public bool ExistsCombinationWithAll(CellValue cellValue)
        {
            return AllPossibleCombinations().Any(combination => combination.All(el => el.Value == cellValue));
        }

        public List<IEnumerable<GridElement>> AllPossibleCombinations()
        {
            return Rows()
                .Union(Columns())
                .Union(Diagonals())
                .ToList();
        }

        public List<List<GridElement>> Rows()
        {
            return ElementsBy(RowSelector);
        }

        private List<List<GridElement>> ElementsBy(Func<GridElement, int> selector)
        {
            return _elements.GroupBy(selector).Select(g=>g.ToList()).ToList();
        }

        private static Func<GridElement, int> RowSelector
        {
            get { return el => el.Coordinate.Row; }
        }

        public IEnumerable<List<GridElement>> Columns()
        {
            return ElementsBy(ColumnSelector);
        }

        private static Func<GridElement, int> ColumnSelector
        {
            get { return el => el.Coordinate.Column; }
        }

        public List<IEnumerable<GridElement>> Diagonals()
        {
            var leftDiagonal =
                GetElementsAt(
                    new[]
                    {
                        new Coordinate {Row = 0, Column = 0},
                        new Coordinate {Row = 1, Column = 1},
                        new Coordinate {Row = 2, Column = 2}
                    });

            var rightDiagonal =
                GetElementsAt(
                    new[]
                    {
                        new Coordinate {Row = 0, Column = 2},
                        new Coordinate {Row = 1, Column = 1},
                        new Coordinate {Row = 2, Column = 0}
                    });

            return new [] { leftDiagonal, rightDiagonal }.ToList();
        }

        private IEnumerable<GridElement> GetElementsAt(Coordinate[] coordinates)
        {
            return _elements.Where(val => coordinates.Any(v => v.Equals(val.Coordinate)));
        }

        public bool BoardFull()
        {
            return _elements.All(el => el.Value != CellValue.Empty);
        }
    }
}