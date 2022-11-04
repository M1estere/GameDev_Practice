using System.IO;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [Header("Screenshot")] 
    [SerializeField] private RenderTexture _texture;
    [SerializeField] private Camera _screenshotCamera;
    [Space(7)]
    
    [SerializeField] private Camera _mainCamera;
    
    [SerializeField] private GameObject _line;
    
    private LineRenderer _currentLineRenderer;
    
    private Draw _lineScript;

    private int _currentLayer = 0;
    
    private void Awake()
    {
        Directory.CreateDirectory($"{Application.dataPath}/screenshots/");
        _line.GetComponent<LineRenderer>().sortingOrder = 0;
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.F12)) TakeScreenshot();
        
        if (Input.GetMouseButtonDown(0))
        {
            var newLine = Instantiate(_line);

            _currentLineRenderer = _line.GetComponent<LineRenderer>();
            _currentLineRenderer.sortingOrder = _currentLayer;
            _currentLayer++;
            
            _lineScript = newLine.GetComponent<Draw>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lineScript = null;
        }

        if (_lineScript != null)
        {
            var mousePosition = new Vector3();
            mousePosition.x = Input.mousePosition.x;
            mousePosition.y = Input.mousePosition.y;
            mousePosition.z = 0;

            var output = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
            _lineScript.UpdateLine(new Vector2(output.x, output.y));
        }
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
        
        // Debug.Log(string.Format("Took screenshot to: {0}", path));
        
        DestroyImmediate(tex);

        _screenshotCamera.targetTexture = _texture;
        _screenshotCamera.Render();
        RenderTexture.active = _texture;

        DestroyImmediate(mRt);
    }
    
    private static string ScreenShotName(int width, int height) {
        return
            $"{Application.dataPath}/screenshots/screen_{width}x{height}_{System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.png";
    }
}
