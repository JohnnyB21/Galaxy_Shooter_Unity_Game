using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        _explosionAudio = GetComponent<AudioSource>();
        if (_explosionAudio == null)
        {
            Debug.LogError("Explosion Audio Source is NULL.");
        }
        _explosionAudio.Play();
        Destroy(this.gameObject, 3.0f);

    }


}
