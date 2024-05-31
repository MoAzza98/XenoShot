using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public static class AnimationRiggingExtensions {

    public static void SetConstrained(this MultiAimConstraint constraint, Transform obj) {
        constraint.data.constrainedObject = obj;
    }

    public static void SetSource(this MultiAimConstraint constraint, Transform obj) {
        WeightedTransformArray array = new WeightedTransformArray();
        array.Add(new WeightedTransform(obj, 1));
        constraint.data.sourceObjects = array;
    }

    public static void SetConstrained(this MultiPositionConstraint constraint, Transform obj) {
        constraint.data.constrainedObject = obj;
    }

    public static void SetSource(this MultiPositionConstraint constraint, Transform obj) {
        WeightedTransformArray array = new WeightedTransformArray();
        array.Add(new WeightedTransform(obj, 1));
        constraint.data.sourceObjects = array;
    }

    public static void SetConstrained(this MultiParentConstraint constraint, Transform obj) {
        constraint.data.constrainedObject = obj;
    }

    public static void SetSource(this MultiParentConstraint constraint, Transform obj) {
        WeightedTransformArray array = new WeightedTransformArray();
        array.Add(new WeightedTransform(obj, 1));
        constraint.data.sourceObjects = array;
    }

    public static void SetConstrained(this OverrideTransform constraint, Transform obj) {
        constraint.data.constrainedObject = obj;
    }

    public static void SetSource(this OverrideTransform constraint, Transform obj) {
        constraint.data.sourceObject = obj;
    }
}
