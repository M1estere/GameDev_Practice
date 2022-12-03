using UnityEngine;

public class LeverUI : MonoBehaviour
{
    [SerializeField] private Lever _lever;
    
    public void ButtonClick() => _lever.Activate();
}
