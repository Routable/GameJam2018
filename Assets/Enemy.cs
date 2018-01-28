using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Enemy : MonoBehaviour
{
    public List<Card> playerCards = new List<Card>();
    public List<TrapCard> trapCards;
    public Player player;
    public WinCondition condition;
    public DiscardPile discardPile;
    public int amountOfCardsPerTrapCard;
    public bool isTurn;
    public GetEnum ge;

    private bool playPhase;

    public Text aiText;

    public GameObject enemyTrapCardPrefab;

    public void StartAiTurn()
    {
        isTurn = true;
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
                trapCards[Random.Range(0, trapCards.Count)].UseCard();
            }
            else
            {
                break;
            }
        }

        EndTurn();
    }

    //during turn
    public void TryTradeCardsForTrapCard(GetEnum g)
    {
        if (playPhase && HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        {
            RemoveCards(amountOfCardsPerTrapCard, g.state);
            GameObject tc = Instantiate(enemyTrapCardPrefab, Vector3.zero, Quaternion.identity);
            trapCards.Add(tc.GetComponent<TrapCard>().GetNewTrapCard());
        }
    }

    //ending turn
    public void EndTurn()
    {
        if (playPhase)
        {
            playPhase = false;
            isTurn = false;
            player.StartTurn();
        }
    }

    public void DrawCard(int amount = 1)
    {
        while (amount > 0)
        {
            Card card = new Card();
            //Card animations and stuff here
            playerCards.Add(card);
            amount--;
        }
        CheckWinAndUpdateUI();
    }

    public void DiscardCard(GetEnum g)
    {
        if (g.state == CardType.None)
            return;

        discardPile.discardCards.Add(new Card(g.state));
        CheckWinAndUpdateUI();
    }

    private bool HasEnoughCards(CardType ct, int targetAmount)
    {
        if (playerCards.Where(x => x.cardType == ct).Count() >= targetAmount)
        {
            return true;
        }
        return false;
    }

    public void RemoveCards(int amount, CardType cardType = CardType.None)
    {
        while (amount > 0)
        {
            //random
            if (cardType == CardType.None)
            {
                if (playerCards.Count >= 0)
                {
                    playerCards.RemoveAt(Random.Range(0, playerCards.Count));
                }
            }
            else
            {
                if (playerCards.Where(x => x.cardType == cardType).Count() > 0)
                    playerCards.Remove(playerCards.First(x => x.cardType == cardType));
            }
            amount--;
        }
        CheckWinAndUpdateUI();
    }

    private void CheckWinAndUpdateUI()
    {
        Debug.Log("THIS IS THE AI");
        Debug.Log(condition.CheckWin(playerCards));
        aiText.text = "I has " + playerCards.Count() + " cards";
    }

    public void StartPlayerTurn()
    {
        player.StartTurn();
    }
}
