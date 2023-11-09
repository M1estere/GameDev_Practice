using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    
    public int CurrentRings { get; private set; }
    private UiController _uiController;

    private void Start()
    {
        Instance = this;

        _uiController =  FindObjectOfType<UiController>();
    }

    public void IncreaseRings(int amount)
    {
        CurrentRings += amount;
        _uiController.UpdateRingsText(CurrentRings);
        
        CheckRingsForBank();
    }

    private void CheckRingsForBank()
    {
        if (CurrentRings < 10) return;

        Bank.Instance.AddToBank();
        CurrentRings = 0;
        
        _uiController.UpdateBankRingsText(Bank.Instance.BankRings);
    }

    public void LoseRings()
    {
        CurrentRings = 0;
        _uiController.UpdateRingsText(CurrentRings);
    }
}
