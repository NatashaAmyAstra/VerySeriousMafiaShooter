using UnityEngine;

public class GunVisual : MonoBehaviour
{
    [SerializeField] private Transform _gunHolderOriginTransform;

    private void Update() {
        int lookDirection = (int) Mathf.Sign(transform.position.x - _gunHolderOriginTransform.position.x);
        transform.localScale = new Vector3(1, lookDirection, 1);
    }
}
