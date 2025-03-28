using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int[,] board = new int[3, 3]; // 0 = Empty, 1 = X, -1 = 0
    private int[] rowSum = new int[3];
    private int[] colSum = new int[3];
    private int diagSum = 0, antiDiagSum = 0;
    private int _turn = -1;
    [HideInInspector]
    public UnityAction a_ResetGame;

    private void Awake()
    {
        a_ResetGame += resetGame;
    }

    public bool makeMove(int row, int col, int turn)
    {
        if (checkDraw())
        {
            return false;
        }

        int player = turn % 2 == 0 ? 1 : -1;
        board[row, col] = player;
        rowSum[row] += player;
        colSum[col] += player;
        if (row == col) diagSum += player;
        if (row + col == 2) antiDiagSum += player;

        if (Mathf.Abs(rowSum[row]) == 3 ||
            Mathf.Abs(colSum[col]) == 3 ||
            Mathf.Abs(diagSum) == 3 ||
            Mathf.Abs(antiDiagSum) == 3)
        {
            Debug.Log("Player " + (player == 1 ? "X" : "0") + " Has Won the game");// Winner (1 for X, -1 for O)
            a_ResetGame.Invoke();
            return false;
        }

        return true;
    }

    public int getCurrentTurn()
    {
        _turn++;        
        return _turn % 2;
    }

    public bool checkDraw()
    {
        if (_turn > 7)
        {
            Debug.Log("Game is a Draw");
            a_ResetGame.Invoke();
            return true;
        }
        return false;
    }

    private void resetGame()
    {
        _turn = -1;
        board = new int[3, 3];
        rowSum = new int[3];
        colSum = new int[3];
        diagSum = 0;
        antiDiagSum = 0;
    }
}
