using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MummyAgent : Agent
{
    private Transform tr;
    private Rigidbody rb;

    public float moveSpeed = 1.5f;
    public float turnSpeed = 200.0f;

    private StageManager stageManager;

    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        stageManager = tr.parent.GetComponent<StageManager>();

        MaxStep = 50;
    }

    public override void OnEpisodeBegin()
    {
        stageManager.InitStage();

        rb.velocity = rb.angularVelocity = Vector3.zero;

        tr.localPosition = new Vector3(0, 0.05f, -5.0f);
        tr.localRotation = Quaternion.identity;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
    }

}
