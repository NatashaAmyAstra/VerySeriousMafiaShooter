using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletConstructor : MonoBehaviour
{
    [SerializeField] private GunUserBullets[] _gunUserBullets;


    private void Start()
    {
        LoadPreconstructedBullets();
    }

    private void LoadPreconstructedBullets()
    {
        foreach(GunUserBullets gunUser in _gunUserBullets)
        {
            Gun gun;
            if(gunUser.IsPlayer)
                gun = Player.Instance.transform.GetComponentInChildren<Gun>();
            else
                gun = gunUser.User.GetComponentInChildren<Gun>();

            foreach(BulletConstructionData bullet in gunUser.Bullets)
            {
                gun.LoadBullet(ConstructBullet(bullet.TriggerTypeBehaviour, bullet.BulletDataSO));
            }
        }
    }

    private Bullet ConstructBullet(ITriggerTypeBehaviour triggerBehaviour, BulletDataSO bulletDataSO)
    {
        return new Bullet(triggerBehaviour, bulletDataSO);
    }

}


[Serializable]
public class BulletConstructionData
{
    public enum triggerType
    {
        Basic,
        Shotgun,
        Sniper
    }

    private Dictionary<triggerType, ITriggerTypeBehaviour> _triggerTypeBehaviourDict = new() {
        { triggerType.Basic, new BasicTriggerBehaviour() },
        { triggerType.Shotgun, new ShotgunTriggerBehaviour() }
    };

    [SerializeField] triggerType TriggerType;
    public ITriggerTypeBehaviour TriggerTypeBehaviour { get { return _triggerTypeBehaviourDict[TriggerType]; } set { } }
    public BulletDataSO BulletDataSO;
}


[Serializable]
public class GunUserBullets
{
    public bool IsPlayer;
    public Transform User;
    public BulletConstructionData[] Bullets;
}
