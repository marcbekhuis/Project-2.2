using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkylanderPuzzleBoard : MonoBehaviour
{
    public char[,] board;
    public RectTransform[,] boardGameObjects;

    [Space]
    [SerializeField] private RectTransform boardParent;

    [SerializeField] private GameObject end;
    [SerializeField] private GameObject floor;

    [Space]
    [SerializeField] private string[] inputBoard;

    public Dictionary<RectTransform, Vector2Int> movables = new Dictionary<RectTransform, Vector2Int>();
    public RectTransform placedPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private RectTransform playerParent;
    [SerializeField] private GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        board = new char[inputBoard.Length, inputBoard[0].Length];
        boardGameObjects = new RectTransform[inputBoard.Length, inputBoard[0].Length];
        playerParent.sizeDelta = new Vector2(5 + 100 * board.GetLength(1) + 5 * board.GetLength(1), 5 + 100 * board.GetLength(0) + 5 * board.GetLength(0));
        if ((board.GetLength(1) - 3) > (board.GetLength(0) - 3))
        {
            float temp = Mathf.Clamp((float)1 - ((float)(board.GetLength(1) - 3) * 0.1f),0,1);
            playerParent.localScale = new Vector3(temp, temp, 1);
        }
        else
        {
            float temp = Mathf.Clamp((float)1 - ((float)(board.GetLength(0) - 3) * 0.1f), 0, 1);
            playerParent.localScale = new Vector3(temp, temp, 1);
        }

        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                board[x, y] = inputBoard[x][y];
                switch (board[x,y])
                {
                    case '_':
                        boardGameObjects[x,y] = Instantiate(floor, new Vector3(0,0,0), new Quaternion(0,0,0,0),boardParent).GetComponent<RectTransform>();
                        boardGameObjects[x, y].anchoredPosition = new Vector2(5 + 100 * y + 5 * y, -5 - 100 * x - 5 * x);
                        break;
                    case 'O':
                        boardGameObjects[x, y] = Instantiate(floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), boardParent).GetComponent<RectTransform>();
                        boardGameObjects[x, y].anchoredPosition = new Vector2(5 + 100 * y + 5 * y, -5 - 100 * x - 5 * x);
                        placedPlayer = Instantiate(player, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), playerParent).GetComponent<RectTransform>();
                        placedPlayer.anchoredPosition = boardGameObjects[x, y].anchoredPosition;
                        movables.Add(placedPlayer,new Vector2Int(x,y));
                        break;
                    case 'X':
                        boardGameObjects[x, y] = Instantiate(end, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), boardParent).GetComponent<RectTransform>();
                        boardGameObjects[x, y].anchoredPosition = new Vector2(5 + 100 * y + 5 * y, -5 - 100 * x - 5 * x);
                        break;
                    case '#':
                        boardGameObjects[x, y] = Instantiate(wall, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), boardParent).GetComponent<RectTransform>();
                        boardGameObjects[x, y].anchoredPosition = new Vector2(5 + 100 * y + 5 * y, -5 - 100 * x - 5 * x);
                        break;
                    default:
                        Debug.Log("Something Broke");
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
