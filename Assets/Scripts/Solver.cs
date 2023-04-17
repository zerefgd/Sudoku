using System;

public class Solver
{
    private const int BOARD_SIZE = 9;
    private const int SUBGRID_SIZE = 3;
    private const int EMPTY_CELL = 0;

    public static int[,] SolveSudoku(int[,] unsolvedPuzzle)
    {
        int[,] solvedPuzzle = new int[BOARD_SIZE, BOARD_SIZE];
        Array.Copy(unsolvedPuzzle, solvedPuzzle, unsolvedPuzzle.Length);
        BackTrack(solvedPuzzle, 0, 0);
        return solvedPuzzle;
    }

    private static bool BackTrack(int[,] puzzle, int row, int col)
    {
        if (row == BOARD_SIZE)
        {
            return true;
        }

        if (col == BOARD_SIZE)
        {
            return BackTrack(puzzle, row + 1, 0);
        }

        if (puzzle[row, col] != 0)
        {
            return BackTrack(puzzle, row, col + 1);
        }

        for (int i = 1; i <= BOARD_SIZE; i++)
        {
            if (IsValid(puzzle, row, col, i))
            {
                puzzle[row, col] = i;
                if (BackTrack(puzzle, row, col + 1))
                {
                    return true;
                }
                puzzle[row, col + 1] = 0;
            }
        }

        return false;
    }

    public static bool HasUniqueSolution(int[,] board)
    {
        int solutionCount = 0;

        int row = -1, col = -1;
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            for (int c = 0; c < BOARD_SIZE; c++)
            {
                if (board[r, c] == EMPTY_CELL)
                {
                    row = r;
                    col = c;
                    break;
                }
            }
            if (row != -1)
            {
                break;
            }
        }

        if (row == -1)
        {
            return true;
        }

        for (int value = 1; value <= BOARD_SIZE; value++)
        {
            if (IsValid(board, row, col, value))
            {
                board[row, col] = value;
                if (HasUniqueSolution(board))
                {
                    solutionCount++;
                }
                if (solutionCount > 1)
                {
                    return false;
                }
                board[row, col] = EMPTY_CELL;
            }
        }

        return solutionCount == 1;
    }

    private static bool IsValid(int[,] board, int row, int col, int val)
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
}
