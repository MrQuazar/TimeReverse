using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRecorder : MonoBehaviour
{
    public float recordDuration = 5f;
    private List<Vector3> positionHistory = new List<Vector3>();
    private List<Quaternion> rotationHistory = new List<Quaternion>();
    private List<Quaternion> cameraRotationHistory = new List<Quaternion>();
    private bool isRewinding = false;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject myCamera;

    void Update()
    {
        if (!isRewinding && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RewindMovement());
        }

        if (!isRewinding)
        {
            RecordMovement();
        }
    }

    void RecordMovement()
    {
        positionHistory.Add(player.transform.position);
        rotationHistory.Add(player.transform.rotation);
        cameraRotationHistory.Add(myCamera.transform.localRotation);

        // Limit history to last N seconds
        int maxFrames = Mathf.RoundToInt(recordDuration / Time.deltaTime);
        if (positionHistory.Count > maxFrames)
        {
            positionHistory.RemoveAt(0);
            rotationHistory.RemoveAt(0);
            cameraRotationHistory.RemoveAt(0);
        }
    }

    IEnumerator RewindMovement()
    {
        isRewinding = true;

        while (Input.GetKey(KeyCode.Space) && positionHistory.Count > 0)
        {
            int lastIndex = positionHistory.Count - 1;

            player.transform.position = positionHistory[lastIndex];
            player.transform.rotation = rotationHistory[lastIndex];
            myCamera.transform.localRotation = cameraRotationHistory[lastIndex];

            positionHistory.RemoveAt(lastIndex);
            rotationHistory.RemoveAt(lastIndex);
            cameraRotationHistory.RemoveAt(lastIndex);

            yield return new WaitForEndOfFrame();
        }

        isRewinding = false;

    }
}