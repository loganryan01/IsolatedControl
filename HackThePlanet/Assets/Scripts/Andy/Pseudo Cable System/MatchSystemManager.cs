using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MatchSystemManager : MonoBehaviour
{
    public List<Material> _colorMaterials;
    private List<MatchEntity> _matchEntities;
    private int _targetMatchCount;
    private int _currentMatchCount = 0;

    void Start()
    {
        _matchEntities = transform.GetComponentsInChildren<MatchEntity>().ToList();
        _targetMatchCount = _matchEntities.Count;
        SetEntityColors();
        RandomizeMovablePairPlacement();
    }

    void SetEntityColors()
    {
        Shuffle(_colorMaterials);

        for (int i = 0; i < _matchEntities.Count; i++)
        {
            _matchEntities[i].SetMaterialToPairs(_colorMaterials[i]);
        }
    }

    void RandomizeMovablePairPlacement()
    {
        List<Vector3> movablePairPositions = new List<Vector3>();

        for (int i = 0; i < _matchEntities.Count; i++)
        {
            movablePairPositions.Add(item: _matchEntities[i].GetMovablePairPosition());
        }

        Shuffle(movablePairPositions);

        for (int i = 0; i < _matchEntities.Count; i++)
        {
            _matchEntities[i].SetMovablePairPosition(movablePairPositions[i]);
        }
    }

    public void NewMatchRecord(bool MatchConnected)
    {
        if (MatchConnected)
        {
            _currentMatchCount++;
        }

        else
        {
            _currentMatchCount--;
        }

        Debug.Log(message: "Currently, there are " + _currentMatchCount + " matches");

        if (_currentMatchCount == _targetMatchCount)
        {
            Debug.Log(message: "Well Done, All Paired");
        }
    }

    public static void Shuffle<T>(IList<T> List)
    {
        int n = List.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = List[k];
            List[k] = List[n];
            List[n] = value;
        }
    }
}
