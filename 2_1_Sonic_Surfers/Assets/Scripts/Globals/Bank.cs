using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; private set; }
    public int BankRings { get; private set; }
    
    private void Awake() => Instance = this;
    
    public void AddToBank() => BankRings += 10;
    public void RecordRings() 
    {
        RedisController.RedisControllerInstance.SetNewValue(PlayerPrefs.GetString("player_name"), BankRings);
        PlayerPrefs.SetInt("BankRings", PlayerPrefs.GetInt("BankRings") + BankRings);
    } 
}
