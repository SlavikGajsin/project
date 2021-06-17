using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadSystem
{

    private static string playerInfoPath = Application.persistentDataPath + "/playerInfo.dat";

    public static void SavePlayer (PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = playerInfoPath;
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(script);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer() {
        string path = playerInfoPath;

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    
    private static string highscoresPath = Application.persistentDataPath + "/highscores.dat";

    public static void SaveHighscore(HighscoreInfoModel infoModelToSave) {
        BinaryFormatter formatter = new BinaryFormatter();

        List<HighscoreInfoModel> scoresToSave = new List<HighscoreInfoModel>();

        // считываем сохраненные ранее рекорды
        var savedScores = GetAllHighscores();
        scoresToSave.AddRange(savedScores);

        // добавляем новый рекорд в конец списка
        scoresToSave.Add(infoModelToSave);

        // создаём модель-обёртку для нового списка
        PlayerHighscoresModel newHighscoresModel = new PlayerHighscoresModel(scoresToSave.ToArray());
        
        // запись модели в файл
        FileStream writeStream = new FileStream(highscoresPath, FileMode.Create);
        formatter.Serialize(writeStream, newHighscoresModel);
        writeStream.Close();
    }

    public static HighscoreInfoModel[] GetAllHighscores() {
        BinaryFormatter formatter = new BinaryFormatter();

        List<HighscoreInfoModel> savedScores = new List<HighscoreInfoModel>();

        // если файл существует - считываем рекорды 
        if (File.Exists(highscoresPath)) {
            FileStream readStream = new FileStream(highscoresPath, FileMode.Open);
            PlayerHighscoresModel savedHighscoresModel = formatter.Deserialize(readStream) as PlayerHighscoresModel;
            readStream.Close();
            savedScores.AddRange(savedHighscoresModel.scores);
        }

        return savedScores.ToArray();
    }


}



[System.Serializable]
public class PlayerHighscoresModel {
    public HighscoreInfoModel[] scores;

    public PlayerHighscoresModel(HighscoreInfoModel[] scores){
        this.scores = scores;
    }


}
