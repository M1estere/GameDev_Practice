using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; private set; }
    public int BankRings { get; private set; }
    
    private void Awake() => Instance = this;
    
    public void AddToBank() => BankRings += 100;
    public void RecordRings() => PlayerPrefs.SetInt("BankRings", PlayerPrefs.GetInt("BankRings") + BankRings);
}
