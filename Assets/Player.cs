using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : MonoBehaviour {
    public List<Card> playerCards = new List<Card>();
    public List<TrapCard> trapCards;
    public Player ai;
    public WinCondition condition;
    public DiscardPile discardPile;
    public int amountOfCardsPerTrapCard;
    public bool isTurn;

    public bool isAi;

    private bool playPhase;
    public Text rockAmount;
    public Text waterAmount;
    public Text woodAmount;

    public Text aiText;

    public GameObject trapCardPrefab;
    public Transform trapCardParent;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isAi)
                StartTurn();
        }
    }

    public void StartTurn()
    {
        isTurn = true;
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
        if (!isAi)
        {
            if (playPhase)
            {
                playPhase = false;
                isTurn = false;
                StartAiTurn();
            }
        }
        else
        {
            if (playPhase)
            {
                playPhase = false;
                isTurn = false;
                //this is actually the player
                ai.StartTurn();
            }
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
        if (!isAi)
        {
            Debug.Log("THIS IS THE PLAYER");
            Debug.Log(condition.CheckWin(playerCards));
            rockAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Rock).Count();
            waterAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Water).Count();
            woodAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Wood).Count();
        }
        //this is run from AI, this is why we dont code stupidly like this
        else
        {
            Debug.Log("THIS IS THE AI");
            Debug.Log(condition.CheckWin(playerCards));
            aiText.text = "I has " + playerCards.Count() + " cards";
        }
    }

    public void StartAiTurn()
    {
        ai.StartTurn();


        //AI checks for extra cards and buys trap cards
        GetEnum ge = new GetEnum();
        ge.state = CardType.Wood;
        if (ai.condition.CheckEnoughCardsOfType(CardType.Wood, ai.playerCards, ai.amountOfCardsPerTrapCard))
            TryTradeCardsForTrapCard(ge);

        ge.state = CardType.Water;
        if (ai.condition.CheckEnoughCardsOfType(CardType.Water, ai.playerCards, ai.amountOfCardsPerTrapCard))
            TryTradeCardsForTrapCard(ge);

        ge.state = CardType.Rock;
        if (ai.condition.CheckEnoughCardsOfType(CardType.Rock, ai.playerCards, ai.amountOfCardsPerTrapCard))
            TryTradeCardsForTrapCard(ge);


        //Make AI use random trap cards
        while (ai.trapCards.Count() > 0)
        {
            if (Random.Range(0, 3) > 1)
            {
                ai.trapCards[Random.Range(0, ai.trapCards.Count)].UseCard();
            }
            else
            {
                break;
            }
        }

        ai.EndTurn();
    }
}
