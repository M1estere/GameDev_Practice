using UnityEngine;

public class Burger : MonoBehaviour
{
    [SerializeField] private GameObject[] _components;

    private enum Components
    {
        Cheese, Ham, Mayonnaise, Ketchup, Lettuce, BeefCutlet, ChickenCutlet, Onion
    }

    public void AddCheese() => _components[(int)Components.Cheese].SetActive(true);
    public void AddHam() => _components[(int)Components.Ham].SetActive(true);

    public void AddOnion() => _components[(int)Components.Onion].SetActive(true);
    public void AddLettuce() => _components[(int)Components.Lettuce].SetActive(true);

    public void AddKetchup() => _components[(int)Components.Ketchup].SetActive(true);
    public void AddMayonnaise() => _components[(int)Components.Mayonnaise].SetActive(true);

    public void AddChickenCutlet() => _components[(int)Components.ChickenCutlet].SetActive(true);
    public void AddBeefCutlet() => _components[(int)Components.BeefCutlet].SetActive(true);
}
