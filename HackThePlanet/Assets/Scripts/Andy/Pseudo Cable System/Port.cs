using UnityEngine;

public class Port : MonoBehaviour
{
    public MatchEntity _ownerMatchEntity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovablePair CollidedMoveable))
        {
            _ownerMatchEntity.PairObjectInteraction(IsEnter:true, CollidedMoveable); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovablePair CollidedMoveable))
        {
            _ownerMatchEntity.PairObjectInteraction(IsEnter:false, CollidedMoveable);
        }
    }
}
