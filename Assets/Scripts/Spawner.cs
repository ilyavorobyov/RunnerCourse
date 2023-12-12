using System.Collections;
using UnityEngine;

public class Spawner : ObjectPull
{
    [SerializeField] private SpawningObject[] _objectTemplates;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _timeBetweenSpawns;

    private Coroutine _addObjects;

    private void Start()
    {
        Init(_objectTemplates);
        _addObjects = StartCoroutine(CreateResource());
    }

    private void OnEnable()
    {
        Player.Died += OnStopAddObjects;
    }

    private void OnDisable()
    {
        Player.Died -= OnStopAddObjects;
    }

    private void OnDestroy()
    {
        OnStopAddObjects();
    }

    private void OnStopAddObjects()
    {
        if (_addObjects != null)
            StopCoroutine(_addObjects);
    }

    private void SetObject(SpawningObject spawningObject, Vector3 spawnpoint)
    {
        spawningObject.gameObject.SetActive(true);
        spawningObject.transform.position = spawnpoint;
    }

    private IEnumerator CreateResource()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenSpawns);
        bool isCreating = true;

        while (isCreating)
        {
            if (TryGetObject(out SpawningObject spawningObject))
            {
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                SetObject(spawningObject, _spawnPoints[spawnPointNumber].position);
            }

            yield return waitForSeconds;
        }
    }
}