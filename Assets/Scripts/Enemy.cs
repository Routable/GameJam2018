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
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);
        DoAiThings();
        EndTurn();
    }

    public void DoAiThings()
    {
        DrawCard(2);

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
    }

    //during turn
    public override void TryTradeCardsForTrapCard(GetEnum g)
    {
        if (HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        {
            RemoveCards(amountOfCardsPerTrapCard, g.state);
            GameObject tc = Instantiate(enemyTrapCardPrefab, Vector3.zero, Quaternion.identity);
            tc.GetComponent<TrapCard>().SetValues(tcdb.GetTrapCardValues());
            trapCards.Add(tc.GetComponent<TrapCard>());
        }
    }

    public override void CheckWin()
    {
        win = condition.CheckWin(playerCards);
        if (win)
        {
            player.playing = false;
            gh.HandleWin(isPlayer);
        }

        int number = condition.CheckWinPercent(playerCards);

        aiText.text = "The AI has " + playerCards.Count() + " resources.\nThey have collected " + number + " resources\nneeded to get off the island.";
    }
}
