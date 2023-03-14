using System;
using System.Collections;
using System.Collections.Generic;
using EndlessDestructionOfCubes.Scripts.Interface;
using EndlessDestructionOfCubes.Scripts.Bullet.Data;
using EndlessDestructionOfCubes.Scripts.Bullet.RestoreSystem;
using EndlessDestructionOfCubes.Scripts.Infrastructure;
using EndlessDestructionOfCubes.Scripts.Infrastructure.Timers;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Bullet
{
    public class BulletController : MonoBehaviour, ICoroutineRunner
    {
        public event Action<int> OnChangeBulletCount;

        [SerializeField]
        private int maxBulletCount;

        [SerializeField]
        private BulletData bulletData;

        [SerializeField]
        private GameObject BulletPrefab;

        [SerializeField]
        private Vector3 StartSpawnPos;


        private int _bulletCount;
        private IBulletRestoreSystem _restoreBulletSystem;
        private Countdown Cooldown;

        public void Start()
        {
            _restoreBulletSystem = new BulletRestoreRepeatable(this, bulletData.RestoreTime);
            Cooldown = new Countdown();
            AddBulletCount(maxBulletCount);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && IsAvailableShoot())
            {
                Shoot();
            }
        }

        private bool IsAvailableShoot()
        {
            return _bulletCount > 0 && Cooldown.IsOver;
        }

        private void Shoot()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                CreateBullet(hit.point);
                RemoveBulletCount(1);
                Cooldown.Run(bulletData.CoolDownTime);
            }
        }

        private void CreateBullet(Vector3 destination)
        {
            IMoveable moveable = Instantiate(BulletPrefab, StartSpawnPos, Quaternion.identity).GetComponent<IMoveable>();
            moveable.Move(destination, bulletData.Speed);
            moveable.OnFinishMove += DestroyBullet;
        }

        private void DestroyBullet(IMoveable moveable)
        {
            moveable.OnFinishMove -= DestroyBullet;
            Destroy(moveable.GetTransform().gameObject);
        }


        #region Add/Remove bullet

        public void AddBulletCount(int addBullet)
        {
            _bulletCount = Mathf.Clamp(_bulletCount + addBullet, 0, maxBulletCount);
            OnChangeBulletCount?.Invoke(_bulletCount);

            CheckRestoreSystem();
        }

        private void CheckRestoreSystem()
        {
            if (IsAvailableRestore())
                _restoreBulletSystem.EnableRestore();
            else if (IsPossibleDisableRestore())
                _restoreBulletSystem.DisableRestore();
        }

        public void RemoveBulletCount(int removeBullet)
        {
            if (_bulletCount >= removeBullet)
                _bulletCount -= removeBullet;

            OnChangeBulletCount?.Invoke(_bulletCount);

            if (IsAvailableRestore())
                _restoreBulletSystem.EnableRestore();
        }

        private bool IsAvailableRestore()
        {
            return !_restoreBulletSystem.IsRestoring() && _bulletCount < maxBulletCount;
        }

        private bool IsPossibleDisableRestore()
        {
            return _restoreBulletSystem.IsRestoring() && _bulletCount == maxBulletCount;
        }

        #endregion
    }
}
