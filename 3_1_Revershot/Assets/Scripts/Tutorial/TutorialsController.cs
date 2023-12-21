using System.Collections;
using UnityEngine;

public class TutorialsController : MonoBehaviour
{
    [SerializeField] private GameObject _wasdTutorial;
    [SerializeField] private GameObject _shootingTutorial;
    [SerializeField] private GameObject _enemiesTutorial;

    public void StartTutorial(int index)
    {
        switch (index)
        {
            case 1:
                StartCoroutine(ShowTutorial(_wasdTutorial));
                break;
            case 2:
                StartCoroutine(ShowTutorial(_shootingTutorial));
                break;
            case 3:
                StartCoroutine(ShowTutorial(_enemiesTutorial));
                break;
        }
    }

    private IEnumerator ShowTutorial(GameObject tutorial)
    {
        DisableAllTutorials();
        tutorial.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        tutorial.SetActive(false);
    }

    private void DisableAllTutorials()
    {
        _wasdTutorial.SetActive(false);
        _shootingTutorial.SetActive(false);
        _enemiesTutorial.SetActive(false);
    }
}
