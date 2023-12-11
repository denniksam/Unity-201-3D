using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LabirinthState {
    private static List<Action<String>> observers = new();
    private static Dictionary<String, List<Action>> propertyObservers = 
        initPropertyObservers();
    private static Dictionary<String, List<Action>> initPropertyObservers()
    {
        Dictionary<String, List<Action>> res = new();
        foreach(var prop in typeof(LabirinthState).GetProperties())
        {
            res[prop.Name] = new();
        }
        return res;
    }

    public static void AddPropertyListener(String propertyName, Action listener)
    {
        if (propertyObservers.ContainsKey(propertyName))
        {
            propertyObservers[propertyName].Add(listener);
        }
        else
        {
            throw new ArgumentException($"'{propertyName}' Could not be observed");
        }
    }
    
    public static void RemovePropertyListener(String propertyName, Action listener)
    {
        if (propertyObservers.ContainsKey(propertyName))
        {
            propertyObservers[propertyName].Remove(listener);
        }
        else
        {
            throw new ArgumentException($"'{propertyName}' Could not be observed");
        }
    }

    public static void AddNotifyListener(Action<String> listener)
    {
        observers.Add(listener);
    }
    public static void RemoveNotifyListener(Action<String> listener)
    {
        observers.Remove(listener);
    }
    private static void NotifyListeners([CallerMemberName] String propertyName = "")
    {
        observers.ForEach(listener => listener.Invoke(propertyName));
        if (propertyObservers.ContainsKey(propertyName))
        {
            propertyObservers[propertyName]
                .ForEach(listener => listener.Invoke());
        }
        /*
        foreach(var listener in observers)
        {
            listener.Invoke(propertyName);
        }*/
    }

    private static float _checkPoint1Amount;
    public static float checkPoint1Amount
    {
        get { return _checkPoint1Amount; }
        set {
            if (_checkPoint1Amount != value)
            {
                _checkPoint1Amount = value; 
                NotifyListeners();
            }
        }
    }

    private static float _musicVolume;
    public static float musicVolume
    {
        get { return _musicVolume; }
        set { _musicVolume = value; NotifyListeners(); }
    }


    public static bool checkPoint1Passed;
    public static bool cameraFirstPerson;
    public static bool isDay;
    public static bool isPaused;

    public static float effectsVolume;
    public static bool isSoundsMuted;

    #region ckeckPoint2
    private static float _checkPoint2Amount;
    public static float checkPoint2Amount
    {
        get { return _checkPoint2Amount; }
        set
        {
            if (_checkPoint2Amount != value)
            {
                _checkPoint2Amount = value;
                NotifyListeners();
            }
        }
    }
    private static bool _checkPoint2Passed;
    public static bool checkPoint2Passed
    {
        get { return _checkPoint2Passed; }
        set
        {
            if (_checkPoint2Passed != value)
            {
                _checkPoint2Passed = value;
                NotifyListeners();
            }
        }
    }
    #endregion
}
/* Д.З. Архітектура оповіщення
 * Створити властивість (Property) об'єкту-стану, яка
 * відповідає за факт проходження Активатора 2го рівня.
 * Забезпечити можливість її спостережння (підписки на неї)
 * Реалізувати скрипт Активатора2, який змінює цю властивість
 * Реалізувати скрипт чекпоїнта 2, який спостерігає за
 * зміною. При активації починає зворотний відлік.
 * Синхронізувати стан чекпоїнта 2 та його іконки на дисплеї
 * (холсті)
 * ** Додати до дисплею поля (наприклад, текст з номером "рівня"
 * або час проходження етапу), також підписати їх до змін Активатора
 */
