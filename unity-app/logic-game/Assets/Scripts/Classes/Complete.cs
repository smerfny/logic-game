using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Classes
{
    public class Complete : Base.BaseUI
    {
        public Action<int> NextClicked;
        public Action MenuClicked;
        public Action<int,int> SendClicked;

        private Button nextButton;
        private Button menuButton;
        private Button sendButton;
        private Text congratsText;
        private int currentLevel;
        private int steps;

        public Complete(GameObject menuObject) : base(menuObject)
        {
        }

        public void Init()
        {
            LoadReferences();
            AttachEvents();
        }

        private void LoadReferences()
        {
            nextButton = GetReference<Button>("Next");
            menuButton = GetReference<Button>("Menu");
            sendButton = GetReference<Button>("SendScore");
            congratsText = GetReference<Text>("CongratsTxt");
        }

        private void AttachEvents()
        {
            nextButton.onClick.AddListener(() => OnNextClick(currentLevel));
            menuButton.onClick.AddListener(OnMenuClick);
            sendButton.onClick.AddListener(() => OnSendClick(currentLevel, steps));
        }

        private void OnNextClick(int level)
        {
            if (NextClicked == null) return;
            NextClicked(level + 1);
        }

        private void OnMenuClick()
        {
            if (MenuClicked == null) return;
            MenuClicked();
        }

        private void OnSendClick(int level, int steps)
        {
            if (SendClicked == null) return;
            SendClicked(level, steps);
        }

        public void Show(int level, int steps)
        {
            this.currentLevel = level;
            this.steps = steps;
            congratsText.text = string.Format("You completed level {0} in {1} steps.", level + 1, steps);
            if (level < 4)
            {
                nextButton.gameObject.SetActive(true);
                menuButton.gameObject.SetActive(false);
            } else
            {
                nextButton.gameObject.SetActive(false);
                menuButton.gameObject.SetActive(true);
            }
            Show();
        }
    }
}
