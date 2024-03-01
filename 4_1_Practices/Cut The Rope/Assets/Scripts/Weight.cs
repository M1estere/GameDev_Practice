using UnityEngine;

public class Weight : MonoBehaviour 
{
    [SerializeField] private float _distanceFromChainEnd = .6f;

    public void ConnectRopeEnd(Rigidbody2D rigidbody)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = rigidbody;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0, -_distanceFromChainEnd);
    }
}
