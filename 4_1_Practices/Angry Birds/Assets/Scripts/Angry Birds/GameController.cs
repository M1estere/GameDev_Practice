using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [field: SerializeField]
    public int MaxNumberOfShots { get; private set; } = 3;

    [SerializeField] private float _secondsBeforeCheck = 2;
    [SerializeField] private GameObject _restartScreenObject;

    private int _usedShots;
    private LivesGraphicsController _livesGraphicsController;
    private SlingShotController _slingshotController;

    private List<Pig> _pigs;

    private void Awake()
    {
        Instance = this;

        _livesGraphicsController = FindObjectOfType<LivesGraphicsController>();
        _slingshotController = FindObjectOfType<SlingShotController>();

        Pig[] pigs = FindObjectsOfType<Pig>();
        _pigs = new List<Pig>(pigs);
    }

    public void UseShot()
    {
        _usedShots++;

        _livesGraphicsController.UseShot(_usedShots);

        CheckLastShot();
    }

    public bool HasEnoughShots()
    {
        return _usedShots < MaxNumberOfShots;
    }

    public void CheckLastShot()
    {
        if (_usedShots == MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWait());
        }
    }

    private IEnumerator CheckAfterWait()
    {
        yield return new WaitForSeconds(_secondsBeforeCheck);

        if (_pigs.Count == 0)
        {
            Win();
        } else
        {
            Lose();
        }
    }

    public void DeletePig(Pig pig)
    {
        _pigs.Remove(pig);
        CheckAllDead();
    }

    private void CheckAllDead()
    {
        if (_pigs.Count == 0)
        {
            Win();
        }
    }

    private void Win()
    {
        _restartScreenObject.SetActive(true);
        _slingshotController.enabled = false;
    }

    private void Lose()
    {
        Restart();
    }

    public void Restart()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
