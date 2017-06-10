using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Classes
{
    public class LevelMenu : Base.BaseUI
    {
        public Action<int> LoadLevelClicked;
        public Action BackClicked;

        private Button[] levelButtons;
        private Button backButton;
        static private int buttonCount = 5;

        public LevelMenu(GameObject menuObject) : base(menuObject)
        {
        }

        public void Init()
        {
            LoadReferences();
            AttachEvents();
        }

        private void LoadReferences()
        {
            levelButtons = new Button[buttonCount];
            for (var i = 0; i < buttonCount; i++)
            {
                levelButtons[i] = GetReference<Button>(string.Format("L{0}", i + 1));
            }
            backButton = GetReference<Button>("Back");
        }

        private void AttachEvents()
        {
            for (var i = 0; i < buttonCount; i++)
            {
                levelButtons[i].onClick.AddListener(() => OnLevelClick(i));
            }
            backButton.onClick.AddListener(OnBackClick);
        }

        private void OnLevelClick(int level)
        {
            if (LoadLevelClicked == null) return;
            LoadLevelClicked(level);
        }

        private void OnBackClick()
        {
            if (BackClicked == null) return;
            BackClicked();
        }
    }
}
