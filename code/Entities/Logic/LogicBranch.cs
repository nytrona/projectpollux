using Sandbox;
using Editor;
using System;

/// <summary>
/// Tests a boolean value and fires an output based on whether the value is true (one) or false (zero). Use this entity to branch between two potential sets of events.
/// </summary>
[Library( "logic_branch" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/logic_branch.vmat" )]
[Title( "Branch" ), Category( "Logic" ), Icon( "call_split" )]
public partial class LogicBranch : Entity
{
    /// <summary>
    /// Initial value for the boolean value (0 or 1).
    /// </summary>
    [Property( "InitialValue", Title = "Initial Value" )]
    public bool InitialValue { get; set; }

    public override void Spawn()
    {
        base.Spawn();

        // Set the initial value
        Value = InitialValue;
    }

    // The current boolean value
    private bool Value { get; set; }

    // Inputs
    [Input]
    public void SetValue(Entity activator, bool value)
    {
        // Set the boolean value without performing the comparison
        Value = value;
    }

    [Input]
    public void SetValueTest(Entity activator, bool value)
    {
        // Set the boolean value and test it, firing OnTrue or OnFalse based on the new value
        Value = value;
        Test(activator);
    }

    [Input]
    public void Toggle(Entity activator)
    {
        // Toggle the boolean value between true and false
        Value = !Value;
    }

    [Input]
    public void ToggleTest(Entity activator)
    {
        // Toggle the boolean value and test it, firing OnTrue or OnFalse based on the new value
        Value = !Value;
        Test(activator);
    }

    [Input]
    public void Test(Entity activator)
    {
        // Test the boolean value and fire OnTrue or OnFalse based on the value
        if (Value)
            OnTrue.Fire(activator);
        else
            OnFalse.Fire(activator);
    }

    // Outputs
    public Output OnTrue { get; set; }
    public Output OnFalse { get; set; }
}
