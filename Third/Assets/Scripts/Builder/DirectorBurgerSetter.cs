using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DirectorBurgerSetter : MonoBehaviour
{
    [SerializeField] private Toggle[] _componentToggles;
    [Space(5)] 
    
    [SerializeField] private GameObject _result;
    [Space(3)] 
    
    [SerializeField] private Button _applyButton;
    
    private Burger _burger;

    private void Awake()
    {
        _burger = GetComponent<Burger>();
    }

    public void ApplyOrder()
    {
        CreateResult();
        SetSceneFinish();
    }

    public void OrderAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    private void CreateResult()
    {
        IBuilderBurger burgerBuilder = new BurgerBuilder();
        burgerBuilder.MyBurger = _burger;
        
        CheckToggles(burgerBuilder);
    }
    
    private void SetSceneFinish()
    {
        _applyButton.interactable = false;
        foreach (Toggle toggle in _componentToggles) toggle.interactable = false;
        
        _result.SetActive(true);
    }
    
    private void CheckToggles(IBuilderBurger builder)
    {
        if (_componentToggles[0].isOn == true) builder.AddLettuce();
        if (_componentToggles[1].isOn == true) builder.AddOnion();
        if (_componentToggles[2].isOn == true) builder.AddKetchup();
        if (_componentToggles[3].isOn == true) builder.AddMayonnaise();
        if (_componentToggles[4].isOn == true) builder.AddBeefCutlet();
        if (_componentToggles[5].isOn == true) builder.AddChickenCutlet();
        if (_componentToggles[6].isOn == true) builder.AddCheese();
        if (_componentToggles[7].isOn == true) builder.AddHam();
    }
}
