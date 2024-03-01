using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesGraphicsController : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _lifeObject;

    private List<Image> _lives;

    private void Start()
    {
        _lives = new ();

        int livesAmount = GameController.Instance.MaxNumberOfShots;
        for (int i = 0; i < livesAmount; i++)
        {
            Image image = Instantiate(_lifeObject, _parent.position, Quaternion.identity, _parent).GetComponent<Image>();
            _lives.Add(image);
        }
    }

    public void UseShot(int number)
    {
        for (int i = 0; i < _lives.Count; i++)
        {
            if (number == _lives.Count - i)
            {
                Color temp = _lives[i].color;
                temp.a = .2f;

                _lives[i].color = temp;
                return;
            }
        }
    }
}
