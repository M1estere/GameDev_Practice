using UnityEngine;

public class QueueParamsBlock : MonoBehaviour
{
    [SerializeField] private MusicManager _musicM;
    
    public void CreateNewPlaylist()
    {
        _musicM.CreateNewPlaylist();

        _musicM.DisplayError("Playlist Saved!\n:)");
    }
}
