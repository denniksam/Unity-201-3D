using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;

    private float camEulerX;
    private float camEulerY;
    private float camSunEulerX;
    private float camSunEulerY;
    private Vector3 camSun;  // вектор Сонце -> Камера
    private Camera _camera;

    void Start()
    {
        camEulerX = this.transform.eulerAngles.x;
        camEulerY = this.transform.eulerAngles.y;
        camSun = sun.transform.position - this.transform.position;
        camSunEulerX = 0;
        camSunEulerY = 0;
        _camera = this.GetComponent<Camera>();
    }
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");  // Це НЕ координати, а дані
        float my = Input.GetAxis("Mouse Y");  // про пересування миші
        camEulerX -= my;
        camEulerY += mx;
        if (Input.GetMouseButton(0))
        {
            camSunEulerX -= my;
            camSunEulerY += mx;
        }
    }
    private void LateUpdate()
    {
        // поворот камери навколо свого центру
        this.transform.eulerAngles = new Vector3(camEulerX, camEulerY, 0f);
        if (Input.GetMouseButton(0))
        {
            // поворот вектора camSun
            this.transform.position =
                sun.transform.position -
                Quaternion.Euler(camSunEulerX, camSunEulerY, 0) * camSun;
        }
        Vector2 scroll = Input.mouseScrollDelta;
        if(scroll != Vector2.zero)
        {
            // Обмежити зміни поля зору діапазоном [5..120]
            float newField = _camera.fieldOfView - scroll.y;
            if(newField >= 5f && newField <= 120)
            {
                _camera.fieldOfView = newField;
            }
            else
            {
                if (_camera.fieldOfView < 5f) _camera.fieldOfView = 5f;
                if (_camera.fieldOfView > 120f) _camera.fieldOfView = 120f;
            }
        }
    }
}
/* Управління камерою
 * 1. Обертання рухами миші
 *  - недосконалий підхід: this.transform.Rotate(-my, mx, 0);
 *     поворот по двох осях призводить до ефекту повороту по третій осі.
 *     "поворот повернутого" тіла відбувається по всіх осях.
 *  - більш правильний підхід: безпосередньо встановлювати 
 *     кути повороту (кути Ейлера) із збереженням значення 0 для Z
 *     
 * 2. Різні режими управління
 *  - обертання тільки навколо своєї (камери) осі
 *  - обертання навколо деякого центру (поза камерою)
 *  
 * 3. Комбінований режим
 *  - прості рухи миші - обертання навколо власної осі
 *  - рух із затисненою клавішою - обертання навколо Сонця
 *  
 *  Д.З. Реалізувати планету Марс та її супутники (достатньо одного)
 *  Скласти алгоритми обертання планети та супутників, досягнути
 *  правильних траєкторій.
 */
