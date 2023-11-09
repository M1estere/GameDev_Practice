using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetupPlayerLeaderBlock : MonoBehaviour
{
    [SerializeField] private TMP_Text _idText;
    [Space(5)]

    [SerializeField] private Image _playerImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _maxScoreText;
    [Space(5)]

    [SerializeField] private Sprite[] _images;

    public void SetData(int id, string name, int score)
    {
        _idText.SetText(id.ToString());

        _playerImage.sprite = _images[Random.Range(0, _images.Length)];
        _nameText.SetText("<b>Player</b>: " + name.ToString());
        _maxScoreText.SetText("<b>Max Score</b>: " + score.ToString());
    }
}
