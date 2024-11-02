using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] float airSpeed = 0.005f;
    [SerializeField] float steerSpeed = 50.0f;
    [SerializeField] TextMeshProUGUI airSpeedLabel;
    [SerializeField] TextMeshProUGUI yawLabel;
    [SerializeField] GameObject planeProjection;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        movePlane();
        generateProjection();
        updateDashboard();
        quit();
    }

    void movePlane() 
    {
        // -1 or 1 based on the left/right arrow key press
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        // -1 or 1 based on the down/up arrow key press
        airSpeed += Input.GetAxis("Vertical") * 0.01f * Time.deltaTime;

        // give's air speed lower and upper bound
        // range exagerated to demo controls (high = 10 x low)
        if(airSpeed >= 0.05f){
            airSpeed = 0.05f;
        } else if(airSpeed <= 0.005f) {
            airSpeed = 0.005f;
        }

        // update rotation and translation based on the yaw and air speed
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, airSpeed, 0);
    }

    // generates plane projection based on current air speed
    // i.e it always takes the plane the same time to arrive at the projection
    void generateProjection()
    {
        planeProjection.transform.position = transform.position;
        planeProjection.transform.rotation = transform.rotation;
        planeProjection.transform.Translate(0, airSpeed*100, 0);

        // workaround to avoid overlap with the line render
        transform.Translate(0, 0.5f, 0);
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, planeProjection.transform.position);

        // resets to correct position after line has been rendered
        transform.Translate(0, -0.5f, 0);
        planeProjection.transform.Translate(0, 0.5f, 0);
    }

    // updates the airplanes "dashboard"
    // contains air speed and yaw
    // yaw degrees measured as N:0, W:90, E:-90 S:180
    void updateDashboard()
    {
        // speed interpolation to mimic a boeing's min/max speed while cruising
        float interpolatedAirSpeed = 250+((airSpeed-0.005f)/0.045f*238);
        airSpeedLabel.SetText($"Air Speed: {interpolatedAirSpeed:0.0}knots");
        // left yaw recorded as positive, right yaw as negative where north is 0 and south is 180
        float yaw = transform.rotation.eulerAngles.z < 180 ? transform.rotation.eulerAngles.z : transform.rotation.eulerAngles.z - 360;
        yawLabel.SetText($"Yaw: {yaw:0.0}°");
    }

    void quit()
    {
        // allows for quitting the app by pressing esc key
        if(Input.GetKeyDown("escape")){
            Application.Quit();
        }
    }
}
