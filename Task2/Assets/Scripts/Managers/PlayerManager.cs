using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // player manager instance
    public static PlayerManager Instance;
    // player data
    public PlayerData Data;

    // set defaults
    void Awake()
    {
        Instance = this;
        LoadPlayerData();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // load local player data
    public void LoadPlayerData()
    {
        // get json string with playerprefs
        var str = PlayerPrefs.GetString("player_data");

        if (!string.IsNullOrEmpty(str))
        {
            Data = JsonConvert.DeserializeObject<PlayerData>(str);
        }
        else
        {
            Data = new PlayerData();
        }
    }

    // save player data to playerprefs
    public void SavePlayerData()
    {
        var str = JsonConvert.SerializeObject(Data);
        PlayerPrefs.SetString("player_data", str);
    }

    // save score function
    public void SaveScore(int score)
    {
        Data.LastScore = score;

        // if score is highest then set new high score
        if (score > Data.HighestScore)
        {
            Data.HighestScore = score;
        }

        SavePlayerData();
    }
}
