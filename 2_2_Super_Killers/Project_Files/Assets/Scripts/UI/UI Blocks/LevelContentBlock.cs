using UnityEngine;
using TMPro;

public class LevelContentBlock : MonoBehaviour
{
    public Level BlockLevel { get; set; }

    [Header("Block Setup")]
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private TMP_Text _wavesText;
    
    private void Start() => Setup();
    private void Setup() => _wavesText.text = "Waves: " + BlockLevel.WavesAmount;

    public void SetLevelName(int number)
    {
        _levelNumberText.text = "Level " + number;
        gameObject.name = "Level Block " + number;
    }
}
