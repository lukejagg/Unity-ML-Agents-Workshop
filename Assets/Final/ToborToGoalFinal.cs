using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class ToborToGoalFinal : Agent
{
    public Transform targetTransform;
    public float speed;
    public Walls walls;
    
    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        
        // Set transforms to random position
        
        // Set Tobor to random position
        transform.localPosition = new Vector3(
            Random.Range(-5.5f, 4f),
            transform.localPosition.y,
            Random.Range(-6f, 3.5f));
        
        // Set Goal to random position
        targetTransform.localPosition = new Vector3(
            Random.Range(6.5f, 14f),
            targetTransform.localPosition.y,
            Random.Range(-6f, 3.5f));
        StartCoroutine(EndEpisodeAfter(5f));
    }

    private IEnumerator EndEpisodeAfter(float time)
    {
        yield return new WaitForSeconds(time);
        SetReward(-1f);
        EndEpisode();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    // Makes an action when decision is requested
    public override void OnActionReceived(ActionBuffers actions)
    {
        float x = actions.ContinuousActions[0];
        float z = actions.ContinuousActions[1];
        
        transform.position += (new Vector3(x, 0, z) * Time.deltaTime * speed);
    }
    
    // For manual input
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxisRaw("Horizontal");
        actions[1] = Input.GetAxisRaw("Vertical");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetTransform)
        {
            SetReward(10f);
            walls.SetWalls(true);
            StopAllCoroutines();
            EndEpisode();
        }
        else if (other.transform.name.Contains("Wall"))
        {
            SetReward(-1f);
            walls.SetWalls(false);
            StopAllCoroutines();
            EndEpisode();
        }
    }
}
