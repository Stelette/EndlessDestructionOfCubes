using System;
using System.Collections;
using EndlessDestructionOfCubes.Scripts.Interface;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Bullet
{
    public class BulletMoveable : MonoBehaviour, IMoveable
    {
        public event Action<IMoveable> OnFinishMove;

        private const float EXTRA_POINT_HEIGH = 5;

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
            //calc move time
            float time = GetMoveTime();
            float elapsed_time = 0;

            Vector3 start_pos = transform.position;
            Vector3 extra_point = GetExtraPoint(start_pos, destination);
            while (elapsed_time < time)
            {
                transform.position = QuadraticCurve(start_pos, extra_point, destination, elapsed_time / time);
                elapsed_time += Time.deltaTime;
                yield return null;
            }

            yield return null;
            moveRoutine = null;
            OnFinishMove?.Invoke(this);
        }

        private float GetMoveTime()
        {
            return Vector3.Distance(transform.position, destination) / speed;
        }

        private Vector3 QuadraticCurve(Vector3 start, Vector3 extraPoint, Vector3 end, float t)
        {
            Vector3 p0 = Vector3.Lerp(start, extraPoint, t);
            Vector3 p1 = Vector3.Lerp(extraPoint, end, t);
            return Vector3.Lerp(p0, p1, t);
        }

        private Vector3 GetExtraPoint(Vector3 start, Vector3 end)
        {
            Vector3 center = (start + end) / 2;

            //get dir
            Vector3 dir = end - start;
            dir.Normalize();

            //rotate 90 degrees
            Vector3 newDir = Quaternion.AngleAxis(90, Vector3.left) * dir;

            Vector3 extra_point = center + newDir * EXTRA_POINT_HEIGH;
            return extra_point;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}