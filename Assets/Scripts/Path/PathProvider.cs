using UnityEngine;
using System.Collections;

namespace EndlessDestructionOfCubes.Scripts.Path
{
    public class PathProvider : IPathProvider
    {
        public Vector2 MinPoint;
        public Vector2 MaxPoint;

        public PathProvider(Vector2 Min, Vector2 Max)
        {
            MinPoint = Min;
            MaxPoint = Max;
        }

        public Vector3 GetDestination()
        {
            float x = Random.Range(MinPoint.x, MaxPoint.x);
            float z = Random.Range(MinPoint.y, MaxPoint.y);
            return new Vector3(x, 0.5f, z);
        }
    }
}