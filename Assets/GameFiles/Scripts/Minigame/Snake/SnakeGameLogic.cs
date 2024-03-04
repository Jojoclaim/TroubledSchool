using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace Minigame.Snake
{
    public class SnakeGameLogic : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI debugScore;
        [SerializeField] public SpriteRenderer[] cell;

        [SerializeField] Color snakeColor, fruitColor, emptyColor, borderColor;

        [SerializeField] public int boardWidth = 10;
        [SerializeField] public int boardHeight = 10;
        [SerializeField] int snakeLength = 1;
        [SerializeField] int direction = 1;
        [SerializeField] float moveCooldown;

        [SerializeField] UnityEvent OnStartGame, OnEndGame, OnExitGame, OnWin;


        Vector2Int position;
        int curentSnakeLength;
        int Score;


        int ConvertToListElement(int x, int y)
        {
            int returnInt;
            //returnInt = (y * (boardWidth + 1)) + x;
            returnInt = ((y - 1) * boardWidth) + x - 1;
            return returnInt;
        }

        public void Update()
        {
            Dictionary<KeyCode, int> keyToDirection = new Dictionary<KeyCode, int>
        {
            { KeyCode.W, 0 },
            { KeyCode.D, 1 },
            { KeyCode.S, 2 },
            { KeyCode.A, 3 }
        };

            foreach (var key in keyToDirection.Keys)
            {
                if (Input.GetKeyDown(key))
                {
                    direction = keyToDirection[key];
                    break;
                }
            }
        }
        public void StartGame()
        {
            curentSnakeLength = snakeLength;
            position = new Vector2Int(8, 5);
            OnStartGame.Invoke();
            StartCoroutine(CallNextMove());
            for (int x = 0; x < cell.Length; x++)
            {
                cell[x].color = emptyColor;
            }
            ColorBorder();
            AddFruit();
            Score = 0;
            debugScore.text = "Score - " + Score + "/15";
        }

        private void ColorBorder()
        {
            for (int x = 1; x <= boardWidth; x++)
            {
                cell[ConvertToListElement(x, 1)].color = borderColor;
                cell[ConvertToListElement(x, boardHeight)].color = borderColor;
            }
            for (int y = 1; y <= boardHeight; y++)
            {
                cell[ConvertToListElement(1, y)].color = borderColor;
                cell[ConvertToListElement(boardWidth, y)].color = borderColor;
            }
        }

        public void AddFruit()
        {
            int RandomInt = Random.Range(0, ConvertToListElement(boardWidth, boardHeight));
            while (cell[RandomInt].color != emptyColor)
            {
                RandomInt = Random.Range(0, ConvertToListElement(boardWidth, boardHeight));
            }
            cell[RandomInt].color = fruitColor;
        }

        public void EndGame()
        {
            OnEndGame.Invoke();
            StopAllCoroutines();
            if (Score >= 15)
            {
                OnWin.Invoke();
            }
        }

        public void ReturnTo3D()
        {
            OnExitGame.Invoke();
            StopAllCoroutines();
            if (Score >= 15)
            {
                OnWin.Invoke();
            }
        }

        void AddSegment()
        {
            curentSnakeLength++;
            Score++;
            debugScore.text = "Score - " + Score + "/15";
        }

        void MoveSnake()
        {
            Vector2Int newPos = position + GetDirectionVector();

            if (cell[ConvertToListElement(newPos.x, newPos.y)].color == snakeColor || newPos.x == 1 || newPos.y == 1 || newPos.x >= boardWidth || newPos.y >= boardHeight)
            {
                EndGame();
                ReturnTo3D();
            }
            else if (cell[ConvertToListElement(newPos.x, newPos.y)].color == fruitColor)
            {
                AddFruit();
                AddSegment();
            }

            if (cell[ConvertToListElement(newPos.x, newPos.y)] != null)
            {
                position = newPos;
            }
            else
            {
                EndGame();
                ReturnTo3D();
            }
            cell[ConvertToListElement(position.x, position.y)].color = snakeColor;
            StartCoroutine(RemoveSnakeCell(ConvertToListElement(position.x, position.y)));
        }

        Vector2Int GetDirectionVector()
        {
            return direction switch
            {
                0 => Vector2Int.down,
                1 => Vector2Int.right,
                2 => Vector2Int.up,
                3 => Vector2Int.left,
                _ => Vector2Int.right,
            };
        }

        IEnumerator CallNextMove()
        {
            while (true)
            {
                yield return new WaitForSeconds(moveCooldown);
                MoveSnake();
            }
        }

        IEnumerator RemoveSnakeCell(int _localCell)
        {
            int _curentLocal = _localCell;
            int _Steps = 0;
            while (curentSnakeLength - _Steps > 0)
            {
                yield return new WaitForSeconds(moveCooldown);
                _Steps++;
            }
            if (cell[_curentLocal].color != borderColor && cell[_curentLocal].color != fruitColor)
            {
                cell[_curentLocal].color = emptyColor;
            }
        }
    }
}