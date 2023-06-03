using Sandbox;
using Editor;
using System;

/// <summary>
/// A logic entity that stores and manipulates a numerical value.
/// It can perform simple mathematical operations such as addition, subtraction, multiplication, and division.
/// The counter can trigger outputs when it reaches user-defined maximum or minimum values, or output its value every time it changes.
/// When the MathCounter is disabled, it becomes read-only until re-enabled.
/// </summary>
[Library( "math_counter" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/math_counter.vmat" )]
[Title( "Math Counter" ), Category( "Logic" ), Icon( "calculate" )]
public partial class MathCounter : Entity
{
    // Keyvalues as properties
    [Property]
    public float StartValue { get; set; }

    [Property]
    public float min { get; set; }

    [Property]
    public float max { get; set; }

    [Property]
    public bool StartDisabled { get; set; }

    private float counterValue;
    private bool Enabled;

    public override void Spawn()
    {
        base.Spawn();

        counterValue = StartValue;
        Enabled = !StartDisabled;
    }

    // Inputs
    [Input]
    public void Add(float amount)
    {
        if (!Enabled) return;

        counterValue += amount;
        ClampAndFireOutputs();
    }

    [Input]
    public void Subtract(float amount)
    {
        if (!Enabled) return;

        counterValue -= amount;
        ClampAndFireOutputs();
    }

    [Input]
    public void Multiply(float amount)
    {
        if (!Enabled) return;

        counterValue *= amount;
        ClampAndFireOutputs();
    }

    [Input]
    public void Divide(float amount)
    {
        if (!Enabled || amount == 0) return;

        counterValue /= amount;
        ClampAndFireOutputs();
    }

    [Input]
    public void SetValue(float value)
    {
        if (!Enabled) return;

        counterValue = value;
        ClampAndFireOutputs();
    }

    [Input]
    public void SetValueNoFire(float value)
    {
        if (!Enabled) return;

        counterValue = value;
        ClampValue();
    }

    [Input]
    public void SetHitMax(float value)
    {
        if (!Enabled) return;

        max = value;
        ClampAndFireOutputs();
    }

    [Input]
    public void SetHitMin(float value)
    {
        if (!Enabled) return;

        min = value;
        ClampAndFireOutputs();
    }

    [Input]
    public void GetValue()
    {
        if (!Enabled) return;

        OnGetValue.Fire(this, counterValue);
    }

    [Input]
    public void SetMaxValueNoFire(float value)
    {
        if (!Enabled) return;

        max = value;
        ClampValue();
    }

    [Input]
    public void SetMinValueNoFire(float value)
    {
        if (!Enabled) return;

        min = value;
        ClampValue();
    }

    [Input]
    public void Enable()
    {
        Enabled = true;
    }

    [Input]
    public void Disable()
    {
        Enabled = false;
    }

    private void ClampValue()
    {
        counterValue = Math.Clamp(counterValue, min, max);
    }

    private void ClampAndFireOutputs()
    {
        ClampValue();
        OnOutValue.Fire(this, counterValue);

        if (counterValue <= min)
        {
            OnHitMin.Fire(this);
        }
        else if (counterValue >= max)
        {
            OnHitMax.Fire(this);
        }
    }

    // Outputs
    public Output<float> OnOutValue { get; set; }
    public Output OnHitMin { get; set; }
    public Output OnHitMax { get; set; }
    public Output<float> OnGetValue { get; set; }
}
