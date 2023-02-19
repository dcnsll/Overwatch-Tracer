using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public int uses;
    public float cooldown, distance, speed, destinationMultiplier, cameraHeight;
    public Text UIText;
    public Transform cam;
    public LayerMask layerMask;


    int maxUses;
    float cooldownTimer;
    bool blinking = false;
    Vector3 destination;
    TrailRenderer trail;

    void Start()
    {
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        maxUses = uses;
        cooldownTimer = cooldown;
        UIText.text = uses.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BlinkFunction();
        }

        if (uses < maxUses)
        {
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
            else { uses += 1; cooldownTimer = cooldown; UIText.text = uses.ToString(); }
        }

        if (blinking)
        {
            var dis = Vector3.Distance(transform.position, destination);
            if (dis > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            }
            else { blinking = false; }
        }

    }

    void BlinkFunction()
    {
        if (uses > 0)
        {
            uses -= 1;
            UIText.text = uses.ToString();

            RaycastHit hit;
            if (Physics.Raycast (cam.position, cam.forward, out hit , distance, layerMask))
            {
                destination = hit.point * destinationMultiplier;
            }
            else
            {
                destination = (cam.position + cam.forward.normalized * distance) * destinationMultiplier;

            }

            destination.y += cameraHeight;
            blinking = true;
             
        }
    }
}

