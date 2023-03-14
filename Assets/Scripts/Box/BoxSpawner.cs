using System.Collections;
using System.Collections.Generic;
using EndlessDestructionOfCubes.Scripts.Interface;
using EndlessDestructionOfCubes.Scripts.Path;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Box
{
    public class BoxSpawner : MonoBehaviour
    {
        private const int INIT_BOX_COUNT = 3;

        public Box boxPrefab;
        public Vector3 InitSpawnPos;
        public Vector2 MinPoint;
        public Vector2 MaxPoint;

        private IPathProvider _pathProvider;
        private int _boxCount = 0;

        private void Start()
        {
            _pathProvider = new PathProvider(MinPoint, MaxPoint);
            Init();
        }

        private void Init()
        {
            SpawnStartBox();
            Box.OnDestroyed += BoxDestroyed;
        }

        private void SpawnStartBox()
        {
            for (int i = 0; i < INIT_BOX_COUNT; i++)
            {
                SpawnBox();
            }
        }

        private void BoxDestroyed()
        {
            _boxCount--;
            StartCoroutine(WaitTime());
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSecondsRealtime(Random.Range(1f, 3f));
            SpawnBox();
        }

        public void SpawnBox()
        {
            Box box = Instantiate(boxPrefab, InitSpawnPos, Quaternion.identity);
            box.Init(_pathProvider);
            box.Run();
            _boxCount++;
        }

        private void OnDestroy()
        {
            Box.OnDestroyed -= BoxDestroyed;
        }
    }
}