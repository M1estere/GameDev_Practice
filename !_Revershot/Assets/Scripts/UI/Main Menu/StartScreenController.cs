using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) 
            OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        gameObject.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
