using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player's turn state
//public enum TurnState

public class Player : MonoBehaviour {
    public List<Card> playerCards;
    public WinCondition condition;
    public bool isTurn;
    public TurnState state;



}
