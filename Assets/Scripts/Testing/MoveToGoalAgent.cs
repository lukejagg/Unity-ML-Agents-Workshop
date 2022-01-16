using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float speed = 5f;

    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        float x = vectorAction[0];
        float z = vectorAction[1];

        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * speed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal");
        actionsOut[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetTransform)
        {
            SetReward(1f);
            EndEpisode();
        }
        else if (other.transform.name.Contains("Wall"))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}
