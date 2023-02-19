using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : MonoBehaviour
{
    public float maxDuration = 3;
    public float saveInterval = 0.5f;
    public float recallSpeed = 5;
    public CanvasGroup cameraFX;

    public List<Vector3> positions;

    private bool recalling;
    private float saveStatstimer;
    private float maxStatsStored;

    void Start()
    {
        maxStatsStored = maxDuration / saveInterval;
        
    }

    void Update()
    {
        if (!recalling)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && positions.Count >0)
            {
                recalling = true;
            }

            if (saveStatstimer >0)
            {
                saveStatstimer -= Time.deltaTime;
            }
            else { StoreStats(); }

            cameraFX.alpha = Mathf.Lerp(cameraFX.alpha, 0, recallSpeed * Time.deltaTime);

        }
        else
        {
            if (positions.Count > 0 )
            {
                transform.position = Vector3.Lerp(transform.position, positions[0], recallSpeed * Time.deltaTime);

                float dist = Vector3.Distance(transform.position, positions[0]);
                if (dist<0.25f)
                {
                    SetStats();
                }
            }
            else
            {
                recalling = false;
            }

            cameraFX.alpha = Mathf.Lerp(cameraFX.alpha, 1, recallSpeed * Time.deltaTime);
        }
    }

    void StoreStats()
    {
        saveStatstimer = saveInterval;
        positions.Insert(0, transform.position);

        if (positions.Count > maxStatsStored)
        {
            positions.RemoveAt(positions.Count - 1);
        }
    }
    void SetStats()
    {
        transform.position = positions[0];

        positions.RemoveAt(0);
    }

}
