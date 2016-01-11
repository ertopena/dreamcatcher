using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PoolBuilder : MonoBehaviour {

	public int defaultInstancesPerPrefab = 25;

	private GameObject[] _prefabs;
	private List<GameObject[]> _prefabPools;
	private int[] _lastIndexFrom;


	public List<GameObject[]> BuildPools(GameObject[] prefabs)
	{
		_prefabs = prefabs;
		_prefabPools = new List<GameObject[]>();
		_lastIndexFrom = new int[_prefabs.Length];

		for (int i = 0; i < _prefabs.Length; i++)
			SeedPool(i);

		return _prefabPools;
	}


	public void SeedPool(int prefabIndex, int instancesPerPrefab)
	{
		Transform poolHolder = CreatePoolHolder(prefabIndex);
		GameObject[] prefabPool = new GameObject[instancesPerPrefab];

		for (int i = 0; i < prefabPool.Length; i++)
			prefabPool[i] = CreatePoolMember(prefabIndex, poolHolder);

		_prefabPools.Add(prefabPool);
	}
	public void SeedPool(int prefabIndex)
	{
		SeedPool(prefabIndex, defaultInstancesPerPrefab);
	}


	public GameObject GetNextInstanceFromPool(int poolIndex)
	{
		GameObject[] pool = _prefabPools[poolIndex];
		
		for (int i = 1; i <= pool.Length; i++)
		{
			int currentMember = (i + _lastIndexFrom[poolIndex]) % pool.Length;

			if (!(pool[currentMember].activeInHierarchy))
			{
				_lastIndexFrom[poolIndex] = currentMember;
				return pool[currentMember];
			}
		}

		Debug.Log("Reached the end of the pool without an inactive object! Fix this!");
		return pool[0];
	}


	GameObject CreatePoolMember(int prefabIndex, Transform parent)
	{
		GameObject poolMember = Instantiate<GameObject>(_prefabs[prefabIndex]);
		poolMember.transform.parent = parent;
		poolMember.SetActive(false);

		return poolMember;
	}


	Transform CreatePoolHolder(int prefabIndex)
	{
		GameObject poolHolder = new GameObject();
		poolHolder.transform.parent = this.transform;
		poolHolder.name = _prefabs[prefabIndex].name + " Pool";

		return poolHolder.transform;
	}
}
