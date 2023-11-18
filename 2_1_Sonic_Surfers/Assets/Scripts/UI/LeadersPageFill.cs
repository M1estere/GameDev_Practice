using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LeadersPageFill : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private GameObject _playerScoreBlock;

    private void Start() => GetBlocks();

    public void GetBlocks()
    {
        int idCounter = 1;

        (Dictionary<string, int>, int) playersDataTuple = RedisController.RedisControllerInstance.GetAllValuesFromFolder();

        Dictionary<string, int> playersScores = playersDataTuple.Item1;
        int currentPlayerIndex = playersDataTuple.Item2;

        playersScores = playersScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        int i = 0;
        foreach (var keyValuePair in playersScores)
        {
            SetupPlayerLeaderBlock block = Instantiate(_playerScoreBlock, _contentTransform.position, Quaternion.identity, _contentTransform).GetComponent<SetupPlayerLeaderBlock>();
            block.SetData(idCounter, keyValuePair.Key, keyValuePair.Value, i == currentPlayerIndex);

            idCounter++;
            i++;
        }
    }
}
