using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int TotalCoins;
    public float HighestScore;

	public PlayerData (Player player)
    {
        if(HighestScore < player._score)
        {
            HighestScore = player._score;
        }

        TotalCoins += player._coins;
    }
}
