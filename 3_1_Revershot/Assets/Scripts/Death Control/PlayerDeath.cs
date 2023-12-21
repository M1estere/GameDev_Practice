using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;

    private PlayerController _playerController;

    private float _timeDecrement = .35f;

    private void Awake() =>
        _playerController = GetComponent<PlayerController>();

    public void InitiateDeath()
    {
        _playerController.enabled = false;

        StartCoroutine(TimeChangingCor());

        _gameOverScreen.SetActive(true);
    }

    private IEnumerator TimeChangingCor()
    {
        while (Time.timeScale > _timeDecrement)
        {
            Time.timeScale -= _timeDecrement;
            yield return null;
        }
    }
}
