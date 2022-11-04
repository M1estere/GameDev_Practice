using UnityEngine;

public class OptionsBlock : MonoBehaviour
{
    [HideInInspector] public TrackBlock ThisBlock;

    public void QueueLast()
    {
        ThisBlock.SetLastInQueue();
    }

    public void QueueNext()
    {
        ThisBlock.SetNextInQueue();
    }

    public void Remove()
    {
        ThisBlock.RemoveThisBlock();
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
