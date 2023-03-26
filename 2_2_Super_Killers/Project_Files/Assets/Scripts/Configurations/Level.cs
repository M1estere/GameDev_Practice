using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order=2)]
public class Level : ScriptableObject
{
    public int WavesAmount;

    [Tooltip("Amount of enemies for each wave")] public int[] EnemiesPerWave;
    [Tooltip("Time given for each wave (sec)")] public float[] WavesTime;
}
