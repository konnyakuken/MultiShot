using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

using DG.Tweening;


public class AgentController : Agent
{
    public Rigidbody2D rBody;
    private float moveSpeed = 5.0f;
    private float rotateSpeed = 10.0f;
    Vector3 dir;

    ShotController shotController;
    bool isShot = false;


    void Start()
    {
        rBody = this.gameObject.GetComponent<Rigidbody2D>();
        GameObject child = transform.GetChild(1).gameObject;
        shotController = child.GetComponent<ShotController>();
    }


    public Transform Target;

    //シーンの初期化を行う（ランダムにターゲットを表示）
    public override void OnEpisodeBegin()
    {

        // Move the target to a new spot
        //Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    //環境の情報の収集を行う
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        //sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        //sensor.AddObservation(rBody.velocity.x);
        //sensor.AddObservation(rBody.velocity.z);
    }

    public float speedMultiplier = 10;

    //行った行動による報酬の決定など
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.y = actionBuffers.ContinuousActions[1];

        //回転させる角度
        float angle = controlSignal.x * rotateSpeed;
        transform.Rotate(new Vector3(0, 0, -angle));

        float angleDir = (transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
        dir = new Vector3(Mathf.Cos(angleDir), Mathf.Sin(angleDir), 0.0f);
        rBody.velocity = dir.normalized * moveSpeed * controlSignal.y;

        if (actionBuffers.ContinuousActions[2] == 1 && isShot == false)
        {
            isShot = true;
            DOVirtual.DelayedCall(0.3f, () => isShot = false);
            shotController.BulletShot();
        }

        // Rewards
        float distanceToTarget = 0;//Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            //SetReward(1.0f);
            //EndEpisode();
        }

        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            //EndEpisode();
        }
    }

    //Player操作時
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(0) && shotController.ShotGauge > 1)
        {
            continuousActionsOut[2] = 1;
            
        }
        else
        {
            continuousActionsOut[2] = 0;
        }
    }
}
