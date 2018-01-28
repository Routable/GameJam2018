using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CurrntPlayer : Player {

    private bool playPhase;
    public Text rockAmount;
    public Text waterAmount;
    public Text woodAmount;

    public GameObject trapCardPrefab;
    public Transform trapCardParent;

    private void UpdateUI()
    {
        rockAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Rock).Count();
        waterAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Water).Count();
        woodAmount.text = " x " + playerCards.Where(x => x.cardType == CardType.Wood).Count();
    }
}
