using Unity.Entities;
using UnityEngine;

public class SpeedAuthoring : MonoBehaviour
{
    public float Value;
}

// A baker connects the Authoring class with the component
// Converts the class to the component
// So once baked: SpeedAuthoring->Speed(Component)
// N.B! Any components we want on the entity, need to be backed on them!!
public class SpeedBaker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        // Adds a new component
        // Grants the value from the authoring class and assigns it to the component
        AddComponent(new Speed
        {
            Value = authoring.Value
        });
    }
}
