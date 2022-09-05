using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{
    public static SettingsHolder Instance;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("A SettingsHolder already exists");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }



    public static Vector3 GetWorldPositionWithZ(Vector3 screenPosition, Camera mainCamera)
    {

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;

    }


    public static Vector3 GetMousePosition()
    {
        Vector3 v3 = GetWorldPositionWithZ(Input.mousePosition, Camera.main);
        v3.z = 0f;
        return v3;
    }



}
