using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    public Player player;
    public Enemy enemy;
    public bool playerTurn;
    public GameObject start;

    public Image winImage;
    public Text winLostText;
    public Text winLostTitle;
    public Sprite winSprite;
    public Sprite loseSprite;

    public GameObject winLoseModal;

    public void Start()
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
            winImage.sprite = winSprite;
            winLostTitle.text = "YOU WIN!";
            winLostText.text = "Using all your gathered resources you were able to successfully grab the attention of a plane that was passing overhead.\nYou were able to escape!";

            player.playing = false;
            //start.SetActive(true);
        }
        else
        {
            winImage.sprite = loseSprite;
            winLostTitle.text = "YOU LOSE!";
            winLostText.text = "You hear the whirling blades of a helicopter in the distance. You drop everything and run towards the sound. In the distance, you see the helicopter with your arch-nemesis sitting in it.\nYour arch-nemesis has left you on the island. You lose!";

            player.playing = false;
            //start.SetActive(true);
        }

        winLoseModal.SetActive(true);
    }
}
