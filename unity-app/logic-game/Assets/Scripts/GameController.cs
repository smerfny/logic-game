using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes;

public class GameController : MonoBehaviour {

    public GameObject menuObject;
    public GameObject levelMenuObject;

    private Menu menu;
    private LevelMenu levelMenu;

    private void Start() {
        menu = new Menu(menuObject);
        menu.Init();
        menu.PlayClicked = ChangeToLevelMenu;

        levelMenu = new LevelMenu(levelMenuObject);
        levelMenu.Init();
        levelMenu.BackClicked = ChangeToMenu;
    }

    private void ChangeToLevelMenu()
    {
        menu.Hide();
        levelMenu.Show();
    }

    private void ChangeToMenu()
    {
        menu.Show();
        levelMenu.Hide();
    }

    // Update is called once per frame
    void Update() {
		
	}
}
