using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Enemy : PlayerBase
{
    public Player player;
    public GetEnum ge;
    public Text aiText;
    public GameObject enemyTrapCardPrefab;

    public override void StartTurn()
    {
        DrawCard(2);
        playPhase = true;

        //AI checks for extra cards and buys trap cards
        ge.SetState(CardType.Wood);
        if (condition.CheckEnoughCardsOfType(CardType.Wood, playerCards, amountOfCardsPerTrapCard))
            TryTradeCardsForTrapCard(ge);

        ge.SetState(CardType.Water);
        if (condition.CheckEnoughCardsOfType(CardType.Water, playerCards, amountOfCardsPerTrapCard))
            TryTradeCardsForTrapCard(ge);

        ge.SetState(CardType.Rock);
        if (condition.CheckEnoughCardsOfType(CardType.Rock, playerCards, amountOfCardsPerTrapCard))
            TryTradeCardsForTrapCard(ge);


        //Make AI use random trap cards
        while (trapCards.Count() > 0)
        {
            if (Random.Range(0, 3) > 1)
            {
                trapCards[Random.Range(0, trapCards.Count)].UseCard(isPlayer);
            }
            else
            {
                break;
            }
        }

        EndTurn();
    }

    //during turn
    public override void TryTradeCardsForTrapCard(GetEnum g)
    {
        if (playPhase && HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        {
            RemoveCards(amountOfCardsPerTrapCard, g.state);
            GameObject tc = Instantiate(enemyTrapCardPrefab, Vector3.zero, Quaternion.identity);
            trapCards.Add(tc.GetComponent<TrapCard>().GetNewTrapCard());
        }
    }

    public override void CheckWin()
    {
        win = condition.CheckWin(playerCards);
        if (win)
        {
            gh.HandleWin(isPlayer);
        }

        aiText.text = "I has " + playerCards.Count() + " cards";
    }
}
