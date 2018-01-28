using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapCardType
{
    AddThreeCards
}

public class TrapCard : MonoBehaviour {
    TrapCardType trapCardType;

    public TrapCard GetNewTrapCard() {
        //TODO: fancy math for weighted cards?
        Array values = Enum.GetValues(typeof(TrapCardType));
        System.Random random = new System.Random();
        trapCardType = (TrapCardType)values.GetValue(random.Next(values.Length));

        return (TrapCard)this.MemberwiseClone();
    }

    public void UseCard(Player usingPlayer = null, Player targetPlayer = null)
    {
        switch (trapCardType)
        {
            case TrapCardType.AddThreeCards:
                usingPlayer.DrawCard(3);
                break;

            default:
                Debug.LogError("Danny you are dumb");
                break;
        }

        Destroy(this.gameObject);
    }
}
