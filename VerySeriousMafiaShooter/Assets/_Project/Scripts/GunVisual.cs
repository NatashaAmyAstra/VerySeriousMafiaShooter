using UnityEngine;

public class GunVisual : MonoBehaviour
{
    [SerializeField] private Transform _gunHolderOriginTransform;

    private void Update() {
        int lookDirection = (int) Mathf.Sign(transform.position.x - _gunHolderOriginTransform.position.x);
        transform.localScale = new Vector3(lookDirection, 1, 1);
        transform.localRotation = Quaternion.Euler(0, 0, -90 + lookDirection * 90);
    }
}
