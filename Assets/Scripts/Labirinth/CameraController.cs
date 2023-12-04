using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor;

    private float camAngleX;
    private float camAngleY;
    private float rodAngleX;
    private float rodAngleY;
    private Vector3 camRod;

    void Start()
    {
        camAngleX = this.transform.eulerAngles.x;
        camAngleY = this.transform.eulerAngles.y;
        camRod = transform.position;
        rodAngleX = rodAngleY = 0f;
    }
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        camAngleX -= my;
        camAngleY += mx;
        rodAngleX -= my;
        rodAngleY += mx;

        if(Input.GetKeyDown(KeyCode.V))
        {
            LabirinthState.cameraFirstPerson  = 
                ! LabirinthState.cameraFirstPerson;
        }
    }
    private void LateUpdate()
    {
        this.transform.eulerAngles = new Vector3(camAngleX, camAngleY, 0);
        if (LabirinthState.cameraFirstPerson)
        {
            transform.position = cameraAnchor.transform.position;
        }
        else
        {
            transform.position = 
                Quaternion.Euler(rodAngleX, rodAngleY, 0) * camRod;
        }        
    }
}
/* Д.З. Обмежити діапазон управління камерою з міркувань:
 * - горизонт Світу не повинен потрапляти у її поле зору (мінімальний
 *    нахил камери близько 35)
 * - камера не переходить вертикальну точку (не перегортається)
 *    обмежити вертикальний кут 90.
 * - залишати поняття "направленості" погляду камери, тобто не
 *    дозволяти вертикальний кут рівно 90.
 */
