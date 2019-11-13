using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkylanderPuzzleMovement : MonoBehaviour
{
    [SerializeField] private Transform board;
    [SerializeField] private Rotations rotation = Rotations.Bottom;

    private SkylanderPuzzleBoard skylanderPuzzleBoard;

    private enum Rotations
    {
        top,
        Left,
        Bottom,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        skylanderPuzzleBoard = GetComponent<SkylanderPuzzleBoard>();

        rotation = Rotations.Bottom;
        Physics();
    }

    public void RotateBoardLeft()
    {
        board.Rotate(new Vector3(0, 0, 90));
        switch (rotation)
        {
            case Rotations.Bottom:
                rotation = Rotations.Left;
                Physics();
                break;
            case Rotations.Left:
                rotation = Rotations.top;
                Physics();
                break;
            case Rotations.Right:
                rotation = Rotations.Bottom;
                Physics();
                break;
            case Rotations.top:
                rotation = Rotations.Right;
                Physics();
                break;
        }
    }

    public void RotateBoardRight()
    {
        board.Rotate(new Vector3(0, 0, -90));
        switch (rotation)
        {
            case Rotations.Bottom:
                rotation = Rotations.Right;
                Physics();
                break;
            case Rotations.Left:
                rotation = Rotations.Bottom;
                Physics();
                break;
            case Rotations.Right:
                rotation = Rotations.top;
                Physics();
                break;
            case Rotations.top:
                rotation = Rotations.Left;
                Physics();
                break;
        }
    }

    private void Physics()
    {
        foreach (var movingObject in skylanderPuzzleBoard.movables.ToList())
        {
            switch (rotation)
            {
                case Rotations.Bottom:
                    for (int x = movingObject.Value.x + 1; x < skylanderPuzzleBoard.board.GetLength(0); x++)
                    {
                        if (skylanderPuzzleBoard.board[x, movingObject.Value.y] == '#')
                        {
                            break;
                        }
                        else
                        {
                            skylanderPuzzleBoard.board[movingObject.Value.x, movingObject.Value.y] = '_';
                            skylanderPuzzleBoard.movables[movingObject.Key] = new Vector2Int(x, movingObject.Value.y);
                            movingObject.Key.anchoredPosition = skylanderPuzzleBoard.boardGameObjects[x, movingObject.Value.y].anchoredPosition;
                            WinCheck(x, movingObject.Value.y);
                        }
                    }
                    break;
                case Rotations.Left:
                    for (int y = movingObject.Value.y - 1; y >= 0; y--)
                    {
                        if (skylanderPuzzleBoard.board[movingObject.Value.x, y] == '#')
                        {
                            break;
                        }
                        else
                        {
                            skylanderPuzzleBoard.board[movingObject.Value.x, movingObject.Value.y] = '_';
                            skylanderPuzzleBoard.movables[movingObject.Key] = new Vector2Int(movingObject.Value.x, y);
                            movingObject.Key.anchoredPosition = skylanderPuzzleBoard.boardGameObjects[movingObject.Value.x, y].anchoredPosition;
                            WinCheck(movingObject.Value.x, y);
                        }
                    }
                    break;
                case Rotations.Right:
                    for (int y = movingObject.Value.y + 1; y < skylanderPuzzleBoard.board.GetLength(1); y++)
                    {
                        if (skylanderPuzzleBoard.board[movingObject.Value.x, y] == '#')
                        {
                            break;
                        }
                        else
                        {
                            skylanderPuzzleBoard.board[movingObject.Value.x, movingObject.Value.y] = '_';
                            skylanderPuzzleBoard.movables[movingObject.Key] = new Vector2Int(movingObject.Value.x, y);
                            movingObject.Key.anchoredPosition = skylanderPuzzleBoard.boardGameObjects[movingObject.Value.x, y].anchoredPosition;
                            WinCheck(movingObject.Value.x, y);
                        }
                    }
                    break;
                case Rotations.top:
                    for (int x = movingObject.Value.x - 1; x >= 0; x--)
                    {
                        if (skylanderPuzzleBoard.board[x, movingObject.Value.y] == '#')
                        {
                            break;
                        }
                        else
                        {
                            skylanderPuzzleBoard.board[movingObject.Value.x, movingObject.Value.y] = '_';
                            skylanderPuzzleBoard.movables[movingObject.Key] = new Vector2Int(x, movingObject.Value.y);
                            movingObject.Key.anchoredPosition = skylanderPuzzleBoard.boardGameObjects[x, movingObject.Value.y].anchoredPosition;
                            WinCheck(x, movingObject.Value.y);
                        }
                    }
                    break;
            }
            
        }
    }

    private void WinCheck(int x, int y)
    {
        if(skylanderPuzzleBoard.board[x, y] == 'X')
        {
            Debug.Log("You Won");
        }
        else
        {
            skylanderPuzzleBoard.board[x, y] = 'O';
        }
    }
}
