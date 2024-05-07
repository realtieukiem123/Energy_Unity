﻿#if PLAYMAKER

using System.Collections;
using HutongGames.PlayMaker;

namespace Micosmo.SensorToolkit.PlayMaker {

    [ActionCategory("SensorToolkit")]
    [Tooltip("For a valid raycasting sensor this will retrieve the ray intersection details for a detected object. Works with the Ray Sensor, Arc Sensor and their 2D analogues.")]
    public class SensorGetDetectionRayHit : FsmStateAction {

        [RequiredField]
        [ObjectType(typeof(BasePulsableSensor))]
        public FsmObject sensor;

        [ActionSection("Inputs")]

        [RequiredField]
        [Tooltip("The object to retrieve the RayHit for")]
        public FsmGameObject targetObject;

        [Tooltip("Run each frame?")]
        public bool everyFrame;

        [ActionSection("Outputs")]

        [UIHint(UIHint.Variable)]
        [Tooltip("Stores the point where the ray intersected the object")]
        public FsmVector3 storePoint;

        [UIHint(UIHint.Variable)]
        [Tooltip("Stores the normal vector at the interesection point")]
        public FsmVector3 storeNormal;

        [UIHint(UIHint.Variable)]
        [Tooltip("Stores the distance travelled by the ray before intersecting the object")]
        public FsmFloat storeDistance;

        [UIHint(UIHint.Variable)]
        [Tooltip("Stores the fraction of the ray's length travelled before intersecting the object")]
        public FsmFloat storeDistanceFraction;

        [ActionSection("Events")]

        [Tooltip("Invoked if the targetObject is detected")]
        public FsmEvent isIntersectedEvent;

        [Tooltip("Invoked if the targetObject is not detected")]
        public FsmEvent isNotIntersectedEvent;

        IRayCastingSensor _sensor => sensor.Value as IRayCastingSensor;
        UnityEngine.GameObject _targetObject => targetObject.Value;

        public override void Reset() {
            OwnerDefaultSensor();
            targetObject = null;
            storePoint = null;
            storeNormal = null;
            storeDistance = null;
            storeDistanceFraction = null;
            isIntersectedEvent = null;
            isNotIntersectedEvent = null;
            everyFrame = false;
        }

        void OwnerDefaultSensor() {
            if (Owner != null) {
                sensor = new FsmObject() { Value = Owner.GetComponent(typeof(IRayCastingSensor)) };
            } else {
                sensor = null;
            }
        }

        public override string ErrorCheck() {
            if (sensor.Value != null && _sensor == null) {
                return "'Sensor' is not Ray Casting. Must implement IRayCastingSensor";
            }
            return base.ErrorCheck();
        }

        public override void OnEnter() {
            DoAction();
            if (!everyFrame) {
                Finish();
            }
        }

        public override void OnUpdate() {
            DoAction();
        }

        void DoAction() {
            if (_sensor == null || _targetObject == null) {
                return;
            }
            var hit = _sensor.GetDetectionRayHit(_targetObject);
            storePoint.Value = hit.Point;
            storeNormal.Value = hit.Normal;
            storeDistance.Value = hit.Distance;
            storeDistanceFraction.Value = hit.DistanceFraction;
            if (hit.Equals(RayHit.None)) {
                Fsm.Event(isNotIntersectedEvent);
            } else {
                Fsm.Event(isIntersectedEvent);
            }
        }
    }

}

#endif