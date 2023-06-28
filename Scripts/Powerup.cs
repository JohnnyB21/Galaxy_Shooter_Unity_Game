using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    private AudioSource _powerUpAudio;
    private SpriteRenderer _sprite;

    [SerializeField] //0 = Triple Shot, 1 = Speed, 2 = Shields
    private int _powerupID;

    // Start is called before the first frame update
    void Start()
    {
        _powerUpAudio = GetComponent<AudioSource>();
        if (_powerUpAudio == null)
        {
            Debug.LogError("Power Up Audio Source is NULL.");
        }

        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite == null)
        {
            Debug.LogError("Sprite Renderer is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move down at a speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // When we leave screen, destroy this object

        if (transform.position.y < -10.0f)
        {
            Destroy(this.gameObject);
        }

    }

    // OnTriggerCollision
    // Only be collectable by the player
    // On collection, destroy this object

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                _powerUpAudio.Play();
                _sprite.enabled = false;
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("Speed boost collected.");
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Collected Shields.");
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            
            Destroy(this.gameObject, 1.0f);
        }

        
    }

}
