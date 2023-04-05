using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _ringsText;
    [SerializeField] private TMPro.TMP_Text _bankRingsText;
    
    public void UpdateRingsText(int rings) => _ringsText.SetText($"{rings:000}");
    public void UpdateBankRingsText(int amount) => _bankRingsText.SetText($"{amount:00000}");
}
