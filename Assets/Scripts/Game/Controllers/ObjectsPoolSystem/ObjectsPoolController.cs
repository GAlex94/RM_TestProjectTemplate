using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace testProjectTemplate
{
    [Serializable]
    class WaitPoolObject
    {
        public GameObject prefab;
        public GameObject lifeObject;
        public IPoolObject poolObject;
        public bool isUsed;
        public PoolType poolType;
    }

    public enum PoolType
    {
       UnitsOne,
       UnitsTwo,
    }

    [Serializable]
    public class PoolTypeObjects
    {
        public PoolType poolType;
        public int countPregenerateByPrefab;
        public GameObject[] prefabsForPool;
    }

    public class ObjectsPoolController : MonoBehaviour
    {
        [SerializeField] private PoolTypeObjects[] poolTypes;

        [SerializeField] private bool disabledRealInstantiate;

        List<WaitPoolObject> poolObjects = new List<WaitPoolObject>();
        private int[] processIndicies = new int[100];

        
        bool isInit = false;

        public void Init()
        {
            if (isInit)
                return;
            isInit = true;


            bool isDisableInstantiate = disabledRealInstantiate;

            disabledRealInstantiate = false;
            foreach (var curPoolType in poolTypes)
            {
                foreach (var curPrefab in curPoolType.prefabsForPool)
                {
                    for (int i = 0; i < curPoolType.countPregenerateByPrefab; i++)
                    {
                        InstantiateFromPool(curPrefab, Vector3.zero, Quaternion.identity, curPoolType.poolType);
                    }
                }
            }

            foreach (var curPoolObject in poolObjects)
            {
                DestroyFromPool(curPoolObject.lifeObject);
            }

            disabledRealInstantiate = isDisableInstantiate;
        }

        public void AddObjectToPoolByType(GameObject prefab, PoolType poolType)
        {
            PoolTypeObjects objects = poolTypes.FirstOrDefault(x => x.poolType == poolType);

            if (objects == null)
            {
                return;
            }

            GameObject[] oldPoolObjects = objects.prefabsForPool;

            objects.prefabsForPool = new GameObject[oldPoolObjects.Length + 1];
            for (int i = 0; i < objects.prefabsForPool.Length - 1; i++)
            {
                objects.prefabsForPool[i] = oldPoolObjects[i];
            }

            objects.prefabsForPool[objects.prefabsForPool.Length - 1] = prefab;

            bool isDisableInstantiate = disabledRealInstantiate;
            disabledRealInstantiate = false;

            for (int i = 0; i < objects.countPregenerateByPrefab; i++)
            {
                InstantiateFromPool(prefab, Vector3.zero, Quaternion.identity, objects.poolType);
            }

            foreach (var curPoolObject in poolObjects)
            {
                if (curPoolObject.prefab == prefab)
                    DestroyFromPool(curPoolObject.lifeObject);
            }

            disabledRealInstantiate = isDisableInstantiate;
        }

        public GameObject InstantiateFromPool(GameObject prefab, Vector3 position, Quaternion rotation, PoolType poolType)
        {
            Init();

            int foundIndex = poolObjects.FindIndex(o => o.prefab == prefab && !o.isUsed);

            if (foundIndex == -1 && disabledRealInstantiate)
            {
                foundIndex = FindFreePoolObjectIndex(poolType);
            }

            if (foundIndex == -1)
            {
                if (disabledRealInstantiate)
                {
                    return null;
                }

                WaitPoolObject newPoolObject = InstantiateNew(prefab, poolType);
                if (newPoolObject != null)
                {
                    poolObjects.Add(newPoolObject);
                    newPoolObject.isUsed = true;
                    newPoolObject.lifeObject.transform.position = position;
                    newPoolObject.lifeObject.transform.rotation = rotation;
                    newPoolObject.lifeObject.SetActive(true);
                    newPoolObject.poolObject.Activate(newPoolObject.prefab);
                    return newPoolObject.lifeObject;
                }
                else
                {
                    GameObject newObject = Instantiate(prefab);
                    newObject.transform.position = position;
                    newObject.transform.rotation = rotation;
                    return newObject;
                }
            }
            else
            {
                WaitPoolObject newPoolObject = poolObjects[foundIndex];
                newPoolObject.isUsed = true;
                newPoolObject.lifeObject.transform.position = position;
                newPoolObject.lifeObject.transform.rotation = rotation;
                newPoolObject.lifeObject.SetActive(true);
                newPoolObject.poolObject.Activate(newPoolObject.prefab);
                return newPoolObject.lifeObject;
            }
        }

        public void DestroyFromPool(GameObject currentObject)
        {
            int foundIndex = poolObjects.FindIndex(o => o.lifeObject == currentObject);
            if (foundIndex != -1)
            {
                poolObjects[foundIndex].poolObject.Deactivate();
                poolObjects[foundIndex].lifeObject.SetActive(false);
                poolObjects[foundIndex].isUsed = false;
            }
            else
            {
                Destroy(currentObject);
            }
        }

        WaitPoolObject InstantiateNew(GameObject prefab, PoolType poolType)
        {
            if (prefab.GetComponent<IPoolObject>() != null)
            {
                GameObject newGameObject = Instantiate(prefab);
                newGameObject.transform.SetParent(this.transform, true);
                IPoolObject poolObject = newGameObject.GetComponent<IPoolObject>();
                poolObject.IsPooledObject = true;

                WaitPoolObject newObj = new WaitPoolObject()
                {
                    isUsed = true,
                    lifeObject = newGameObject,
                    poolObject = poolObject,
                    prefab = prefab,
                    poolType = poolType
                };

                return newObj;
            }

            return null;
        }


        int FindFreePoolObjectIndex(PoolType type)
        {
            int countFree = 0;
            for (int i = 0; i < poolObjects.Count; i++)
            {
                if (poolObjects[i].poolType == type && !poolObjects[i].isUsed)
                    processIndicies[countFree++] = i;
            }

            if (countFree == 0)
                return -1;

            int randomIndex = UnityEngine.Random.Range(0, countFree);
            return processIndicies[randomIndex];
        }
    }
}
