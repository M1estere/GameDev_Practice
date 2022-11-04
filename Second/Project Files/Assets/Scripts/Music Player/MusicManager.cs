using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

public class MusicManager : MonoBehaviour
{
    [HideInInspector] public bool MusicPaused;
    [HideInInspector] public AudioClip CurrentClip;

    [Header("Error Handling")]
    [SerializeField] private GameObject _warningObject;
    [SerializeField] private TMPro.TMP_Text _warningText;
    [Space(7)]
    
    [Header("Track List Control")]
    [SerializeField] private GameObject _contentParent;
    [SerializeField] private GameObject _contentObject;
    [Space(7)]
    
    [Header("Main Audio Control")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private List<AudioClip> _tracks = new List<AudioClip>();
    [Space(7)]

    private List<TrackBlock> _blocks = new List<TrackBlock>();
    private List<TrackBlock> _tempBlocks = new List<TrackBlock>();
    
    private int _currentTrackNumber = 0;

    private AudioClip _addableClip;
    private string _newClipPath;
    
    private void Awake()
    {
        SelectTrack(_currentTrackNumber);
    }

    private void Start()
    {
        CreateTrackList();
    }
    
    private void Update()
    {
        if (_source.isPlaying == false && MusicPaused == false)
        {
            NextTrack();
        }
    }

    private void CreateTrackList()
    {
        for (int i = 0; i < _tracks.Count; i++)
        {
            InitializeNewBlock(i, i);
        }
    }

    private void UpdateTracksNumbers()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].ID == _currentTrackNumber)
            {
                _blocks[i].ID = i;
                _currentTrackNumber = i;
            }
            else { _blocks[i].ID = i; }
        }
    }

    public void PauseMusic()
    {
        MusicPaused = true;
        _source.Pause();
    }

    public void UnpauseMusic()
    {
        MusicPaused = false;
        _source.UnPause();
    }

    public void MoveTrack(float newValue)
    {
        _source.Stop();
        _source.time = newValue;
        _source.Play();
        if (MusicPaused == true) _source.Pause();
    }
    
    public void NextTrack()
    {
        if (_currentTrackNumber == _tracks.Count - 1)
        {
            // Cycle
            _currentTrackNumber = 0;
            SelectTrack(_currentTrackNumber);
            return;
        }
        
        _currentTrackNumber++;
        SelectTrack(_currentTrackNumber);
    }
    
    public void PrevTrack()
    {
        if ((_source.time / CurrentClip.length) > .02f)  // lower than 5 pct
        {
            SelectTrack(_currentTrackNumber);
            return;
        }
        
        if (_currentTrackNumber == 0)
        {
            // Back to first
            _currentTrackNumber = 0;
            SelectTrack(_currentTrackNumber);
            return;
        }

        _currentTrackNumber--;
        SelectTrack(_currentTrackNumber);
    }

    public void SelectTrack(int songIndex)
    {
        _currentTrackNumber = songIndex;
        for (var i = 0; i < _tracks.Count; i++)
        {
            if (i != songIndex) continue;
            
            _source.time = 0;
            _source.clip = _tracks[i];
            CurrentClip = _tracks[i];
            _source.Play();
                
            if (MusicPaused) _source.Pause();
        }
    }

    public int DeleteTrack(int trackID)
    {
        if (trackID == _currentTrackNumber)
        {
            DisplayError("This track cannot be removed\n:(");
            return 0;
        }
        _tracks.RemoveAt(trackID);
        _blocks.RemoveAt(trackID);
        
        UpdateTracksNumbers();
        return 1;
    }

    public void DisplayError(string message)
    {
        _warningText.text = message;
        _warningObject.SetActive(true);
        
        Invoke(nameof(DisableWarning), 2);
    }

    private void DisableWarning()
    {
        _warningObject.SetActive(false);
    }
    
    public void AddToEnd(int ID)
    {
        _tracks.Add(_tracks[ID]);
        InitializeNewBlock(ID, 15600);
        
        UpdateTracksNumbers();
    }

    public void AddNext(int ID)
    {
        _tracks.Insert(_currentTrackNumber + 1, _tracks[ID]);
        
        var obj = Instantiate(_contentObject, _contentParent.transform.position, Quaternion.identity,
            _contentParent.transform);

        var block = obj.GetComponent<TrackBlock>();
        
        block.MusicM = this;
        block.Duration = _tracks[ID].length;
        block.Title = _tracks[ID].name;
        block.ID = 15600;

        block.Init();
        
        _blocks.Insert(_currentTrackNumber + 1, block);
        
        UpdateTracksNumbers();
        RedrawBlocks();
    }

    public IEnumerator LoadAudio(string path)
    {
        WWW request = GetAudioFromFile(path);
        yield return request;

        _addableClip = request.GetAudioClip(true, false);
        _addableClip.name = Path.GetFileNameWithoutExtension(path);
        AddCustomClip();

        _addableClip = null;
    }

    private void AddCustomClip()
    {
        _tracks.Add(_addableClip);
        
        InitializeNewBlock(_tracks.Count-1, 15600);
        
        UpdateTracksNumbers();
    }

    private WWW GetAudioFromFile(string path)
    {
        string audioToLoad = path;
        WWW request = new WWW(audioToLoad);
        
        return request;
    }

    private void RedrawBlocks()
    {
        for (int i = 0; i < _blocks.Count; i++)
            _tempBlocks.Add(_blocks[i]);

        for (int i = 0; i < _blocks.Count; i++)
            _blocks[i].DeleteBlock();
        _blocks.Clear();

        for (int i = 0; i < _tempBlocks.Count; i++)
        {
            InitializeNewBlock(i, i);
        }

        _tempBlocks.Clear();
    }
    
    private void InitializeNewBlock(int id, int blockID)
    {
        var obj = Instantiate(_contentObject, _contentParent.transform.position, Quaternion.identity,
            _contentParent.transform);

        var block = obj.GetComponent<TrackBlock>();
        
        block.MusicM = this;
        block.Duration = _tracks[id].length;
        block.Title = _tracks[id].name;
        block.ID = blockID;

        block.Init();
        
        _blocks.Add(block);
    }
}
