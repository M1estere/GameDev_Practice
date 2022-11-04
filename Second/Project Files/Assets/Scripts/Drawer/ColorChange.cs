using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private LineRenderer _linePrefab;
    [Space(7)] 
    
    [SerializeField] private Camera _camera;
    [Space(3)] 
    [SerializeField] private Camera _screenshotCamera;
    [Space(7)]
    
    [SerializeField] private Slider _widthSlider;

    private void Start()
    {
        _widthSlider.value = _linePrefab.startWidth;
    }
    
    public void ChangeBrushColor(Image image)
    {
        _linePrefab.startColor = image.color;
        _linePrefab.endColor = image.color;
    }

    public void ChangeBGColor(Image image)
    {
        _camera.backgroundColor = image.color;
        _screenshotCamera.backgroundColor = image.color;
    }

    public void ChangeBrushWidth(float width)
    {
        _linePrefab.startWidth = width;
        _linePrefab.endWidth = width;
    }
}
