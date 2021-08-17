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

    private Renderer floorRd;
    private Material originMt;

    public Material goodMt, badMt;

    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        stageManager = tr.parent.GetComponent<StageManager>();
        floorRd = tr.parent.Find("Floor").GetComponent<Renderer>();
        originMt = floorRd.material;

        MaxStep = 2000;
    }

    IEnumerator RevertMaterial(Material changedMt)
    {
        floorRd.material = changedMt;
        yield return new WaitForSeconds(0.2f);
        floorRd.material = originMt;
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
        var action = actions.DiscreteActions;

        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        // Branch 0
        switch (action[0])
        {
            case 1: dir = tr.forward; break;
            case 2: dir = -tr.forward; break;
        }
        // Branch 1
        switch (action[1])
        {
            case 1: rot = -tr.up; break; //왼쪽 회전
            case 2: rot = tr.up; break;  //오른쪽 회전
        }

        tr.Rotate(rot, Time.fixedDeltaTime * turnSpeed);
        rb.AddForce(dir * moveSpeed, ForceMode.VelocityChange);

        // 마이너스 패널티
        AddReward(-1 / (float)MaxStep); // 2000 -> 0.002 
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;
        actions.Clear();

        // Branch 0 - 이동 (정지/전진/후진) 0, 1, 2 : Size 3
        if (Input.GetKey(KeyCode.W))
        {
            actions[0] = 1; //전진
        }
        if (Input.GetKey(KeyCode.S))
        {
            actions[0] = 2; //후진
        }
        // Branch 1 - 회전 (정지/왼쪽회전/오른쪽회전) 0, 1, 2 : Size 3
        if (Input.GetKey(KeyCode.A))
        {
            actions[1] = 1; //왼쪽 회전
        }
        if (Input.GetKey(KeyCode.D))
        {
            actions[1] = 2; //오른쪽 회전
        }
    }

}
