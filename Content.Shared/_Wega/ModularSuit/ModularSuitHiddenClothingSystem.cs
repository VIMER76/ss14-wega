using Content.Shared.Electrocution;
using Content.Shared.Inventory;

namespace Content.Shared.Modular.Suit;

/// <summary>
/// Use this if you need to transfer effects from clothing hidden under a suit.
/// </summary>
public sealed class ModularSuitHiddenClothingSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ModularSuitHiddenClothingComponent, InventoryRelayedEvent<ElectrocutionAttemptEvent>>(OnElectrocutionAttempt);
    }

    private void OnElectrocutionAttempt(Entity<ModularSuitHiddenClothingComponent> ent, ref InventoryRelayedEvent<ElectrocutionAttemptEvent> args)
    {
        foreach (var (_, item) in ent.Comp.HiddenItems)
        {
            if (TryComp<InsulatedComponent>(item, out var insulated))
                args.Args.SiemensCoefficient *= insulated.Coefficient;
        }
    }
}
