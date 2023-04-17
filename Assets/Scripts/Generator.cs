using System.Collections.Generic;
using UnityEngine;

public class Generator
{
    public enum DifficultyLevel
    {
        EASY,
        MEDIUM,
        DIFFICULT
    }

    private const int GRID_SIZE = 9;
    private const int SUBGRID_SIZE = 3;
    private const int BOARD_SIZE = 9;
    private const int MIN_SQUARES_REMOVED = 10;
    private const int MAX_SQUARED_REMOVED = 50;

    public static int[,] GeneratePuzzle(DifficultyLevel difficultyLevel)
    {
        var grid = new int[GRID_SIZE, GRID_SIZE];
        int squaresToRemove = 0;

        switch (difficultyLevel)
        {
            case DifficultyLevel.EASY:
                squaresToRemove = Random.Range(MIN_SQUARES_REMOVED, MIN_SQUARES_REMOVED + 5);
                break;
            case DifficultyLevel.MEDIUM:
                squaresToRemove = Random.Range(MIN_SQUARES_REMOVED + 5, MIN_SQUARES_REMOVED + 10);
                break;
            case DifficultyLevel.DIFFICULT:
                squaresToRemove = Random.Range(MIN_SQUARES_REMOVED + 10, MAX_SQUARED_REMOVED);
                break;
            default:
                break;
        }

        InitializeGrid(grid);

        while (squaresToRemove > 0)
        {
            int randRow = Random.Range(0, BOARD_SIZE);
            int randCol = Random.Range(0, BOARD_SIZE);

            if (grid[randRow, randCol] != 0)
            {
                int temp = grid[randRow, randCol];
                grid[randRow, randCol] = 0;

                if (Solver.HasUniqueSolution(grid))
                {
                    squaresToRemove--;
                }
                else
                {
                    grid[randRow, randCol] = temp;
                }
            }
        }

        return grid;
    }

    public static void InitializeGrid(int[,] grid)
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Shuffle(numbers);
        for (int i = 0; i < GRID_SIZE; i++)
        {
            grid[0, i] = numbers[i];
        }
        FillGrid(1, 0, grid);
    }

    private static bool FillGrid(int r, int c, int[,] grid)
    {
        if (r == GRID_SIZE)
        {
            return true;
        }
        if (c == GRID_SIZE)
        {
            return FillGrid(r + 1, 0, grid);
        }

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Shuffle(numbers);
        foreach (var num in numbers)
        {
            if (IsValid(num, r, c, grid))
            {
                grid[r, c] = num;

                if (FillGrid(r, c + 1, grid))
                {
                    return true;
                }
            }
        }

        grid[r, c] = 0;
        return false;
    }

    private static bool IsValid(int val, int row, int col, int[,] board)
    {

        for (int i = 0; i < BOARD_SIZE; i++)
        {
            if (board[row, i] == val)
            {
                return false;
            }
        }

        for (int i = 0; i < BOARD_SIZE; i++)
        {
            if (board[i, col] == val)
            {
                return false;
            }
        }

        int subGridRow = row / SUBGRID_SIZE * SUBGRID_SIZE;
        int subGridCol = col / SUBGRID_SIZE * SUBGRID_SIZE;
        for (int r = subGridRow; r < subGridRow + SUBGRID_SIZE; r++)
        {
            for (int c = subGridCol; c < subGridCol + SUBGRID_SIZE; c++)
            {
                if (board[r, c] == val)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}
