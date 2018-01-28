using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    public Player player;
    public Enemy enemy;
    public bool playerTurn;

    public void StartGame()
    {
        player.SetupGame();
        enemy.SetupGame();

        var go = GameObject.Find("Start Game");
        if (go != null && go.activeInHierarchy)
        {
            go.SetActive(false);
        }

        player.StartTurn();
    }
    public void PlayerTurnEnded(bool isPlayer)
    {
        if (isPlayer)
        {
            enemy.StartTurn();
        }
        else
        {
            player.StartTurn();
        }      
    }


    public void HandleWin(bool isPlayer)
    {
        Debug.Log("Won");
    }
}
