using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartGamePanel : Panel
{
    [SerializeField] protected Button startGameButton;

     protected override void LoadComponents()
    {
        base.LoadComponents();
        startGameButton = this.transform.Find("StartGameButton").GetComponent<Button>();
        startGameButton.onClick.AddListener(delegate {StartGame();});
        //exitMenuButton.onClick.AddListener(GameScreen.instance.Hide);
    }

    protected virtual void StartGame() {
        GameScreen.instance.StartScreenGame();
        GameManager.instance.LoadMap(1);
    }
}
