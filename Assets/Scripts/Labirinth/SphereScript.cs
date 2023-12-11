using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SphereScript : MonoBehaviour
{
    // [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private GameObject cameraAnchor;

    private Rigidbody body;
    private float forceFactor = 500f;
    private Vector3 anchorOffset;

    private AudioSource collectSound;
    private AudioSource backgroundMusic;

    private static SphereScript instance = null;

    void Start()
    {
        if(instance != null)  
        {
            // Цей код викликається якщо
            // спавниться новий ГО у новій сцені, але є збережений 
            // об'єкт (instance) перенесений з попередньої сцени
            // Треба перенести з нього потрібні характеристики та
            // видалити його, перейшовши на роботу з "місцевим" ГО

            this.transform.position += 
                new Vector3(0, instance.transform.position.y, 0);

            GameObject.Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        _camera = Camera.main.gameObject;
        Debug.Log("SphereScript::Start");

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
/* Взаємодія між сценами
 * - Статичний контекст (наприклад, LabirinthState) існує між сценами
 *    Дані, записані скриптами однієї сцени, залишаються доступними для
 *    іншої.
 *    Динамічні об'єкти руйнуються при переході між сценами.
 * - DontDestroyOnLoad запобігає руйнуванню об'єкту (GameObject) у т.ч.
 *    його внутрішнього складу. Зберігаються усі характеристики об'єкту - 
 *    позиція, повороти, фізичні показники (швидкість руху, обертання,
 *    участь у гравітації) тощо.
 *    Але усі зв'язки, утворені зі зруйнованою
 *    сценою, при переході на іншу спричинять 
 *    MissingReferenceException: The object ... has been destroyed but you are still trying to access it.
 *    Оскільки Start при переході збереженого об'єкту не повторюється,
 *    відновити зв'язки ускладнено.
 * - Як варіант, на кожній сцені створюють свій "клон" персонажа,
 *    а при старті сцени, коли виникає два об'єкти (новий та збережений),
 *    приймається рішення про залишок одного з них.
 */
