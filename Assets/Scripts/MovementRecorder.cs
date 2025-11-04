using UnityEngine;
using System.Collections.Generic;

public class MovementRecorder : MonoBehaviour
{
    public float recordDuration = 5f;
    [SerializeField]
    private List<Vector3> positionHistory = new List<Vector3>();
    [SerializeField]
    private List<Quaternion> rotationHistory = new List<Quaternion>();
    private bool isRewinding = false;
    [SerializeField]
    private GameObject player;

    void Update()
    {
        if (isRewinding)
            return;

        RecordMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RewindMovement());
        }
    }

    void RecordMovement()
    {
        positionHistory.Add(player.transform.position);
        rotationHistory.Add(player.
            
            transform.rotation);

        // Limit history to last N seconds
        int maxFrames = Mathf.RoundToInt(recordDuration / Time.deltaTime);
        if (positionHistory.Count > maxFrames)
        {
            positionHistory.RemoveAt(0);
            rotationHistory.RemoveAt(0);
        }
    }

    IEnumerator<WaitForEndOfFrame> RewindMovement()
    {
        isRewinding = true;

        for (int i = positionHistory.Count - 1; i >= 0; i--)
        {
            player.transform.position = positionHistory[i];
            player.transform.rotation = rotationHistory[i];
            yield return new WaitForEndOfFrame();
        }

        isRewinding = false;
        positionHistory.Clear();
        rotationHistory.Clear();
    }
}
