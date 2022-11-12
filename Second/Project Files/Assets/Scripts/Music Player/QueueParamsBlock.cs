using UnityEngine;

public class QueueParamsBlock : MonoBehaviour
{
    [SerializeField] private MusicManager _musicM;
    
    public void CreateNewPlaylist()
    {
        _musicM.CreateNewPlaylist();
    }
}
