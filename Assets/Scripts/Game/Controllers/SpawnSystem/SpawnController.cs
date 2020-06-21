using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace testProjectTemplate
{
    public class SpawnController : MonoBehaviour
    {
        private List<Bounds> usedPosition;

        public void Init()
        {
            if (usedPosition == null)
            {
                usedPosition = new List<Bounds>();
            }
        }

        private Vector3 GetPositionSpawn()
        {
            var bounds = BattleGame.Instance.GameAreaController.AreaRenderer;
            var min = bounds.bounds.min;
            var max = bounds.bounds.max;
            var sizeUnit = GameManager.Instance.MainGameSetting.unitSpawnMaxRadius;
            min.x += sizeUnit / 2;
            min.y += sizeUnit / 2;
            max.x -= sizeUnit / 2;
            max.y -= sizeUnit / 2;

            int iteration = 10000;
            bool canSpawn = false;
            var newPos = new Vector3(0, 0, 0f);
            while (!canSpawn && iteration > 0)
            {
                iteration--;
                var posX = UnityEngine.Random.Range(min.x, max.x);
                var posY = UnityEngine.Random.Range(min.y, max.y);
                newPos = new Vector3(posX, posY, 0f);
                canSpawn = true;
                foreach (var curBounds in usedPosition)
                {
                    if ((newPos.x >= curBounds.min.x && newPos.x <= curBounds.max.x) && (newPos.y >= curBounds.min.y && newPos.y <= curBounds.max.y))
                    {
                        canSpawn = false;
                    }
                }

            }
            var newBounds = new Bounds(newPos, new Vector3(sizeUnit * 2, sizeUnit * 2, 0));
            usedPosition.Add(newBounds);
            return newPos;
        }

        public void SpawnUnits(int countUnit, float delaySpawn, UnitInfo unitOne, UnitInfo unitTwo, Action<BasicUnit, UnitTypeEnum> spawnUnitAction, Action endSpawnAction = null)
        {
            StartCoroutine(Spawn(countUnit, delaySpawn, unitOne, unitTwo, spawnUnitAction, endSpawnAction));
        }

        private IEnumerator Spawn(int countUnit, float delaySpawn, UnitInfo unitOne, UnitInfo unitTwo, Action<BasicUnit, UnitTypeEnum> spawnUnitAction, Action endSpawnAction)
        {
            yield return null;
            yield return new WaitUntil(()=> BattleGame.Instance.GameAreaController.IsSetSize);
            for (int i = 0; i < countUnit * 2; i++)
            {
                var currentInfo = i % 2 == 0 ? unitOne : unitTwo;
                var unit = BattleGame.Instance.ObjectsPoolController.InstantiateFromPool(currentInfo.unitPrefab.gameObject, GetPositionSpawn(), Quaternion.identity, i % 2 == 0 ? PoolType.UnitsOne : PoolType.UnitsTwo).GetComponent<BasicUnit>();
                spawnUnitAction?.Invoke(unit.GetComponent<BasicUnit>(), currentInfo.unitType);
                yield return new WaitForSeconds(delaySpawn);
            }

            endSpawnAction?.Invoke();
        }
    }
}