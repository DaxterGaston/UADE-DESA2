
using UnityEngine;
using Assets.Scripts.Bullets;

namespace Assets.Scripts.DP.Factory
{
    public class BulletFactory : MonoBehaviour, IFactory<BaseBullet, BaseBulletSO>
    {
        [SerializeField]
        private BaseBullet _prefab;
        public BaseBullet Product => _prefab;

        public BulletFactory(BaseBullet prefab)
        {
            _prefab = prefab;
        }

        public BaseBullet Create() => Instantiate(_prefab);
            

        public BaseBullet[] Create(int quantity)
        {
            BaseBullet[] bullets = new BaseBullet[quantity];
            for (int i = 0; i < quantity; i++)
            {
                bullets[i] = Create();
            }
            return bullets;
        }
    }
}
