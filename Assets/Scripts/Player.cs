using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : PlayerBase {
    public Enemy ai;

    public Text rockAmount;
    public Text waterAmount;
    public Text woodAmount;
    public Text winCondition;
    public Text drawCardText;

    public GameObject trapCardPrefab;
    public Transform trapCardParent;

    public override void SetupGame()
    {
        base.SetupGame();
        drawCardText.text = "DRAW CARDS";
        winCondition.text = "Collect These:\nWood x " + condition.neededCards.Where(x => x == CardType.Wood).Count() + "\nWater x " + condition.neededCards.Where(x => x == CardType.Water).Count() + "\nRock x " + condition.neededCards.Where(x => x == CardType.Rock).Count();

    }

    public override void StartTurn()
    {
        base.StartTurn();
        drawCardText.text = "DRAW CARDS";
    }
    public void TappedStartEndButton(Text text)
    {
        if (playPhase)
        {
            if (text.text == "DRAW CARDS")
            {
                DrawCard(amountOfCardsPerTrapCard);
                text.text = "END TURN";
            }
            else if (text.text == "END TURN")
            {
                text.text = "WAITING FOR AI";
                EndTurn();
            }
        }
    }

    //during turn
    public override void TryTradeCardsForTrapCard(GetEnum g)
    {
        if (playPhase && HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        {
            RemoveCards(amountOfCardsPerTrapCard, g.state);
            GameObject tc = Instantiate(trapCardPrefab, Vector3.zero, Quaternion.identity);
            tc.transform.SetParent(trapCardParent);
            trapCards.Add(tc.GetComponent<TrapCard>().GetNewTrapCard(true));
        }
    }

    public override void CheckWin()
    {
        win = condition.CheckWin(playerCards);
        if (win)
        {
            gh.HandleWin(isPlayer);
        }

        //Updating Player's UI
        rockAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Rock).Count();
        waterAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Water).Count();
        woodAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Wood).Count();
    }
}
