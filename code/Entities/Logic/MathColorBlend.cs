using Sandbox;
using Editor;
using System;

/// <summary>
/// Blends between two colors based on an input value and outputs the result.
/// </summary>
[Library( "math_colorblend" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/math_colorblend.vmat" )]
[Title( "Color Blend" ), Category( "Math" ), Icon( "palette" )]
public partial class MathColorBlend : Entity
{
    /// <summary>
    /// Input values below this value will be ignored.
    /// </summary>
    [Property( "inmin", Title = "Minimum Valid Input Value" )]
    public float MinInputValue { get; set; } = 0f;

    /// <summary>
    /// Input values above this value will be ignored.
    /// </summary>
    [Property( "inmax", Title = "Maximum Valid Input Value" )]
    public float MaxInputValue { get; set; } = 1f;

    /// <summary>
    /// Output RGB color when input is min.
    /// </summary>
    [Property( "colormin", Title = "Output RGB Color When Input Is Min" )]
    public Color ColorMin { get; set; } = Color.Black;

    /// <summary>
    /// Output RGB color when input is max.
    /// </summary>
    [Property( "colormax", Title = "Output RGB Color When Input Is Max" )]
    public Color ColorMax { get; set; } = Color.White;

    /// <summary>
    /// If enabled, input values outside the valid range will be ignored. If disabled, they will be clamped to the nearest valid value.
    /// </summary>
    [Property( "ignoreoutofrange", Title = "Ignore Out Of Range Input Values" )]
    public bool IgnoreOutOfRange { get; set; } = true;

    // Inputs
    [Input]
    public void InValue(Entity activator, float value)
    {
        if (IgnoreOutOfRange && (value < MinInputValue || value > MaxInputValue)) return;
        var t = MathX.Clamp((value - MinInputValue) / (MaxInputValue - MinInputValue), 0f, 1f);
        var color = Color.Lerp(ColorMin, ColorMax, t);
        OutColor.Fire(activator, color);
    }

    // Outputs
    public Output<Color> OutColor { get; set; }
}
