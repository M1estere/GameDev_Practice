using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuContentSetup : MonoBehaviour
{
    [Header("Gun Content Setup")]
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _block;
    [Space(2)]
    
    [SerializeField] private TMPro.TMP_Text _currentGunText;
    [Space(5)]
    
    [Header("Level Content Setup")]
    [SerializeField] private Transform _levelContent;
    [SerializeField] private GameObject _levelBlock;
    [Space(2)]
    
    [SerializeField] private string _levelName = "Main Scene";

    private void Awake()
    {
        ChooseGun(PlayerPrefs.GetInt("Gun")); 
    }
    
    private void Start() => Init();
    private void Init()
    {
        InitializeGunContent();
        InitializeLevelContent();
    }

    private void InitializeLevelContent()
    {
        Level[] levels = Resources.LoadAll("Levels", typeof(Level)).Cast<Level>().ToArray();

        for (int i = 0; i < levels.Length; i++)
        {
            int temp = i;
            
            LevelContentBlock block = Instantiate(_levelBlock, _levelContent.position, Quaternion.identity, _levelContent).GetComponent<LevelContentBlock>();
            block.BlockLevel = levels[i];
            block.SetLevelName(temp + 1);
            
            if (block.TryGetComponent(out Button button) == false) continue;
            
            button.onClick.AddListener(() => ChooseLevel(temp));
        }
    }

    private void ChooseLevel(int levelId)
    {
        PlayerPrefs.SetInt("Current Level", levelId);
        SceneManager.LoadScene(_levelName);
    }
    
    private void InitializeGunContent()
    {
        Gun[] guns = Resources.LoadAll("Guns", typeof(Gun)).Cast<Gun>().ToArray();

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
