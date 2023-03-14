using UnityEngine;
using System.Collections;
using EndlessDestructionOfCubes.Scripts.Interface;
using EndlessDestructionOfCubes.Scripts.Path;
using System;

namespace EndlessDestructionOfCubes.Scripts.Box
{
    public class Box : MonoBehaviour
    {
        public static event Action OnDestroyed;

        public float Speed;

        private IMoveable moveable;
        private IPathProvider _pathProvider;

        public void Init(IPathProvider pathProvider)
        {
            moveable = GetComponent<IMoveable>();
            _pathProvider = pathProvider;
            moveable.OnFinishMove += OnFinishMove;
        }

        private void OnFinishMove(IMoveable moveable)
        {
            Run();
        }

        public void Run()
        {
            Move(_pathProvider.GetDestination());
        }

        public void Move(Vector3 destination)
        {
            moveable.Move(destination, Speed);
        }

        public void SetSpeed(float speed)
        {
            moveable.SetSpeed(speed);
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}