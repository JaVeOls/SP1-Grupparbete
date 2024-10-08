using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    [SerializeField] private Vector3 minValues, maxValues;
    [SerializeField] private float smoothing = 1.0f;
  
    void LateUpdate()
    {
        if (boss.inBattle == true)
        {
            Vector3 targetPosition = target.position + offset;

            //Limit the camera movement to stay in bounds during the bossbattle with the min- and max values
            Vector3 boundPosition = new Vector3(
                Mathf.Clamp(targetPosition.x, 25, 30),
                Mathf.Clamp(targetPosition.y, -2, 4),
                Mathf.Clamp(targetPosition.z, -10, -10));

            Vector3 bossPosition = Vector3.Lerp(transform.position, boundPosition, smoothing * Time.deltaTime);
            transform.position = bossPosition;
        }

        if(boss.inBattle == false) 
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
            transform.position = newPosition;
        }
       // Debug.Log($"Camera coordinates: {transform.position}, Target coordinates: {target.position }");
    }
}



