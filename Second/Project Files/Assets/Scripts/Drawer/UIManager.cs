using System.Collections.Generic;
using Directory = System.IO.Directory;
using File = System.IO.File;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public bool IsEraser = false;
    
    [SerializeField, Tooltip("To control local UI")] private LocalUI _localUI;
    
    [Header("Screenshot")] 
    [SerializeField] private Camera _screenshotCamera;
    [SerializeField] private RenderTexture _texture;
    [Space(5)] 
    
    [Header("Gallery")] 
    [SerializeField] private GameObject _contentObject;
    [SerializeField] private GameObject _contentParent;
    [Space(5)]
    
    [Header("For displaying warnings")]
    [SerializeField] private GameObject _warningObject;
    [SerializeField] private TMP_Text _warningText;
    [Space(5)]
    
    [Header("Brush for changing colour")]
    [SerializeField] private LineRenderer _linePrefab;
    [Space(5)] 
    
    [Header("Camera to change its BG colour")]
    [SerializeField] private Camera _camera;
    [Space(5)]
    
    [SerializeField] private Slider _widthSlider;
    [Space(5)]
    
    [SerializeField] private TMP_InputField _pathInputField;

    private List<LineRenderer> _eraserLines = new List<LineRenderer>();
    
    private List<string> _paths = new List<string>();

    private string _currentDirectory;
    
    private void Awake()
    {
        _currentDirectory = $"{Application.dataPath}/screenshots/";
        Directory.CreateDirectory(_currentDirectory);
        
        UpdateGallery();
    }
    
    private void Start()
    {
        float width = _linePrefab.startWidth;
        
        _linePrefab.startColor = Color.black;
        _linePrefab.endColor = Color.black;

        _widthSlider.minValue = 1;
        _widthSlider.maxValue = 5;
        
        _widthSlider.value = width * 2;
    }

    public void AddEraserLine(LineRenderer _renderer)
    {
        _eraserLines.Add(_renderer);
    }
    
    public void ChangeBrushColor(Image image)
    {
        IsEraser = false;
        
        _linePrefab.startColor = image.color;
        _linePrefab.endColor = image.color;
        
        _localUI.UpdateBrushColour(image.color);
        
        _localUI.ChangeUnderState(true);
    }

    public void ChangeBrushToEraser()
    {
        IsEraser = true;
        Color colour = _screenshotCamera.backgroundColor;
        
        _linePrefab.startColor = colour;
        _linePrefab.endColor = colour;
        
        _localUI.UpdateBrushColour(Color.clear);
        
        _localUI.ChangeUnderState(false);
    }
    
    public void ChangeBgColor(Image image)
    {
        _camera.backgroundColor = image.color;
        _screenshotCamera.backgroundColor = image.color;

        UpdateEraseColours(image.color);
        _localUI.UpdateBgColour(image.color);
    }

    private void UpdateEraseColours(Color colour)
    {
        foreach (var t in _eraserLines)
        {
            t.startColor = colour;
            t.endColor = colour;
        }

        if (IsEraser)
        {
            _linePrefab.startColor = colour;
            _linePrefab.endColor = colour;
        }
    }
    
    public void ChangeBrushWidth(float width)
    {
        width /= 2;
        
        _linePrefab.startWidth = width;
        _linePrefab.endWidth = width;
        
        _localUI.UpdateText(width * 2);
    }

    public void GetDirectoryPath(string path)
    {
        _pathInputField.text = "";

        if (Directory.Exists(path) == false)
        {
            DisplayError("No such directory found\n:(", TextAlignmentOptions.Center);
            return;
        }

        _currentDirectory = path;
        UpdateGallery();
    }

    private void UpdateGallery()
    {
        DirectoryInfo d = new DirectoryInfo(_currentDirectory);

        foreach (var file in d.GetFiles("screen*.png"))
        {
            string path = $"{_currentDirectory}\\{file.Name}";
            if (_paths.Contains(path) == false)
            {
                AddImageToGallery(LoadSprite(path));
                _paths.Add(path);
            }
        }
    }

    private void AddImageToGallery(Sprite spriteToPlace)
    {
        var obj = Instantiate(_contentObject, _contentParent.transform.position, Quaternion.identity,
            _contentParent.transform);

        obj.GetComponent<Image>().sprite = spriteToPlace;
    }
    
    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (File.Exists(path) == false) return null;
        
        byte[] bytes = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        
        return sprite;
    }
    
    private void DisplayError(string message, TextAlignmentOptions option)
    {
        _warningText.alignment = option;
        _warningText.text = message;
        _warningObject.SetActive(true);
        
        Invoke(nameof(DisableWarning), 5);
    }

    private void DisableWarning()
    {
        _warningObject.SetActive(false);
    }
    
    public void TakeScreenshot()
    {
        RenderTexture mRt = new RenderTexture(_texture.width, _texture.height, _texture.depth, RenderTextureFormat.ARGB32,
            RenderTextureReadWrite.sRGB);
        mRt.antiAliasing = _texture.antiAliasing;

        var tex = new Texture2D(mRt.width, mRt.height, TextureFormat.ARGB32, false);
        _screenshotCamera.targetTexture = mRt;
        _screenshotCamera.Render();
        RenderTexture.active = mRt;

        tex.ReadPixels(new Rect(0, 0, mRt.width, mRt.height), 0, 0);
        tex.Apply();

        var path = ScreenShotName(mRt.width, mRt.height);
        File.WriteAllBytes(path, tex.EncodeToPNG());
        
        DisplayError($"Screenshot saved at: {path}", TextAlignmentOptions.Left);
        
        DestroyImmediate(tex);

        _screenshotCamera.targetTexture = _texture;
        _screenshotCamera.Render();
        RenderTexture.active = _texture;

        DestroyImmediate(mRt);
        
        UpdateGallery();
    }
    
    private string ScreenShotName(int width, int height)
    {
        string rightPart = $"screen_{width}x{height}_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        
        return _currentDirectory + "/" + rightPart;
    }
}
