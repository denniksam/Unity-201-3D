using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates2Script : MonoBehaviour
{
    private float period = 100f / 360f;
    void Start()
    {
        LabirinthState.AddPropertyListener(
            nameof(LabirinthState.checkPoint2Passed),
            OnCheckPoint2Changed);
    }
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime / period);
    }
    private void OnCheckPoint2Changed()
    {
        if (LabirinthState.checkPoint2Passed)
        {
            period /= 10f;
        }
    }
    private void OnDestroy()
    {
        LabirinthState.RemovePropertyListener(
            nameof(LabirinthState.checkPoint2Passed),
            OnCheckPoint2Changed);
    }
}
/*�.�. ������� �� �������:
 * ���������� ���������� ��������� � 2� ����� �� �����
 * �� ����������� ���� 3 �������. ��������� ������������
 * ������ DontDestroyOnLoad
 */
