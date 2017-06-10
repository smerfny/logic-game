using UnityEngine;
using Assets.Scripts.Classes;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject menuObject;
    public GameObject levelMenuObject;
    public GameObject gameplayObject;
    public GameObject completeObject;

    private Menu menu;
    private LevelMenu levelMenu;
    private Gameplay gameplay;
    private Complete complete;

    private void Start() {
        menu = new Menu(menuObject);
        menu.Init();
        menu.PlayClicked = ChangeToLevelMenu;

        levelMenu = new LevelMenu(levelMenuObject);
        levelMenu.Init();
        levelMenu.BackClicked = ChangeToMenu;
        levelMenu.LoadLevelClicked = ChangeToLevel;

        gameplay = new Gameplay(gameplayObject);
        gameplay.Init();
        gameplay.LeaveClicked = ChangeToLevelMenu;
        gameplay.WinOccured = ChangeToComplete;

        complete = new Complete(completeObject);
        complete.Init();
        complete.MenuClicked = ChangeToMenu;
        complete.NextClicked = ChangeToLevel;
        complete.SendClicked = SendScore;

        ChangeToMenu();
    }

    private void ChangeToLevelMenu()
    {
        menu.Hide();
        levelMenu.Show();
        gameplay.Hide();
        complete.Hide();
    }

    private void ChangeToMenu()
    {
        menu.Show();
        levelMenu.Hide();
        gameplay.Hide();
        complete.Hide();
    }

    private void ChangeToLevel(int level)
    {
        menu.Hide();
        levelMenu.Hide();
        gameplay.LoadLevel(level);
        gameplay.Show();
        complete.Hide();
    }

    private void ChangeToComplete(int level, int steps)
    {
        menu.Hide();
        levelMenu.Hide();
        gameplay.Hide();
        complete.Show(level, steps);
    }

    private void SendScore(int level, int steps)
    {
        StartCoroutine(Upload(level + 1, steps));
    }

    private IEnumerator Upload(int level, int steps)
    {
        WWWForm form = new WWWForm();
        form.AddField("player", menu.GetPlayerName());
        form.AddField("level", level.ToString());
        form.AddField("steps", steps.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://serwer1703374.home.pl/logic-game/save.php", form);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

        Application.OpenURL("http://serwer1703374.home.pl/logic-game/");
    }
}
