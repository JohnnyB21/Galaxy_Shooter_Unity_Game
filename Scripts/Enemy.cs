using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;

    

    [SerializeField]
    private GameObject _enemyExplosion;
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bottom of screen
        if (transform.position.y < -8)
        {
            // respawn at top with new random x pos
            float randx = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(randx, 8, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is Player
        // damage the player
        // destroy us
        if (other.tag == "Player")
        {

            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _speed = 0;
            Destroy(this.gameObject, 0.2f);
        }

        // if other is laser
        // destroy the laser
        // destroy us
        if (other.tag == "Laser")
        {
            Instantiate(_enemyExplosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.ScorePoints();
            }

            _speed = 0;
            Destroy(this.gameObject, 0.2f);
        }
        
    }
}
