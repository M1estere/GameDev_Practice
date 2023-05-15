using UnityEngine.UI;
using UnityEngine;

public class LocalUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private TMPro.TMP_Text _widthText;
    [Space(3)]
    
    [SerializeField] private Image _colorImage;
    [SerializeField] private Image _underColor;
    
    public void UpdateText(float currentWidth)
    {
        _widthText.text = ((int)currentWidth).ToString();
    }

    public void UpdateBrushColour(Color brushColor)
    {
        _colorImage.color = brushColor;
    }

    public void ChangeUnderState(bool state)
    {
        _underColor.gameObject.SetActive(state);
    }
    
    public void UpdateBgColour(Color bgColor)
    {
        if (bgColor == Color.black)
        {
            _widthText.color = Color.white;
            _underColor.color = Color.white;
        }
        else
        {
            _widthText.color = Color.black;
            _underColor.color = Color.black;
        }
    }
}
