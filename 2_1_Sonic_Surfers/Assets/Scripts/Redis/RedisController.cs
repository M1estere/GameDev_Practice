using StackExchange.Redis;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class RedisController : MonoBehaviour
{
    public static RedisController RedisControllerInstance { get; set; }

    [SerializeField] private string _ipAddress = "192.168.1.18";
    [SerializeField] private string _port = "6379";
    [SerializeField] private string _password = "";

    private ConnectionMultiplexer _conn;
    private IDatabase database;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (RedisControllerInstance == null) RedisControllerInstance = this;
        else DestroyObject(gameObject);

        _conn = ConnectionMultiplexer.Connect($"{_ipAddress}:{_port},allowadmin=true,password={_password}");

        database = _conn.GetDatabase(0);
        string str = database.StringGet("test-key");
        print(str);

        SetNewValue(PlayerPrefs.GetString("player_name"), 0);
    }

    public void SetNewValue(string key, int value)
    {
        string str = database.StringGet(key);
        if (str != null)
        {
            int score = Int32.Parse(str);
            if (score > value)
            {
                print($"Current score for {key} is more than {value} ({score})");
                return;
            } else
            {
                print($"Set score for {key} to {value} from {score}");
                database.StringSet(key, value);
                return;
            }
        }

        database.StringSet(key, value);
        // print($"Set score for {key} to {value}");
    }

    public string GetValue(string key) => database.StringGet(key);

    public Dictionary<string, int> GetAllValuesFromFolder(string folderName)
    {
        Dictionary<string, int> resultsDict = new ();

        RedisKey[] keys = _conn.GetServer(_conn.GetEndPoints().First()).Keys(database.Database, "*").ToArray();
        foreach (var key in keys)
        {
            if (KeyCorrect(folderName, key))
            {
                string nameKey = key.ToString().Replace($"{folderName}:", "").Trim();
                int value = ProcessValue(database.StringGet(key));

                resultsDict.Add(nameKey, value);
            }
        }

        return resultsDict;
    }

    private bool KeyCorrect(string format, string key)
    {
        if (key.Contains($"{format}:")) return true;

        return false;
    }

    private int ProcessValue(string value)
    {
        int result = 0;

        if (value == "") return result;
        if (Int32.TryParse(value, out result)) return result;

        return result;
    }
}