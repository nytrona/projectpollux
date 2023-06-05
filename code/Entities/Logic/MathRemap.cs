using Sandbox;
using Editor;
using System;

/// <summary>
/// Remaps a range of input values to a given range of output values.
/// </summary>
[Library( "math_remap" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/math_remap.vmat" )]
[Title( "Remap" ), Category( "Math" ), Icon( "calculate" )]
public partial class MathRemap : Entity
{
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
    [Property( "out1", Title = "Output Value When Input Is Min." )]
    public float OutputMinValue { get; set; }

    /// <summary>
    /// When the input value is equal to "Maximum Valid Input Value", this is the output value.
    /// </summary>
    [Property( "out2", Title = "Output Value When Input Is Max." )]
    public float OutputMaxValue { get; set; }

    /// <summary>
    /// Ignore out of range input values
    /// </summary>
    [Property( Title = "Ignore Out Of Range" )]
    public bool IgnoreOutOfRange { get; set; }

    /// <summary>
    /// Clamp output to output range
    /// </summary>
    [Property( Title = "Clamp Output" )]
    public bool ClampOutput { get; set; }

	// Inputs
	[Input]
	public void InValue(Entity activator, float value)
	{
		if (value < MinInputValue || value > MaxInputValue)
		{
			if (IgnoreOutOfRange) return;
			value = Math.Clamp(value, MinInputValue, MaxInputValue);
		}
		
		float normal = MathX.LerpInverse(MinInputValue, MaxInputValue, value);
		float output = MathX.Lerp(OutputMinValue, OutputMaxValue, normal);
		
		if (ClampOutput) output = Math.Clamp(output, OutputMinValue, OutputMaxValue);
		
		OnValueChanged.Fire(activator, output);
	}

	// Outputs
	public Output<float> OnValueChanged { get; set; }
}
