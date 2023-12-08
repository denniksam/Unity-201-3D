using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private GameObject cameraAnchor;

    private Rigidbody body;
    private float forceFactor = 500f;
    private Vector3 anchorOffset;

    private AudioSource collectSound;
    private AudioSource backgroundMusic;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        anchorOffset = this.transform.position - 
            cameraAnchor.transform.position;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        collectSound = audioSources[0];
        backgroundMusic = audioSources[1];

        if (!LabirinthState.isSoundsMuted)
        {
            // backgroundMusic.volume = LabirinthState.musicVolume;
            backgroundMusic.Play();
        }
        LabirinthState.AddNotifyListener(OnLabirinthStateChanged);
    }

    void Update()
    {
        float kh = Input.GetAxis("Horizontal");
        float kv = Input.GetAxis("Vertical");

        Vector3 right = _camera.transform.right;
        Vector3 forward = _camera.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 forceDirection = // new Vector3(kh, 0, kv); - World space
            kh * right + kv * forward;

        body.AddForce(forceFactor * Time.deltaTime * forceDirection);

        cameraAnchor.transform.position = this.transform.position -
            anchorOffset;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("SphereScript " + other.name);
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            if (!LabirinthState.isSoundsMuted)
            {
                collectSound.Play();
            }
        }
    }

    private void OnDestroy()
    {
        LabirinthState.RemoveNotifyListener(OnLabirinthStateChanged);
    }

    private void OnLabirinthStateChanged(string propertyName)
    {
        if(propertyName == nameof(LabirinthState.musicVolume))
        {
            Debug.Log("OnLabirinthStateChanged: " + propertyName);
            // if(backgroundMusic.volume != LabirinthState.musicVolume)
            // {
            //     backgroundMusic.volume = LabirinthState.musicVolume;
            // }
        }
    }
}
/* Д.З. Управління гучністю
 * Реалізувати передачу даних від UI елементів
 * до реальних налаштувань гучності звуків та музики
 * ** Повторити патерн "Спостерігач" та ChangeNotifier
 */
