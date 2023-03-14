using System;
using System.Collections;
using System.Collections.Generic;
using EndlessDestructionOfCubes.Scripts.Interface;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Box
{
    public class BoxMoveable : MonoBehaviour, IMoveable
    {
        public event Action<IMoveable> OnFinishMove;

        public float RotateSpeed;

        private const float MIN_DISTANCE = 0.2f;

        private Vector3 destination;
        private float speed;
        private Coroutine moveRoutine;

        public void Move(Vector3 destination, float speed)
        {
            this.destination = destination;
            this.speed = speed;
            if (moveRoutine != null)
                StopCoroutine(moveRoutine);
            moveRoutine = StartCoroutine(MoveRoutine());
        }

        public void Move(Vector3 destination)
        {
            this.destination = destination;
            if (moveRoutine != null)
                StopCoroutine(moveRoutine);
            moveRoutine = StartCoroutine(MoveRoutine());
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        private IEnumerator MoveRoutine()
        {
            while (Vector3.Distance(transform.position, destination) > MIN_DISTANCE)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination, step);
                Vector3 targetDirection = (destination - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
                yield return null;
            }
            moveRoutine = null;
            OnFinishMove?.Invoke(this);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}