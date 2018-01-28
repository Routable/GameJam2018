using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    public Player player;
    public Enemy enemy;
    public bool playerTurn;
    public GameObject start;

    public void StartGame()
    {
        player.SetupGame();
        enemy.SetupGame();

        start = GameObject.Find("Start Game");
        if (start != null && start.activeInHierarchy)
        {
            start.transform.GetChild(0).GetComponent<Text>().text = "START GAME";
            start.SetActive(false);
        }

        player.StartTurn();
    }
    public void PlayerTurnEnded(bool isPlayer)
    {
        if (isPlayer)
        {
            enemy.playPhase = true;
        }
        else
        {
            player.playPhase = true;
        }      
    }


    public void HandleWin(bool isPlayer)
    {
        if (isPlayer)
        {
            start.transform.GetChild(0).GetComponent<Text>().text = "YOU Win! TRY AGAIN?";
            player.playing = false;
            start.SetActive(true);
        }
        else
        {
            start.transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE! TRY AGAIN?";
            player.playing = false;
            start.SetActive(true);
        }
    }
}
