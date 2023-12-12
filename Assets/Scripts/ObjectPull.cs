using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPull : MonoBehaviour
{
    [SerializeField] private Container _container;
    [SerializeField] private int _capacity;

    private List<SpawningObject> _pool = new List<SpawningObject>();

    protected void Init(SpawningObject[] spawningObjects)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, spawningObjects.Length);
            SpawningObject spawnedObject = Instantiate(spawningObjects[randomIndex],
                _container.transform);
            spawnedObject.gameObject.SetActive(false);
            _pool.Add(spawnedObject);
        }
    }

    protected bool TryGetObject(out SpawningObject result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
        return result != null;
    }
}