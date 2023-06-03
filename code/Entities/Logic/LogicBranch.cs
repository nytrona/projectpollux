using Sandbox;
using Editor;

/// <summary>
/// A logic entity that stores and tests a true/false value and triggers different events based on the result.
/// </summary>
[Library( "logic_branch" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/logic_branch.vmat" )]
[Title( "Branch" ), Category( "Logic" ), Icon( "calculate" )]
public partial class LogicBranch : Entity
{
    // Keyvalues as properties
    [Property]
    public bool InitialValue { get; set; }

    private bool currentValue;

    public override void Spawn()
    {
        base.Spawn();

        currentValue = InitialValue;
    }

    // Inputs
    [Input]
    public void SetValue(Entity activator, bool value)
    {
        currentValue = value;
    }

    [Input]
    public void SetValueTest(Entity activator, bool value)
    {
        currentValue = value;
        Test(activator);
    }

    [Input]
    public void Toggle(Entity activator)
    {
        currentValue = !currentValue;
    }

    [Input]
    public void ToggleTest(Entity activator)
    {
        currentValue = !currentValue;
        Test(activator);
    }

    [Input]
    public void Test(Entity activator)
    {
        if (currentValue)
        {
            OnTrue.Fire(activator);
        }
        else
        {
            OnFalse.Fire(activator);
        }
    }

    // Outputs
    public Output OnTrue { get; set; }
    public Output OnFalse { get; set; }
}
