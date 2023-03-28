using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    [Serializable]
    public struct Wave
    {
        public int EnemiesOnWave => _enemiesOnWave;
        public float WaveTime => _waveTime;

        [SerializeField, Tooltip("Amount of enemies for each wave")] private int _enemiesOnWave;
        [SerializeField, Tooltip("Time given for each wave (sec)")] private float _waveTime;
    }

    public ReadOnlyCollection<Wave> Waves => _waves.AsReadOnly();
    public int WavesAmount => _waves.Count;
    
    [SerializeField] private List<Wave> _waves;
}
