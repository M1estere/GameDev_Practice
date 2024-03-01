using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _hook;
    [SerializeField] private GameObject _ropePrefab;
    [SerializeField] private Weight _weight;
    [SerializeField] private int _linksAmount;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D prevRigidbody2D = _hook;

        for (int i = 0; i < _linksAmount; i++)
        {
            GameObject link = Instantiate(_ropePrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = prevRigidbody2D;

            if (i < _linksAmount - 1)
            {
                prevRigidbody2D = link.GetComponent<Rigidbody2D>();
            }
            else
            {
                _weight.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
            }
        }
    }
}
