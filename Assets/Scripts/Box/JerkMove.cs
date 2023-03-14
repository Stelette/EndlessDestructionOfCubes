using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Box
{
    public class JerkMove : MonoBehaviour
    {
        public float MinWaitTime = 3;
        public float MaxWaitTime = 6;

        public float MultiplierSpeed = 4f;
        public Box box;

        private float defaultSpeed = 0;

        void Start()
        {
            defaultSpeed = box.Speed;
            StartCoroutine(JerkRoutine());
        }

        private IEnumerator JerkRoutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(Random.Range(MinWaitTime, MaxWaitTime));
                box.SetSpeed(defaultSpeed * MultiplierSpeed);
                yield return new WaitForSecondsRealtime(2f);
                box.SetSpeed(defaultSpeed);
            }
        }

    }
}