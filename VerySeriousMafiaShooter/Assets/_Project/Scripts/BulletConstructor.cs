using System.Collections.Generic;
using System;
using UnityEngine;

public class BulletConstructor : MonoBehaviour
{

    [SerializeField] private bool _loadPreconstructedBulletsIntoGun;
    [SerializeField] private Gun _gun;
    [SerializeField] private BulletConstructionData[] _preconstructedBulletArray;

    private List<Bullet> _bullets = new();


    private void Awake()
    {
        ConstructPreconstructedBullets();
    }

    private void Start()
    {
        if(_loadPreconstructedBulletsIntoGun)
            LoadPreconstructedBullets();
    }

    private void LoadPreconstructedBullets()
    {
        foreach(Bullet bullet in _bullets)
        {
            _gun.LoadBullet(bullet);
        }
    }



    private void ConstructPreconstructedBullets()
    {
        foreach(BulletConstructionData bulletData in _preconstructedBulletArray)
        {
            ConstructBullet(bulletData.TriggerTypeBehaviour, bulletData.BulletDataSO);
        }
    }

    private void ConstructBullet(ITriggerTypeBehaviour triggerBehaviour, BulletDataSO bulletDataSO)
    {
        Bullet bullet = new Bullet(triggerBehaviour, bulletDataSO);
        _bullets.Add(bullet);
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
