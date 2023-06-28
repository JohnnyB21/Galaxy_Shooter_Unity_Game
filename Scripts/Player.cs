using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _nextfire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isShieldsActive = false;

    [SerializeField]
    private GameObject _playerShield;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject[] _engineFailure;

    private AudioSource _laserAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Take current player position and set it to (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        // Find and get access to Spawn Manager
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.Log("The UIManager is null.");
        }

        _laserAudio = GetComponent<AudioSource>();
        if (_laserAudio == null)
        {
            Debug.LogError("The Laser Audio Source is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        // If spacebar is hit
        // Spawn gameObject Laser

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextfire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        // Capture users vertical and horizontal input
        float horizontal_Input = Input.GetAxis("Horizontal");
        float vertical_Input = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * horizontal_Input * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * vertical_Input * _speed * Time.deltaTime);

        // Use translate and input to move the player
        Vector3 direction = new Vector3(horizontal_Input, vertical_Input, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // Set player boundaries
        // If x position is < 11 set x to -11 vice versa

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-10.9f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(10.9f, transform.position.y, 0);
        }

        // If y position is greater than 0 set y to 0

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        // If y position is less than -4 set y to -4

        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }

    void FireLaser()
    {
        _nextfire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0.277f, 0.75f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _laserAudio.Play();
        
    }


    public void Damage()
    {
        if (_isShieldsActive)
        {
            _isShieldsActive = false;
            _playerShield.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _engineFailure[0].SetActive(true);
        }

        if (_lives == 1)
        {
            _engineFailure[1].SetActive(true);
        }

        _uiManager.UpdateLives(_lives);
        
        if (_lives < 1)
        {
            // Comunicate with the spawn manager
            // Let them know to stop spawnin
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed = 12.0f;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speed = 5f;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _playerShield.SetActive(true);
    }

    public void ScorePoints()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }
    
}
