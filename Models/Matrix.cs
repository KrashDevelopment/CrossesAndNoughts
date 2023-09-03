﻿using CrossesAndNoughts.Models.Players;
using CrossesAndNoughts.Models.Strategies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CrossesAndNoughts.Models;

public class Matrix : IEnumerable<Symbol>
{
    public static Grid? Field { get; set; }
    public User? CurrentUser { get; set; }
    public Opponent? CurrentOpponent { get; set; }

    public static Matrix Instance => _instance.Value;

    private static Lazy<Matrix> _instance = new(() => new Matrix());
    private readonly Symbol[,] _state = new Symbol[3, 3];

    private static readonly Line[] _lines = new Line[]
    {
        new Line(new Position(0, 0), new Position(0, 1), new Position(0, 2)),
        new Line(new Position(1, 0), new Position(1, 1), new Position(1, 2)),
        new Line(new Position(2, 0), new Position(2, 1), new Position(2, 2)),

        new Line(new Position(0, 0), new Position(1, 0), new Position(2, 0)),
        new Line(new Position(0, 1), new Position(1, 1), new Position(2, 1)),
        new Line(new Position(0, 2), new Position(1, 2), new Position(2, 2)),

        new Line(new Position(0, 0), new Position(1, 1), new Position(2, 2)),
        new Line(new Position(0, 2), new Position(1, 1), new Position(2, 0))
    };

    public Matrix()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _state[i, j] = Symbol.Empty;
            }
        }
    }

    public IEnumerator<Symbol> GetEnumerator()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                yield return _state[i, j];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Symbol this[int row, int column]
    {
        get => _state[row, column];
        set => _state[row, column] = value;
    }

    public GameStatus GetGameStatus()
    {
        int[] rowScores = new int[3];
        int[] columnScores = new int[3];
        int[] diagonal1Score = new int[1];
        int[] diagonal2Score = new int[1];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Symbol symbol = this[i, j];
                int delta = GetDelta(symbol);

                rowScores[i] += delta;
                columnScores[j] += delta;

                if (i == j)
                {
                    diagonal1Score[0] += delta;
                }

                if (i == 3 - j - 1)
                {
                    diagonal2Score[0] += delta;
                }
            }
        }

        foreach (Symbol symbol in new Symbol[] { Symbol.Cross, Symbol.Nought })
        {
            int winPoints = 3 * GetDelta(symbol);

            for (int i = 0; i < 3; i++)
            {
                if (rowScores[i] == winPoints || columnScores[i] == winPoints)
                {
                    return new GameStatus(true, symbol);
                }
            }

            if (diagonal1Score[0] == winPoints || diagonal2Score[0] == winPoints)
            {
                return new GameStatus(true, symbol);
            }
        }

        return new GameStatus(IsFieldFull(), Symbol.Empty);
    }


    public void ClearSymbol(int row, int column)
    {
        this[row, column] = Symbol.Empty;
    }

    public Matrix Copy()
    {
        Matrix matrix = new();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix[i, j] = this[i, j];
            }
        }

        matrix.CurrentUser = CurrentUser;
        matrix.CurrentOpponent = CurrentOpponent;

        return matrix;
    }

    public static void Reset()
    {
        if (Field is null)
        {
            throw new NullReferenceException(nameof(Field));
        }

        Field.Children.RemoveRange(18, 9);

        foreach (var cell in Field.Children.OfType<Image>())
        {
            Field.Children.Remove(cell);
        }

        var currentUser = _instance.Value.CurrentUser;
        var currentOpponent = _instance.Value.CurrentOpponent;

        _instance = new(() => new Matrix()
        {
            CurrentUser = currentUser,
            CurrentOpponent = currentOpponent
        });
    }

    public static void DrawWinningLine()
    {
        if (Field is null)
        {
            throw new NullReferenceException(nameof(Field));
        }

        foreach (var line in _lines)
        {
            Symbol cell1 = _instance.Value[line.GetCell(0).Row, line.GetCell(0).Column];
            Symbol cell2 = _instance.Value[line.GetCell(1).Row, line.GetCell(1).Column];
            Symbol cell3 = _instance.Value[line.GetCell(2).Row, line.GetCell(2).Column];

            if (cell1 == Symbol.Empty || cell2 == Symbol.Empty || cell3 == Symbol.Empty || cell1 != cell2 || cell2 != cell3 || cell1 != cell3)
            {
                continue;
            }

            Line.Visualize(Line.GetLinePosition(line, Field).from, Line.GetLinePosition(line, Field).to, Field);
        }
    }

    public static int Evaluate(Matrix matrix)
    {
        int score = 0;

        for (int i = 0; i < _lines.Length; i++)
        {
            score += EvaluateLine(_lines[i], matrix);
        }

        return score;
    }

    private static int EvaluateLine(Line line, Matrix matrix)
    {
        int score = 0;

        Symbol cell1 = matrix[line.GetCell(0).Row, line.GetCell(0).Column];
        Symbol cell2 = matrix[line.GetCell(1).Row, line.GetCell(1).Column];
        Symbol cell3 = matrix[line.GetCell(2).Row, line.GetCell(2).Column];

        if (cell1 == matrix.CurrentUser?.CurrentSymbol)
        {
            score = 1;
        }
        else if (cell1 == matrix.CurrentOpponent?.CurrentSymbol)
        {
            score = -1;
        }

        if (cell2 == matrix.CurrentUser?.CurrentSymbol)
        {
            if (score == 1)
            {
                score = 10;
            }
            else if (score == -1)
            {
                return 0;
            }
            else
            {
                score = 1;
            }
        }
        else if (cell2 == matrix.CurrentOpponent?.CurrentSymbol)
        {
            if (score == -1)
            {
                score = -10;
            }
            else if (score == 1)
            {
                return 0;
            }
            else
            {
                score = -1;
            }
        }

        if (cell3 == matrix.CurrentUser?.CurrentSymbol)
        {
            if (score > 0)
            {
                score *= 10;
            }
            else if (score < 0)
            {
                return 0;
            }
            else
            {
                score = 1;
            }
        }
        else if (cell3 == matrix.CurrentOpponent?.CurrentSymbol)
        {
            if (score < 0)
            {
                score *= 10;
            }
            else if (score > 0)
            {
                return 0;
            }
            else
            {
                score = -1;
            }
        }

        return score;
    }

    private bool IsFieldFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (this[i, j] == Symbol.Empty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static int GetDelta(Symbol symbol)
    {
        return symbol switch
        {
            Symbol.Cross => 1,
            Symbol.Nought => -1,
            Symbol.Empty => 0,
            _ => throw new NotImplementedException()
        };
    }

    private class Line
    {
        private readonly Position[] _line = new Position[3];

        internal Line(Position cell1, Position cell2, Position cell3)
        {
            _line[0] = cell1;
            _line[1] = cell2;
            _line[2] = cell3;
        }

        internal Position GetCell(int index)
        {
            return _line[index];
        }

        internal static void Visualize(Point from, Point to, Grid grid)
        {
            DoubleAnimation lineXAnimation = new()
            {
                From = from.X,
                To = to.X,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            DoubleAnimation lineYAnimation = new()
            {
                From = from.Y,
                To = to.Y,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            System.Windows.Shapes.Line visualLine = new()
            {
                X1 = from.X,
                Y1 = from.Y,

                Fill = System.Windows.Media.Brushes.Violet,
                Visibility = Visibility.Visible,
                Stroke = System.Windows.Media.Brushes.Violet,

                StrokeThickness = grid.ActualWidth == grid.MaxWidth
                || grid.ActualHeight == grid.MaxHeight ? 20 : 10
            };

            grid.Children.Add(visualLine);

            Grid.SetRowSpan(visualLine, grid.RowDefinitions.Count);
            Grid.SetColumnSpan(visualLine, grid.ColumnDefinitions.Count);

            visualLine.BeginAnimation(System.Windows.Shapes.Line.X2Property, lineXAnimation);
            visualLine.BeginAnimation(System.Windows.Shapes.Line.Y2Property, lineYAnimation);
        }

        internal static (Point from, Point to) GetLinePosition(Line line, Grid grid)
        {
            IEnumerable<Image> cellsWithSymbol = grid.Children.OfType<Image>();

            Point from, to;

            foreach (Image cell in cellsWithSymbol)
            {
                int row = (int)cell.GetValue(Grid.RowProperty);
                int column = (int)cell.GetValue(Grid.ColumnProperty);

                if (row == line.GetCell(0).Row
                    && column == line.GetCell(0).Column)
                {
                    if (row == 0 && column == 0 && line.GetCell(2).Row == 2 && line.GetCell(2).Column == 2)
                    {
                        from = new Point(0, 0);
                    }
                    else if (row == 0 && column == 2 && line.GetCell(2).Row == 2 && line.GetCell(2).Column == 0)
                    {
                        from = new Point(grid.ActualWidth, 0);
                    }
                    else
                    {
                        from = cell.TranslatePoint(new Point(cell.ActualHeight - 100, cell.ActualHeight / 2), grid);
                    }

                    continue;
                }

                if (row == line.GetCell(2).Row
                    && column == line.GetCell(2).Column)
                {
                    if (row == 2 && column == 2 && line.GetCell(0).Row == 0 && line.GetCell(0).Column == 0)
                    {
                        to = new Point(grid.ActualWidth, grid.ActualHeight);
                    }
                    else if (row == 2 && column == 0 && line.GetCell(0).Row == 0 && line.GetCell(0).Column == 2)
                    {
                        to = new Point(0, grid.ActualHeight);
                    }
                    else
                    {
                        to = cell.TranslatePoint(new Point(cell.ActualHeight, cell.ActualHeight / 2), grid);
                    }

                    continue;
                }
            }

            return (from, to);
        }
    }
}

public class GameStatus
{
    public Symbol WinnerSymbol => _winnerSymbol;
    public bool IsGameOver => _isGameOver;

    private readonly Symbol _winnerSymbol;
    private readonly bool _isGameOver;

    public GameStatus(bool isGameOver, Symbol winnerSymbol)
    {
        _isGameOver = isGameOver;
        _winnerSymbol = winnerSymbol;
    }
}

public class Position
{
    public int Row => _row;
    public int Column => _column;

    private readonly int _row;
    private readonly int _column;

    public Position(int row, int column)
    {
        _row = row;
        _column = column;
    }

    public override string ToString()
    {
        return $"({_row}, {_column})";
    }
}

public class Score
{
    public int BestScore { get => _bestScore; set => _bestScore = value; }
    public int BestRow { get => _bestRow; set => _bestRow = value; }
    public int BestColumn { get => _bestColumn; set => _bestColumn = value; }

    private int _bestScore;
    private int _bestRow;
    private int _bestColumn;

    public Score(int bestScore, int bestRow, int bestColumn)
    {
        _bestScore = bestScore;
        _bestRow = bestRow;
        _bestColumn = bestColumn;
    }
}