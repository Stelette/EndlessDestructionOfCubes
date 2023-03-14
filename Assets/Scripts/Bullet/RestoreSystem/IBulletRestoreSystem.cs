namespace EndlessDestructionOfCubes.Scripts.Bullet.RestoreSystem
{
    public interface IBulletRestoreSystem
    {
        public void SetRestoreTime(float restoreTime);
        public void DisableRestore();
        public void EnableRestore();
        public bool IsRestoring();
    }
}