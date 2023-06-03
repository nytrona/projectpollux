using Sandbox;
using Editor;
using System;

/// <summary>
/// A logic entity that blends between two colors based on an input value.
/// </summary>
[Library( "math_colorblend" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[Title( "Color Blend" ), Category( "Logic" ), Icon( "palette" )]
public partial class MathColorBlend : Entity
{
	/// <summary>
    /// Input values below this value will be ignored.
    /// </summary>
    [Property, Title( "Minimum Valid Input Value" )]
    public float InMin { get; set; }

    /// <summary>
    /// Input values above this value will be ignored.
    /// </summary>
    [Property, Title( "Maximum Valid Input Value" )]
    public float InMax { get; set; }

    /// <summary>
    /// When the input value is equal to 'Minimum Valid Input Value', this is the output RGB color.
    /// </summary>
    [Property, Title( "Output RGB color when input is min" )]
    public Color ColorMin { get; set; }

    /// <summary>
    /// When the input value is equal to 'Maximum Valid Input Value', this is the output RGB color.
    /// </summary>
    [Property, Title( "Output RGB color when input is max" )]
    public Color ColorMax { get; set; }

    [Property, Title( "Ignore Out of Range Input Values" )]
    public bool IgnoreOutOfRange { get; set; }

    // Inputs
    [Input]
    public void InValue(Entity activator, float value)
    {
        if (IgnoreOutOfRange && (value < InMin || value > InMax))
        {
            return;
        }

        float t = Math.Clamp((value - InMin) / (InMax - InMin), 0, 1);
        Color blendedColor = Color.Lerp(ColorMin, ColorMax, t);

        OutColor.Fire(activator, blendedColor);
    }

    // Outputs
    public Output<Color> OutColor { get; set; }
}
