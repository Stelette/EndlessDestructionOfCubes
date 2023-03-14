using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Path
{
    public interface IPathProvider
    {
        Vector3 GetDestination();
    }
}