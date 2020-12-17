using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    void Start()
    {
        
    }

     void Update()
    {
        if (!shopUI.instance.ShopUIPanel.activeSelf)
        {
            currentView = views[0];
        }
        if (shopUI.instance.ShopUIPanel.activeSelf)
        {
            currentView = views[1];
        }

       
    }

     void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed); 
    }
}
