using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EndlessDestructionOfCubes.Scripts.Bullet;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.View
{
    public class BulletView : MonoBehaviour
    {
        public BulletController bulletController;

        public Transform SpawnContent;
        public GameObject BulletIconPrefab;

        private List<GameObject> bulletIcons = new List<GameObject>();

        void Awake()
        {
            bulletController.OnChangeBulletCount += UpdateBulletUI;
        }

        private void UpdateBulletUI(int bulletCount)
        {
            int difference = bulletIcons.Count - bulletCount;

            if (difference > 0)
            {
                RemoveBulletIcons(difference);
            }
            else
            {
                AddBulletIcons(Mathf.Abs(difference));
            }
        }

        private void RemoveBulletIcons(int removeCount)
        {
            for(int i = 0; i < removeCount; i++)
            {
                GameObject icon = bulletIcons.Last();
                bulletIcons.Remove(icon);
                Destroy(icon);
            }
        }

        private void AddBulletIcons(int addCount)
        {
            for (int i = 0; i < addCount; i++)
            {
                GameObject icon = Instantiate(BulletIconPrefab, SpawnContent);
                bulletIcons.Add(icon);
            }
        }

        private void OnDestroy()
        {
            if(bulletController != null)
                bulletController.OnChangeBulletCount -= UpdateBulletUI;
        }
    }
}