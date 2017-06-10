using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Classes
{
    public class Gameplay : Base.BaseUI
    {
        public Action LeaveClicked;
        public Action<int, int> WinOccured;

        private Button[,] boardButtons;
        private Button restartButton;
        private Button leaveButton;
        private Text stepsText;
        private Text levelNameText;
        private int currentLevel;
        private GameplayBoardManager gbm;
       
        static private int boardCount = 5;

        private int _steps;
        private int steps {
            get { return _steps; }
            set {
                _steps = value;
                stepsText.text = string.Format("Steps: {0}", _steps);
            }
        }

        public Gameplay(GameObject menuObject) : base(menuObject)
        {
        }

        public void Init()
        {
            LoadReferences();
            AttachEvents();
        }

        public void LoadLevel(int level)
        {
            currentLevel = level;
            steps = 0;
            levelNameText.text = string.Format("Level {0}", level + 1);
            gbm.Load(level);
        }

        private void LoadReferences()
        {
            boardButtons = new Button[boardCount, boardCount];
            for (var i = 0; i < boardCount; i++)
            {
                for (var j = 0; j < boardCount; j++)
                {
                    boardButtons[i, j] = GetReference<Button>(string.Format("Board/T{1} ({0})", i + 1, j + 1));
                }
            }
            leaveButton = GetReference<Button>("Leave");
            restartButton = GetReference<Button>("Reset");
            stepsText = GetReference<Text>("Steps");
            levelNameText = GetReference<Text>("LevelName");
            gbm = new GameplayBoardManager(boardButtons);
        }

        private void AttachEvents()
        {
            for (var i = 0; i < boardCount; i++)
            {
                for (var j = 0; j < boardCount; j++)
                {
                    int x = i, y = j;
                    boardButtons[i, j].onClick.AddListener(() => OnBoardClick(x, y));
                }
            }
            restartButton.onClick.AddListener(OnRestartClick);
            leaveButton.onClick.AddListener(OnLeaveClick);
            gbm.WinOccured = OnWinOccure;
        }

        private void OnBoardClick(int x, int y)
        {
            steps++;
            gbm.Fire(x, y);
        }

        private void OnRestartClick()
        {
            LoadLevel(currentLevel);
        }

        private void OnLeaveClick()
        {
            if (LeaveClicked == null) return;
            LeaveClicked();
        }

        private void OnWinOccure()
        {
            if (WinOccured == null) return;
            WinOccured(currentLevel, steps);
        }
    }
}
