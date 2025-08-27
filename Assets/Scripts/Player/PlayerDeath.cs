using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Tilemap map;
    [SerializeField] GridInformation mapInfo;

    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Die();
        }
        //CheckDeath(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //CheckDeath(collision);
    }

    private void CheckDeath(Collision2D collider)
    {
        /*
        foreach (var contacts in collider.contacts)
        {
            var positionInside = contacts.point - contacts.normal * 0.01f;
            var cell = map.WorldToCell(positionInside);
            var deadly = mapInfo.GetPositionProperty(cell, "deadly", 0);
            if (deadly == 1)
            {
                Die();
                break;
            }
        }
        */
    }

    private void Die()
    {
        transform.position = initialPosition;
    }
}
