using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Classes
{
    public class GameplayBoardManager
    {
        public Action WinOccured;

        private Button[,] boardButtons;
        private bool[,] board;
        private int currentLevel;

        static private int boardCount = 5;
        static private Color trueColor = new Color(1, 0, 0.6f, 1);
        static private Color falseColor = new Color(0.6f, 1, 0, 1);

        public GameplayBoardManager(Button[,] boardButtons)
        {
            this.boardButtons = boardButtons;
        }

        public void Load(int level)
        {
            switch (level)
            {
                case 1:
                    board = new bool[5, 5]
                    {
                        { false, true, true, true, false},
                        { true, false, false, false, true},
                        { true, false, true, false, true},
                        { true, false, false, false, true},
                        { false, true, true, true, false}
                    };
                    break;
                case 2:
                    board = new bool[5, 5]
                    {
                        { false, false, true, true, true},
                        { true, false, false, false, true},
                        { true, true, true, false, false},
                        { true, false, false, false, true},
                        { false, false, true, true, true}
                    };
                    break;
                case 3:
                    board = new bool[5, 5]
                    {
                        { false, false, true, false, false},
                        { false, true, true, true, false},
                        { false, true, true, true, false},
                        { false, true, true, true, false},
                        { false, false, true, false, false}
                    };
                    break;
                case 4:
                    board = new bool[5, 5]
                    {
                        { true, true, true, true, true},
                        { true, true, true, true, true},
                        { true, true, true, true, true},
                        { true, true, true, true, true},
                        { true, true, true, true, true}
                    };
                    break;
                default:
                    board = new bool[5, 5]
                    {
                        { true, true, false, true, true},
                        { true, false, true, false, true},
                        { false, true, true, true, false},
                        { true, false, true, false, true},
                        { true, true, false, true, true}
                    };
                    break;
            }
            currentLevel = level;
            CheckWinAndRefresh();
        }

        public void Fire(int x, int y)
        {
            if (x - 1 >= 0) InternalFire(x - 1, y);
            if (x + 1 < boardCount) InternalFire(x + 1, y);
            if (y - 1 >= 0) InternalFire(x, y - 1);
            if (y + 1 < boardCount) InternalFire(x, y + 1);
            InternalFire(x, y);
            if (CheckWinAndRefresh())
            {
                OnWin();
            }
        }

        private void InternalFire(int x, int y)
        {
            board[x, y] = !board[x, y];
        }

        private bool CheckWinAndRefresh()
        {
            bool result = true;
            for (var i = 0; i < boardCount; i++)
            {
                for (var j = 0; j < boardCount; j++)
                {
                    boardButtons[i, j].image.color = board[i, j] ? trueColor : falseColor;
                    result = !board[i, j] && result;
                }
            }
            return result;
        }

        private void OnWin()
        {
            if (WinOccured == null) return;
            WinOccured();
        }
    }
}
