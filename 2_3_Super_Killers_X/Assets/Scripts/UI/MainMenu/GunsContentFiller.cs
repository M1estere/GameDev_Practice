using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunsContentFiller : MonoBehaviour
{
    [Header("Gun Content Setup")]
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _block;
    [Space(2)]
    
    [SerializeField] private TMPro.TMP_Text _currentGunText;

    private void Awake()
    {
        InitializeGunContent();
        ChooseGun(PlayerPrefs.GetInt("Gun"));
    }

    private void InitializeGunContent()
    {
        GunConfig[] guns = Resources.LoadAll("Guns", typeof(GunConfig)).Cast<GunConfig>().ToArray();

        for (int i = 0; i < guns.Length; i++)
        {
            int temp = i;
            
            GunContentBlock block = Instantiate(_block, _content.position, Quaternion.identity, _content).GetComponent<GunContentBlock>();
            block.BlockGun = guns[i];
            
            if (block.TryGetComponent(out Button button) == false) continue;
            
            button.onClick.AddListener(() => ChooseGun(temp));
        }
    }
    
    private void ChooseGun(int number)
    {
        PlayerPrefs.SetInt("Gun", number);
        UpdateCurrentGunUI(number);
    }

    private void UpdateCurrentGunUI(int number)
    {
        switch (number)
        {
            case 1:
                _currentGunText.SetText("Current: <b>Fastel</b>");
                break;
            case 2:
                _currentGunText.SetText("Current: <b>Glock</b>");
                break;
            case 3:
                _currentGunText.SetText("Current: <b>Minigun</b>");
                break;
            case 4:
                _currentGunText.SetText("Current: <b>Rev</b>");
                break;
            default:
                _currentGunText.SetText("Current: <b>Beretta</b>");
                break;
        }
    }
}
