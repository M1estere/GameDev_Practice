using System.Collections.Generic;
using UnityEngine;

public class PlaylistBlock : MonoBehaviour
{
    [HideInInspector] public MusicManager MusicM;
    [HideInInspector] public string Title;
    [HideInInspector] public List<AudioClip> Tracks;

    [Header("Main Texts")]
    [SerializeField] private TMPro.TMP_Text _amountText;
    [SerializeField] private TMPro.TMP_Text _titleText;
    [SerializeField] private TMPro.TMP_Text _timeText;
    
    public void Init()
    {
        _titleText.text = Title;

        _amountText.text = $"Tracks: {Tracks.Count}";

        float time = FindWholeDuration();
        _timeText.text = $"Time: {(time / 60).ToString("0")}:{(time % 60).ToString("00")}";
    }

    private float FindWholeDuration()
    {
        float time = 0;
        foreach (AudioClip clip in Tracks)
        {
            time += clip.length;
        }

        return time;
    }
    
    public void ChoosePlaylist()
    {
        MusicM.ChangePlaylist(Tracks);
    }

    public void DeletePlaylist()
    {
        Destroy(gameObject);
    }
}
