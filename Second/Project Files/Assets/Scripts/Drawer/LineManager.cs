using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private UIManager _manager;
    [SerializeField, Tooltip("For looking for mouse")] private Camera _mainCamera;
    [Space(5)]
    
    [SerializeField] private GameObject _linePrefab;
    [Space(3)]
    
    [SerializeField] private Canvas _localUI;

    private GameObject _lastLine;
    
    private LineRenderer _currentLineRenderer;
    private Draw _lineScript;

    private int _currentLayer;

    private int _counter = 1;
    
    private void Awake()
    {
        _linePrefab.GetComponent<LineRenderer>().sortingOrder = 0;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) ToggleLocalUI();
        
        if (Input.GetMouseButtonDown(0))
        {
            var newLine = Instantiate(_linePrefab);
            newLine.name = $"Line {_counter}";
            
            _currentLineRenderer = _linePrefab.GetComponent<LineRenderer>();
            _currentLineRenderer.sortingOrder = _currentLayer;
            _currentLayer++;
            
            _lineScript = newLine.GetComponent<Draw>();
            if (_manager.IsEraser) _manager.AddEraserLine(newLine.GetComponent<LineRenderer>());
            _counter++;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lastLine = _lineScript.gameObject;
            
            _lineScript = null;
        }

        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            if (_lastLine != null)
            {
                Destroy(_lastLine);
                _counter--;
                
                _lastLine = FindNewLine();
            }
            else
            {
                Debug.Log("Nothing to destroy!");
            }
        }
        
        if (_lineScript != null)
        {
            var mousePosition = new Vector3
            {
                x = Input.mousePosition.x,
                y = Input.mousePosition.y,
                z = 0
            };

            var output = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));
            _lineScript.UpdateLine(new Vector2(output.x, output.y));
        }
    }

    private GameObject FindNewLine()
    {
        return GameObject.Find($"Line {_counter - 1}");
    }

    private void ToggleLocalUI()
    {
        _localUI.enabled = !_localUI.enabled;
    }
}