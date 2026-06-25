using System;
using System.Collections;
using UnityEngine;

public class ReloadVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Gun _gun;
    [SerializeField] private Transform _gunHandTransform;

    [Header("Visuals")]
    [SerializeField] private Transform _cylinderTransform;
    [SerializeField] private Transform _cylinderChamberTemplateTransform;
    [SerializeField] private int _minimumCylinderRovolutionCount = 10;
    [SerializeField] private AnimationCurve _cylinderSpinCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private SpriteRenderer[] _cylinderChamberSpriteRendererArray;

    private void Awake()
    {
        _cylinderTransform.gameObject.SetActive(false);
        SetChamberLocations();
    }

    private void Start()
    {
        _gun.OnGunReloaded += OnGunReloaded;
    }

    private void Update()
    {
        int cylinderSignedPosition = (int)Mathf.Sign(transform.localPosition.x);
        int gunSignedPosition = (int)Mathf.Sign(_gunHandTransform.localPosition.x);

        if(cylinderSignedPosition != gunSignedPosition)
            return;

        transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
    }




    private void SetChamberLocations()
    {
        int chamberCount = _gun.GetCylinderChamberCount();
        _cylinderChamberSpriteRendererArray = new SpriteRenderer[chamberCount];
        float anglePerChamber = 360 / chamberCount;

        for(int i = 0; i < chamberCount; i++)
        {
            Transform newChamberTransform = Instantiate(_cylinderChamberTemplateTransform, _cylinderTransform);
            Vector2 chamberPosition = Quaternion.Euler(0f, 0f, anglePerChamber * i) * _cylinderChamberTemplateTransform.localPosition;

            newChamberTransform.localPosition = chamberPosition;
            _cylinderChamberSpriteRendererArray[i] = newChamberTransform.GetComponent<SpriteRenderer>();
        }

        _cylinderChamberTemplateTransform.gameObject.SetActive(false);
    }





    private void OnGunReloaded(object sender, Gun.OnGunReloadEventArgs e)
    {
        SetChamberBulletVisuals(e.BulletsInCylinder);

        float finalRotation = GetChamberAngle(e.BulletsInCylinder, e.SelectedBullet);

        float anglePerChamber = 360 / e.BulletsInCylinder.Length;
        float startRotation = -360 * _minimumCylinderRovolutionCount - anglePerChamber * UnityEngine.Random.Range(0, e.BulletsInCylinder.Length);

        StartCoroutine(GunReloadAnimation(startRotation, finalRotation, e.ReloadDuration));
    }






    private void SetChamberBulletVisuals(Bullet[] bulletsInCylinder)
    {
        for(int i = 0; i < bulletsInCylinder.Length; i++)
        {
            if(bulletsInCylinder[i] == null)
            {
                _cylinderChamberSpriteRendererArray[i].enabled = false;
                continue;
            }

            _cylinderChamberSpriteRendererArray[i].enabled = true;
            _cylinderChamberSpriteRendererArray[i].color = bulletsInCylinder[i].BulletDataSO.ColorInCylinder;
        }
    }


    private float GetChamberAngle(Bullet[] bulletsInCylinder, Bullet selectedBullet)
    {
        float anglePerChamber = 360 / bulletsInCylinder.Length;
        int index = Array.IndexOf(bulletsInCylinder, selectedBullet);
        return -anglePerChamber * index;
    }


    private IEnumerator GunReloadAnimation(float startRotation, float finalRotation, float reloadDuration)
    {
        _cylinderTransform.gameObject.SetActive(true);

        Vector3 eulerStartRotation = new Vector3(0, 0, startRotation);
        Vector3 eulerFinalRotation = new Vector3(0, 0, finalRotation);

        float reloadTimer = 0;
        while(reloadTimer < reloadDuration)
        {
            float t = _cylinderSpinCurve.Evaluate(reloadTimer / reloadDuration);

            Vector3 eulerRotation = Vector3.Lerp(eulerStartRotation, eulerFinalRotation, t);

            _cylinderTransform.rotation = Quaternion.Euler(eulerRotation);


            reloadTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        _cylinderTransform.gameObject.SetActive(false);
    }
}
