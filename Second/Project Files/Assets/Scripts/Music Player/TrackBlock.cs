using UnityEngine;

public class TrackBlock : MonoBehaviour
{
    [HideInInspector] public MusicManager MusicM;
    [HideInInspector] public string Title;
    [HideInInspector] public float Duration;
    [HideInInspector] public int ID;

    [Header("Main Texts")]
    [SerializeField] private TMPro.TMP_Text _durationText;
    [SerializeField] private TMPro.TMP_Text _titleText;
    [Space(7)]
    
    [SerializeField] private OptionsBlock _menu;
    
    private Transform _menuParent;

    public void Init()
    {
        _menuParent = GameObject.FindWithTag("Test").transform;
        
        _titleText.text = Title;

        float leftPart = Duration / 60;
        
        float rightPart = Duration % 60;
        _durationText.text = leftPart.ToString("0") + ":" + rightPart.ToString("00");
    }

    public void OpenActionsMenu()
    {
        _menu.gameObject.SetActive(true);

        _menu.ThisBlock = this;
        
        _menu.transform.parent = _menuParent;
    }

    public void RemoveThisBlock()
    {
        if (MusicM.DeleteTrack(ID) != 1)
        {
            _menu.Close();
            return;
        }

        _menu.transform.parent = transform;
        Destroy(gameObject);
    }

    public void DeleteBlock()
    {
        _menu.transform.parent = transform;
        Destroy(gameObject);
    }
    
    public void SetLastInQueue()
    {
        MusicM.AddToEnd(ID);
        
        _menu.gameObject.SetActive(false);
    }

    public void SetNextInQueue()
    {
        MusicM.AddNext(ID);
        
        _menu.gameObject.SetActive(false);
    }
    
    public void ChoseThisSong()
    {
        MusicM.SelectTrack(ID);
    }
}
