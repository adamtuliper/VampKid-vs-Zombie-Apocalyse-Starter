using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{

    private GameController _gameController;

    // Use this for initialization
    void Start()
    {
        _gameController = GameController.GetGameControllerInScene();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_gameController) _gameController.IncrementCoinScore(10);
            Destroy(gameObject);
        }
    }
}
