using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _idleCamera;
    [SerializeField] private CinemachineVirtualCamera _followCamera;

    private void Awake()
    {
        SwitchToIdle();
    }

    public void SwitchToIdle()
    {
        _idleCamera.enabled = true;
        _followCamera.enabled = false;
    }

    public void SwitchToFollow(Transform follow)
    {
        _followCamera.Follow = follow;

        _idleCamera.enabled = false;
        _followCamera.enabled = true;
    }
}
