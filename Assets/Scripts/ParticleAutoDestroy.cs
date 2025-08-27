using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (ps && !ps.IsAlive()) {
            DestroySelfAnimEvent();
        }
    }

    public void DestroySelfAnimEvent() {
        Destroy(this.gameObject);
    }
}
