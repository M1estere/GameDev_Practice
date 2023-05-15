using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Level Config", fileName = "Level Config")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private Wave[] _waves;

    public Wave[] Waves => _waves;
}
