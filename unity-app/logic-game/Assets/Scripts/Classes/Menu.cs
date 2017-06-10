using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Classes
{
    public class Menu : Base.BaseUI
    {
        public Action PlayClicked;
        
        private InputField playerInput;
        private Button playButton;

        private string playerName;

        public Menu(GameObject menuObject) : base(menuObject)
        {
        }

        public void Init()
        {
            LoadReferences();
            LoadSettings();
            AttachEvents();
        }

        private void LoadReferences()
        {
            playerInput = GetReference<InputField>("Player");
            playButton = GetReference<Button>("Play");
        }

        private void LoadSettings()
        {
            if (!PlayerPrefs.HasKey("playerName"))
            {
                playerName = "Player";
                PlayerPrefs.SetString("playerName", playerName);
            } else
            {
                playerName = PlayerPrefs.GetString("playerName");
            }
            playerInput.text = playerName;
        }

        private void AttachEvents()
        {
            playerInput.onEndEdit.AddListener(delegate { OnPlayerEndEdit(); });
            playButton.onClick.AddListener(OnPlayClick);
        }

        private void OnPlayerEndEdit()
        {
            playerName = playerInput.text;
            PlayerPrefs.SetString("playerName", playerName);
        }

        private void OnPlayClick()
        {
            if (PlayClicked == null) return;
            PlayClicked();
        }

        public string GetPlayerName()
        {
            return playerName;
        }
    }
}
