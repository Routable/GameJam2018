using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public List<Card> playerCards = new List<Card>();
    public List<TrapCard> trapCards;
    public List<Player> otherPlayers;
    public WinCondition condition;
    public DiscardPile discardPile;
    [SyncVar]
    public int amountOfCardsPerTrapCard;
    public bool isTurn;

    private bool playPhase;
    public Text rockAmount;
    public Text waterAmount;
    public Text woodAmount;

    public GameObject trapCardPrefab;
    public Transform trapCardParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartPlayerTurn();
        }
    }

    //start of turn
    public void StartPlayerTurn()
    {
        DrawCard(2);
        playPhase = true;
    }

    //during turn
    public void TryTradeCardsForTrapCard(GetEnum g)
    {
        if (playPhase && HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        {
            RemoveCards(amountOfCardsPerTrapCard, g.state);
            GameObject tc = Instantiate(trapCardPrefab, Vector3.zero, Quaternion.identity);
            tc.transform.SetParent(trapCardParent);
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
        Debug.Log(condition.CheckWin(playerCards));
        rockAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Rock).Count();
        waterAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Water).Count();
        woodAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Wood).Count();
    }
}
