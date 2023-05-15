using UnityEngine;

public class StartCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;
    
    private void Awake() => CheckCharacter();

    private void CheckCharacter()
    {
        int id = PlayerPrefs.GetInt("Character");

        foreach (GameObject character in _characters)
            character.SetActive(false);
        
        _characters[id].SetActive(true);
    }
}
