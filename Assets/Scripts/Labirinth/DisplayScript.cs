using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI clock;
    [SerializeField]
    private Image image1;  // for checkpoint1 indicator

    private float gameTime;

    void Start()
    {
        LabirinthState.AddPropertyListener(
            nameof(LabirinthState.checkPoint1Amount),
            OnPoint1AmountChanged);
        gameTime = 0f;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }
    private void LateUpdate()
    {
        
    }
    private void OnPoint1AmountChanged()
    {
        image1.fillAmount = LabirinthState.checkPoint1Amount;
    }
    private void OnDestroy()
    {
        LabirinthState.RemovePropertyListener(
            nameof(LabirinthState.checkPoint1Amount),
            OnPoint1AmountChanged);
    }
}
/* Д.З. Реалізувати відображення ігрового часу.
 * Переконатись, що на паузі час не іде.
 * Розділити поле Score: 0 
 * на Score:  та його значення (окремим полем),
 * забезпечити зміну рахунку: на початку 100 та
 * з кожним інтервалом 5 секунд зменшується на 1.
 */
