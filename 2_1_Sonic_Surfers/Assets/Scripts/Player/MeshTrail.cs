using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    [Header("Animation Parameters")]
    [SerializeField] private float _activeTime;
    [SerializeField] private float _meshRefreshRate = .1f;
    [Space(5)]
    
    [Header("Mesh Parameters")]
    [SerializeField] private GameObject _meshBody;
    [SerializeField] private Transform _positionSpawn;
    [Space(2)]
    
    [SerializeField] private Material _ghostMaterial;

    private List<GameObject> _ghosts = new();
    private SkinnedMeshRenderer[] _skinnedMeshRenderers;

    private float _startActiveTime;
    
    private void Awake() => _startActiveTime = _activeTime;
    public void Trail() => StartCoroutine(TrailCoroutine());

    private IEnumerator TrailCoroutine()
    {
        while (_activeTime > 0)
        {
            _activeTime -= Time.deltaTime;

            _skinnedMeshRenderers ??= _meshBody.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skinned in _skinnedMeshRenderers)
            {
                GameObject obj = new GameObject();
                obj.transform.SetPositionAndRotation(_positionSpawn.position, _positionSpawn.rotation);
                
                MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinned.BakeMesh(mesh);
                meshFilter.mesh = mesh;
                
                meshRenderer.material = _ghostMaterial;
                
                _ghosts.Add(obj);
            }
            
            yield return new WaitForSeconds(_meshRefreshRate);
        }

        ClearGhosts();
        Reset();
    }

    private void Reset() => _activeTime = _startActiveTime;
    
    private void ClearGhosts()
    {
        foreach (GameObject obj in _ghosts)
            Destroy(obj);

        _ghosts.Clear();
    }
}
