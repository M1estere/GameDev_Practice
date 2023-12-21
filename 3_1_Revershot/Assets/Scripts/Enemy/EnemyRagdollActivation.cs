using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyRagdollActivation : MonoBehaviour
{
    private List<Collider> _ragdollColliders = new ();
    private List<Rigidbody> _ragdollRigidbodies = new ();

    private void Awake()
    {
        _ragdollColliders = GetComponentsInChildren<Collider>().ToList();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>().ToList();

        DeactivateRagdoll();

        _ragdollColliders[0].enabled = true;
        _ragdollRigidbodies[0].isKinematic = false;
    }

    public void ActivateRagdoll()
    {
        _ragdollColliders[0].enabled = false;
        _ragdollRigidbodies[0].isKinematic = true;

        foreach (Collider collider in _ragdollColliders)
            collider.enabled = true;

        foreach (Rigidbody rigidbody in _ragdollRigidbodies)
            rigidbody.isKinematic = false;

        Invoke(nameof(DeactivateRagdoll), 3);
    }

    private void DeactivateRagdoll()
    {
        foreach (Collider collider in _ragdollColliders)
            collider.enabled = false;

        foreach (Rigidbody rigidbody in _ragdollRigidbodies)
            rigidbody.isKinematic = true;
    }
}
