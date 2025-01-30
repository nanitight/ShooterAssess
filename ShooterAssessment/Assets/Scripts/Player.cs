using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        LineRenderer lineRenderer;
        [SerializeField]
        float lineMaxDistance = 100f, MouseSensitivity = 100f, xRotationLimit = 15, yRotationLimit = 45,
            maxTimerForShooting = 0.5f;
        float timer = 0;
        [SerializeField, Range(0.1f, 1f)]
        float lineEndWidth = 0.1f, lineStartWidth = 0.1f;
        bool canShootAgain = true;

        private void Start()
        {
            if (lineRenderer == null)
            {
                if (!TryGetComponent<LineRenderer>(out lineRenderer))
                {
                    string msg = "Attach Line Renderer Component to gameobject";
                    Debug.LogError(msg, this);
                    throw new System.Exception(msg);
                }
            }

            Vector3 originalPosition = lineRenderer.transform.position;
            lineRenderer.SetPositions(new Vector3[] { originalPosition, new Vector3(originalPosition.x, originalPosition.y, lineMaxDistance) });
            TurnLineToColor(lineRenderer, Color.green);
            SetLineWidth(lineRenderer, lineStartWidth);
        }

        float newXRotation = 0 , newYRotation = 0 ;
        void Update()
        {
            /*
             * If mouse input on rotate player to left or right, up or down.
             */
            var mouseInputX = "MouseX";

            Debug.Log($"X: {Input.GetAxis("Mouse X")}, Y: {Input.GetAxis("Mouse Y")}");
            if (Input.GetMouseButton(1))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                float tempYRotation = (MouseSensitivity * mouseY);
                float tempXRotation = (MouseSensitivity * mouseX);

                newXRotation = Mathf.Clamp(newXRotation - tempYRotation, -xRotationLimit, xRotationLimit);
                newYRotation = Mathf.Clamp(newYRotation + tempXRotation, -yRotationLimit, yRotationLimit);
                transform.localRotation = Quaternion.Euler(newXRotation, newYRotation, 0f);
                
            }


            /*
             * Set line render
             */
            //var x = lineRenderer.GetPosition(0);
            lineRenderer.SetPosition(1, lineRenderer.transform.forward * lineMaxDistance);
            //var y = lineRenderer.GetPosition(1);
            Debug.DrawLine(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), Color.red);

            /*
             * user can shoot again after x seconds
             * if x seconds has passed,else their shot means nothing
             */
            if (canShootAgain)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    canShootAgain = false;
                    TurnLineToColor(lineRenderer, Color.red);

                    /*
                     * if from line to forward for maxDistance raycat hits a collider 
                     * then draw a line from linerender origin to hit point
                    */
                    if (Physics.Raycast(lineRenderer.GetPosition(0), lineRenderer.transform.forward, out RaycastHit hitInfo, lineMaxDistance + 1))
                    {
                        if (hitInfo.collider.TryGetComponent<Enemy>(out Enemy enemy))
                        {
                            Debug.DrawLine(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));

                        }
                        Debug.Log(hitInfo.collider.gameObject.name);
                    }
                }

            }
            else
            {
                timer += Time.deltaTime;
                if (timer > maxTimerForShooting)
                {
                    canShootAgain = true;
                    timer = 0;
                    TurnLineToColor(lineRenderer, Color.green);
                }

            }


            //Debug.DrawLine(ray.origin, hit.point);
        }

        void TurnLineToColor(LineRenderer pLineRenderer, Color color)
        {
            ///Turns the LineRenderer Color to specified Color
            pLineRenderer.material.color = color;
        }

        void SetLineWidth(LineRenderer pLineRenderer, float lineWidth)
        {
            ///Sets the LineRenderer start and end widths to same specified width
            pLineRenderer.endWidth = lineWidth;
            pLineRenderer.startWidth = lineWidth;
        }

        void SetLineWidth(LineRenderer pLineRenderer, float lineEndWidth,float lineStartWidth)
        {
            ///Sets the LineRenderer start and end widths to individually specified widths
            pLineRenderer.startWidth = lineStartWidth;
            pLineRenderer.endWidth = lineEndWidth;
        }
    }
}