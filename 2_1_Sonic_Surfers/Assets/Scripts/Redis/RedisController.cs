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

        SetUserData();
    }

    private void SetUserData()
    {
        string username = PlayerPrefs.GetString("player_name");
        string password = PlayerPrefs.GetString("player_password");
        DateTime dateTime = DateTime.UtcNow.Date;

        string fieldName = "players:" + username;

        string str = database.HashGet(fieldName, username);
        if (str != null)
        {
            SetNewValue(username, 0);
            return;
        }

        HashEntry[] entries = new HashEntry[3];
        entries[0] = new HashEntry("username", username);
        entries[1] = new HashEntry("password", password);
        entries[2] = new HashEntry("last_login_date", dateTime.ToString("yyyy-MM-dd"));

        database.HashSet(fieldName, entries);
        SetNewValue(username, 0);
    }

    public void SetNewValue(string username, int value)
    {
        if (database.HashExists("score-hash", username) == false) 
        {
            print($"Set default score (0) for {username}");

            database.HashSet("score-hash", username, value);
            return; 
        }
        int score = Int32.Parse(database.HashGet("score-hash", username).ToString());
        print(score);

        if (score > value)
        {
            print($"Current score for {username} is more than {value} ({score})");
            return;
        } else
        {
            print($"Set score for {username} to {value} from {score}");

            database.HashSet("score-hash", username, value);
            return;
        }
    }

    public string GetValue(string key) => database.HashGet("score-hash", key);

    public (Dictionary<string, int>, int) GetAllValuesFromFolder()
    {
        Dictionary<string, int> resultsDict = new ();
        int currentPlayerIndex = 0;

        HashEntry[] entries = database.HashGetAll("score-hash");
        int i = 0;
        foreach (HashEntry key in entries)
        {
            int value = Int32.Parse(key.Value.ToString());

            if (key.Name == PlayerPrefs.GetString("player_name"))
                currentPlayerIndex = i;

            resultsDict.Add(key.Name, value);

            i++;
        }

        return (resultsDict, currentPlayerIndex);
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