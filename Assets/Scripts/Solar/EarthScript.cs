using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;

    private GameObject surface;
    private GameObject atmosphere;
    private float dayPeriod = 24f / 360f;
    private float skyPeriod = 12f / 360f;
    private float yearPeriod = 100f / 360f;

    void Start()
    {
        surface    = GameObject.Find("EarthSurface");
        atmosphere = GameObject.Find("EarthAtmosphere");
    }

    void Update()
    {
        surface.transform.Rotate(Vector3.up, Time.deltaTime / dayPeriod, Space.Self);
        atmosphere.transform.Rotate(Vector3.up, Time.deltaTime / skyPeriod);

        this.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime / yearPeriod);
    }
}
/* Вектор:
 * - (в Юніті) це тип даних (структура), яка зберігає дані та декларує основні
 *    методи для роботи з ними. Vector2 - два числа (x,y), Vector3 - (x,y,z)
 * - (в алгебрі) це впорядкована множина чисел (або інших об'єктів)
 * - (у геметрії)
 *  = спрямований відрізок - характеристика напряму (наприклад, руху чи обертання)
 *  = точка у просторі (з координатами x,y,z)
 *  
 *  Д.З. Композиція об'єктів: створити планету Венера разом з атмосферою
 *  Скласти скрипт її обертання навколо власної осі
 */
