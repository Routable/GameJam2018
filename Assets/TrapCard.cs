﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapCardType
{
    AddThreeCards
}

public class TrapCard : MonoBehaviour {
    TrapCardType trapCardType;

    public TrapCard() {
        //TODO: fancy math for weighted cards?
        Array values = Enum.GetValues(typeof(TrapCardType));
        System.Random random = new System.Random();
        trapCardType = (TrapCardType)values.GetValue(random.Next(values.Length));
    }

    public void UseCard(Player usingPlayer = null, Player targetPlayer = null)
    {
        switch (trapCardType)
        {
            case TrapCardType.AddThreeCards:
                //do stuff
                break;

            default:
                Debug.LogError("Danny you are dumb");
                break;
        }
    }
}
