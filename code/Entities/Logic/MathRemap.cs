using Sandbox;
using Editor;
using System;

/// <summary>
/// A logic entity that remaps a range of input values to a given range of output values.
/// </summary>
[Library( "math_remap" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[Title( "Remap" ), Category( "Logic" ), Icon( "calculate" )]
public partial class MathRemap : Entity
{
    private bool enabled = true;

    /// <summary>
    /// Input values below this value will be ignored.
    /// </summary>
    [Property( "in1", Title = "Minimum Valid Input Value" )]
    public float MinInputValue { get; set; }

    /// <summary>
    /// Input values above this value will be ignored.
    /// </summary>
    [Property( "in2", Title = "Maximum Valid Input Value" )]
    public float MaxInputValue { get; set; }

    /// <summary>
    /// When the input value is equal to "Minimum Valid Input Value", this is the output value.
    /// </summary>
    [Property( "out1", Title = "Output Value When Input Is Min.")]
    public float MinOutputValue { get; set; }

    /// <summary>
    /// When the input value is equal to "Maximum Valid Input Value", this is the output value.
    /// </summary>
    [Property( "out2", Title = "Output Value When Input Is Max.")]
    public float MaxOutputValue { get; set; }

    /// <summary>
    /// Whether the entity is initially disabled or not.
    /// </summary>
    [Property(Title = "Start Disabled")]
    public bool StartDisabled { get; set; }

    /// <summary>
    /// If set, input values that are outside the valid range will not fire the output.
    /// </summary>
    [Property(Title = "Ignore Out Of Range Input Values")]
    public bool IgnoreOutOfRangeInputValues { get; set; }

    /// <summary>
    /// If set, output values that are outside the output range will be clamped to the nearest boundary.
    /// </summary>
    [Property(Title = "Clamp Output To Output Range")]
    public bool ClampOutputToOutputRange { get; set; }

    public override void Spawn()
    {
        base.Spawn();

        if (StartDisabled)
        {
            this.enabled = false;
        }
    }

    // Inputs
    [Input]
    public void InValue(Entity activator, float value)
    {
        if (!enabled || value < MinInputValue || value > MaxInputValue)
        {
            if (IgnoreOutOfRangeInputValues)
            {
                return;
            }
            else
            {
                value = value < MinInputValue ? MinInputValue : MaxInputValue;
            }
        }

        float remappedValue = Remap(value, MinInputValue, MaxInputValue, MinOutputValue, MaxOutputValue);

        if (ClampOutputToOutputRange)
        {
            remappedValue = Math.Clamp(remappedValue, MinOutputValue, MaxOutputValue);
        }

        OutValue.Fire(activator, remappedValue);
    }

    [Input]
    public void Enable(Entity activator)
    {
        this.enabled = true;
    }

    [Input]
    public void Disable(Entity activator)
    {
        this.enabled = false;
    }

    // Outputs
    public Output<float> OutValue { get; set; }

    // Helper function to remap a value from one range to another
    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
