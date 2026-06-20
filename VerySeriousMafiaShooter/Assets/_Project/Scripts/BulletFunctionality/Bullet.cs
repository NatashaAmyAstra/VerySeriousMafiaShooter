using System.Collections.Generic;

public class Bullet
{

    public Bullet(ITriggerTypeBehaviour behaviour, BulletDataSO bulletDataSO)
    {
        _triggerTypeBehaviour = behaviour;
        _bulletDataSO = bulletDataSO;
    }

    // bullet type
    private ITriggerTypeBehaviour _triggerTypeBehaviour;
    public ITriggerTypeBehaviour TriggerTypeBehaviour { get { return _triggerTypeBehaviour; } set { } }

    private BulletDataSO _bulletDataSO;
    public BulletDataSO BulletDataSO { get { return _bulletDataSO; } set { } }

    // list of trigger effects
        // maybe later :3

    // list of chamber effects

}
