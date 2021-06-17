using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public int coinAmount;   
    public int firstShipLevel;
    public int secondShipLevel;
}

[System.Serializable]
public class HighscoreInfoModel {
    public int score;
    public DateTime date;

    public HighscoreInfoModel(int score, DateTime date){
        this.score = score;
        this.date = date;
    }

    public HighscoreInfoModel(int score) {
        this.score = score;
        this.date = System.DateTime.Now;
    }
}
