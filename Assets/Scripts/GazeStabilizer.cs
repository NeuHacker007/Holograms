using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GazeStabilizer : MonoBehaviour
{
    [Tooltip("Number of samples to iterate on")]
    [Range(1, 120)]
    public int storedStabilitySamples = 60;

    public float PositionDropOffRadius = 0.02f;

    public float DirectionDropOffRadius = 0.1f;

    [Range(0.12f, 0.85f)]
    public float PositionStrength = 0.66f;

    [Range(0.25f, 0.85f)]
    public float DirectionStrength = 0.83f;

    public Vector3 StableHeadPosition { get; private set; }
    public Quaternion StableHeadRotation { get; private set; }

    public Ray StableHeadRay { get; private set; }
    public float StabilityAverageDistanceWeight = 2.0f;

    public float StabilityVarianceWeight = 1.0f;

    public struct GazeSample
    {
        public Vector3 position;
        public Vector3 direction;
        public float timestamp;
    }
    private Queue<GazeSample> stabilitySamples = new Queue<GazeSample>();
    private Vector3 gazePosition;
    private Vector3 gazeDirection;

    private float gazePositionInstability;
    private float gazeDirectionInstability;

    private const float positionDestabilizationFactor = 0.02f;
    private const float directionDestabilizationFactor = 0.3f;

    private Vector3 gravityWellPosition;
    private Vector3 gravityWellDirection;
    private bool isGravityPointExists = false;


    public void UpdateHeadStability(Vector3 position, Quaternion rotation)
    {
        gazePosition = position;
        gazeDirection = rotation * Vector3.forward;
        addGazeSample(gazePosition, gazeDirection);

        updateInstability(out gazePositionInstability, out gazeDirectionInstability);

        if (!isGravityPointExists)
        {
            gravityWellDirection = gazeDirection;
            gravityWellPosition = gazePosition;
            isGravityPointExists = true;
        }

        updateGravityWellPositionDirection();
    }

    private void addGazeSample(Vector3 positionSample, Vector3 directionSample)
    {
        GazeSample gazeSample;
        gazeSample.position = positionSample;
        gazeSample.direction = directionSample;
        gazeSample.timestamp = Time.time;

        if (stabilitySamples != null)
        {
            if (stabilitySamples.Count >= storedStabilitySamples)
            {
                stabilitySamples.Dequeue();
            }
            stabilitySamples.Enqueue(gazeSample);
        }
    }

    private void updateInstability(out float positionInstability, out float directionInstability)
    {
        GazeSample mostRecentSample;

        float positionDeltaMin = 0.0f;
        float positionDeltaMax = 0.0f;
        float positionDeltaMean = 0.0f;


        float directionDeltaMin = 0.0f;
        float directionDeltaMax = 0.0f;
        float directionDeltaMean = 0.0f;


        float positionDelta = 0.0f;
        float directionDelta = 0.0f;

        positionInstability = 0.0f;
        directionInstability = 0.0f;


        if (stabilitySamples.Count < 2)
        {
            return;
        }

        mostRecentSample = stabilitySamples.ElementAt(stabilitySamples.Count - 1);

        for (int i = 0; i < stabilitySamples.Count - 1; i++)
        {
            positionDelta = Vector3.Magnitude(stabilitySamples.ElementAt(i).position - mostRecentSample.position);
            directionDelta = Vector3.Angle(stabilitySamples.ElementAt(i).direction, mostRecentSample.direction);

            if (i == 0)
            {
                positionDeltaMin = positionDelta;
                positionDeltaMax = positionDelta;
                directionDeltaMin = directionDelta;
                directionDeltaMax = directionDelta;
            }
            else
            {
                /*
                 * This part will give us the minimized difference on each position and direction.
                 * This part will give us the minimized difference on each position and direction.
                 */
                positionDeltaMin = Mathf.Min(positionDelta, positionDeltaMin);
                positionDeltaMax = Mathf.Max(positionDelta, positionDeltaMax);


                directionDeltaMin = Mathf.Min(directionDelta, directionDeltaMin);
                directionDeltaMax = Mathf.Max(directionDelta, directionDeltaMax);
            }

            positionDeltaMean += positionDelta;
            directionDeltaMean += directionDelta;
        }

        positionDeltaMean = positionDeltaMean / (stabilitySamples.Count - 1);
        directionDeltaMean = directionDeltaMean / (stabilitySamples.Count - 1);


        //
        positionInstability = StabilityVarianceWeight * (positionDeltaMax - positionDeltaMin) +
                              StabilityAverageDistanceWeight * positionDeltaMean;
        directionInstability = StabilityVarianceWeight * (directionDeltaMax - directionDeltaMin) +
                               StabilityAverageDistanceWeight * directionDeltaMean;

    }

    private void updateGravityWellPositionDirection()
    {
        float stabilityModifiedPositionDropOffDistance;
        float stabilityModifiedDirectionDropOffDistance;
        float normalizedGazeToGravityWellPosition;
        float normalizedGazeToGravityWellDirection;

        stabilityModifiedPositionDropOffDistance = Mathf.Max(0.0f,
            (PositionDropOffRadius - (gazePositionInstability * positionDestabilizationFactor)));

        stabilityModifiedDirectionDropOffDistance = Mathf.Max(0.0f,
            DirectionDropOffRadius - (gazeDirectionInstability * directionDestabilizationFactor));

        normalizedGazeToGravityWellPosition = 2.0f;

        if (stabilityModifiedPositionDropOffDistance > 0.0f)
        {
            normalizedGazeToGravityWellPosition = Vector3.Magnitude(gravityWellPosition - gazePosition) /
                                                  stabilityModifiedPositionDropOffDistance;
        }

        normalizedGazeToGravityWellDirection = 2.0f;

        if (stabilityModifiedDirectionDropOffDistance > 0.0f)
        {
            normalizedGazeToGravityWellDirection = Mathf.Acos(Vector3.Dot(gravityWellDirection, gazeDirection)) /
                                                   stabilityModifiedDirectionDropOffDistance;
        }

        if (normalizedGazeToGravityWellPosition > 1.0f)
        {
            gravityWellPosition = gazePosition -
                                  Vector3.Normalize(gazePosition - gravityWellPosition) *
                                  stabilityModifiedPositionDropOffDistance;
        }

        if (normalizedGazeToGravityWellDirection > 1.0f)
        {
            gravityWellDirection =
                Vector3.Normalize(gazeDirection - Vector3.Normalize(gazeDirection - gravityWellDirection)) *
                stabilityModifiedDirectionDropOffDistance;

        }

        StableHeadPosition = Vector3.Lerp(gazePosition, gravityWellPosition, PositionStrength);
        StableHeadRotation =
            Quaternion.LookRotation(Vector3.Lerp(gazeDirection, gravityWellDirection, DirectionStrength));
        StableHeadRay = new Ray(StableHeadPosition, StableHeadRotation * Vector3.forward);
    }

}
