using Sandbox;
using Editor;
using System;

/// <summary>
/// Stores and manipulates a numerical value. It can trigger on reaching user-defined maximum or minimum values, or output its value every time it changes. It also has the ability to perform simple mathematical functions.
/// </summary>
[Library( "math_counter" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/math_counter.vmat" )]
[Title( "Counter" ), Category( "Math" ), Icon( "calculate" )]
public partial class MathCounter : Entity
{
    /// <summary>
    /// Starting value for the counter.
    /// </summary>
    [Property, Title( "Initial Value" )]
    public float StartValue { get; set; }

    /// <summary>
    /// Minimum legal value for the counter. If min=0 and max=0, no clamping is performed.
    /// </summary>
    [Property, Title( "Minimum Legal Value" )]
    public float Min { get; set; }

    /// <summary>
    /// Maximum legal value for the counter. If min=0 and max=0, no clamping is performed.
    /// </summary>
    [Property, Title( "Maximum Legal Value" )]
    public float Max { get; set; }

    private float currentValue;

    public override void Spawn()
    {
        base.Spawn();

        currentValue = StartValue;
        ClampValue();
    }

    private void ClampValue()
    {
        if ( Min != 0 || Max != 0 )
        {
            currentValue = Math.Clamp( currentValue, Min, Max );
        }
    }

    // Inputs
    [Input]
    public void Add( Entity activator, float amount )
    {
        currentValue += amount;
        ClampValue();
        OutValue.Fire( activator, currentValue );
    }

    [Input]
    public void Divide( Entity activator, float amount )
    {
        if ( amount != 0 )
        {
            currentValue /= amount;
            ClampValue();
            OutValue.Fire( activator, currentValue );
        }
    }

    [Input]
    public void Multiply( Entity activator, float amount )
    {
        currentValue *= amount;
        ClampValue();
        OutValue.Fire( activator, currentValue );
    }

    [Input]
    public void SetValue( Entity activator, float value )
    {
        currentValue = value;
        ClampValue();
        OutValue.Fire( activator, currentValue );
    }

    [Input]
    public void SetValueNoFire( Entity activator, float value )
    {
        currentValue = value;
        ClampValue();
    }

    [Input]
    public void Subtract( Entity activator, float amount )
    {
        currentValue -= amount;
        ClampValue();
        OutValue.Fire( activator, currentValue );
    }

    [Input]
    public void SetHitMax( Entity activator )
    {
        Max = currentValue;
        OutValue.Fire( activator, currentValue );
    }

    [Input]
    public void SetHitMin( Entity activator )
    {
        Min = currentValue;
        OutValue.Fire( activator, currentValue );
    }

	[Input]
	public void GetValue(Entity activator)
	{
		OnGetValue.Fire(activator,currentValue);
	}

	[Input]
	public void SetMaxValueNoFire(Entity activator)
	{
		Max = currentValue;
	}

	[Input]
	public void SetMinValueNoFire(Entity activator)
	{
		Min = currentValue;
	}

	// Outputs
	public Output<float> OutValue { get; set; }
	public Output OnHitMin { get; set; }
	public Output OnHitMax { get; set; }
	public Output<float> OnGetValue { get; set; }
}
