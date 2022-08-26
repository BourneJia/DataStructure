using System;
using QFramework;
using QFramework.Framework;

namespace ShootingEditor2D
{
    public enum GunState
    {
        Idle,
        Shooting,
        Reload,
        EmptyBullet,
        CoolDown
    }
    
    public class GunInfo
    {
        public BindableProperty<int> BulletCountInGun;

        public BindableProperty<string> Name;

        public BindableProperty<GunState> GunState;

        public BindableProperty<int> BulletCountOutGun;

    }
}