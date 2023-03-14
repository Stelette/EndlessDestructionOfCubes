using System;
using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Interface
{
    public interface IMoveable
    {
        public event Action<IMoveable> OnFinishMove;
        public Transform GetTransform();
        public void SetSpeed(float speed);
        public void Move(Vector3 destination, float speed);
        public void Move(Vector3 destination);
    }
}

