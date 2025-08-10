using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    void FixedUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
    }
    void LateUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!GameManager.instance._IsLive)
            return;

        if (!collider.gameObject.CompareTag("Enemy"))
            return;
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();

        GameManager.instance._hp -= enemy._enemyData.Damage;
        foreach (var item in GameManager.instance._InGame2Manager._BackGroundTiles)
        {
            item.ResetSpeed();
        }
        // foreach (var enemyPool in GameManager.instance._InGame2Manager._enemyPoolManager)
        // {
        //     foreach (var item in enemyPool._Pool)
        //     {
        //         item.ResetSpeed();
        //     }
        // }
    }
}
