using Sandbox;
using Editor;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// A logic entity that compares an input value with up to 16 predefined cases. If the input matches a case, it triggers a corresponding output. If there is no match, it triggers a default output.
/// </summary>
[Library( "logic_case" )]
[HammerEntity]
[VisGroup( VisGroup.Logic )]
[EditorSprite( "editor/logic_case.vmat" )]
[Title( "Case" ), Category( "Logic" ), Icon( "calculate" )]
public partial class LogicCase : Entity
{
    // Keyvalues as properties
    [Property]
    public string Case01 { get; set; }

    [Property]
    public string Case02 { get; set; }

	[Property]
    public string Case03 { get; set; }

	[Property]
    public string Case04 { get; set; }

    [Property]
    public string Case05 { get; set; }

	[Property]
    public string Case06 { get; set; }

	[Property]
    public string Case07 { get; set; }

    [Property]
    public string Case08 { get; set; }

	[Property]
    public string Case09 { get; set; }

	[Property]
    public string Case10 { get; set; }

    [Property]
    public string Case11 { get; set; }

	[Property]
    public string Case12 { get; set; }

	[Property]
    public string Case13 { get; set; }

    [Property]
    public string Case14 { get; set; }

	[Property]
    public string Case15 { get; set; }

	[Property]
    public string Case16 { get; set; }

    // List for storing cases
    private List<string> caseList = new List<string>();

    public override void Spawn()
    {
        base.Spawn();

        // Add cases to list for easier management
        caseList.Add(Case01);
        caseList.Add(Case02);
        caseList.Add(Case03);
        caseList.Add(Case04);
        caseList.Add(Case05);
        caseList.Add(Case06);
        caseList.Add(Case07);
        caseList.Add(Case08);
        caseList.Add(Case09);
        caseList.Add(Case10);
        caseList.Add(Case11);
        caseList.Add(Case12);
        caseList.Add(Case13);
        caseList.Add(Case14);
        caseList.Add(Case15);
        caseList.Add(Case16);
    }

    // Inputs
    [Input]
    public void InValue(Entity activator, string value)
    {
        for (int i = 0; i < caseList.Count; i++)
        {
            if (caseList[i] == value)
            {
                FireOutput(i);
                return;
            }
        }
        OnDefault.Fire(activator);
    }

    private static readonly Random rand = new Random();

    [Input]
    public void PickRandom(Entity activator)
    {
        var randomIndex = rand.Next(caseList.Count);
        FireOutput(randomIndex);
    }

    [Input]
    public void PickRandomShuffle(Entity activator)
    {
        // Shuffling the caseList to provide a random output without repeats
        caseList = caseList.OrderBy(x => rand.Next()).ToList();
        FireOutput(0); // Picking the first one after shuffle
    }


    private void FireOutput(int index)
    {
        switch(index)
        {
            case 0: OnCase01.Fire(this); break;
            case 1: OnCase02.Fire(this); break;
            case 2: OnCase03.Fire(this); break;
            case 3: OnCase04.Fire(this); break;
            case 4: OnCase05.Fire(this); break;
            case 5: OnCase06.Fire(this); break;
            case 6: OnCase07.Fire(this); break;
            case 7: OnCase08.Fire(this); break;
            case 8: OnCase09.Fire(this); break;
            case 9: OnCase10.Fire(this); break;
            case 10: OnCase11.Fire(this); break;
            case 11: OnCase12.Fire(this); break;
            case 12: OnCase13.Fire(this); break;
            case 13: OnCase14.Fire(this); break;
            case 14: OnCase15.Fire(this); break;
            case 15: OnCase16.Fire(this); break;
        }
        OnUsed.Fire(this);
    }

    // Outputs
    public Output OnCase01 { get; set; }
    public Output OnCase02 { get; set; }
    public Output OnCase03 { get; set; }
    public Output OnCase04 { get; set; }
    public Output OnCase05 { get; set; }
    public Output OnCase06 { get; set; }
    public Output OnCase07 { get; set; }
    public Output OnCase08 { get; set; }
    public Output OnCase09 { get; set; }
    public Output OnCase10 { get; set; }
    public Output OnCase11 { get; set; }
    public Output OnCase12 { get; set; }
    public Output OnCase13 { get; set; }
    public Output OnCase14 { get; set; }
    public Output OnCase15 { get; set; }
    public Output OnCase16 { get; set; }

    public Output OnDefault { get; set; }
    public Output OnUsed { get; set; }
}
