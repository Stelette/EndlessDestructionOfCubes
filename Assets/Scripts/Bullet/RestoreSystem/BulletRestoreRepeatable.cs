using UnityEngine;
using System.Collections;
using EndlessDestructionOfCubes.Scripts.Infrastructure;

namespace EndlessDestructionOfCubes.Scripts.Bullet.RestoreSystem
{
    public class BulletRestoreRepeatable : IBulletRestoreSystem
    {
        private readonly BulletController bulletController;

        private const int RESTORE_BULLET = 1;

        private ICoroutineRunner CoroutineRunner;
        private float restoreTime = 0;
        private Coroutine restoreCoroutine;

        public BulletRestoreRepeatable(BulletController bulletController, float restoreTime)
        {
            this.bulletController = bulletController;
            CoroutineRunner = bulletController.GetComponent<ICoroutineRunner>();
            this.restoreTime = restoreTime;
        }


        public void SetRestoreTime(float restoreTime)
        {
            this.restoreTime = restoreTime;
        }

        public void EnableRestore()
        {
            restoreCoroutine = CoroutineRunner.StartCoroutine(RestoreRoutine());
        }

        public void DisableRestore()
        {
            if (restoreCoroutine != null)
            {
                CoroutineRunner.StopCoroutine(restoreCoroutine);
                restoreCoroutine = null;
            }
        }

        private IEnumerator RestoreRoutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(restoreTime);
                bulletController.AddBulletCount(RESTORE_BULLET);
            }
        }

        public bool IsRestoring()
        {
            return restoreCoroutine != null;
        }
    }
}