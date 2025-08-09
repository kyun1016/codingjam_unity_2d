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
        DevLog.Log("Player collided with: " + collider.gameObject.name);
        if (!GameManager.instance._IsLive)
            return;

        if (!collider.gameObject.CompareTag("Enemy"))
            return;

        GameManager.instance._hp -= 10;
        for (int i = 0; i < GameManager.instance._BackGroundTiles.Length; i++)
        {
            GameManager.instance._BackGroundTiles[i].GetComponent<BackGround>().ResetSpeed();
        }
    }
}
