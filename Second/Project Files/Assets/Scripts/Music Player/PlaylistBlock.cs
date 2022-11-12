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
    
    public void Init()
    {
        _titleText.text = Title;

        _amountText.text = $"Tracks: {Tracks.Count}";
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
