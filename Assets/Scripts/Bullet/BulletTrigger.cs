using UnityEngine;

namespace EndlessDestructionOfCubes.Scripts.Bullet
{
    public class BulletTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
