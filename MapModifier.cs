//////////////
// - CORE - // 
//////////////

// Specifies the namespace of the plugin, this should match the name of the plugin file
namespace MapModifier;


/// <summary>
/// Main plugin class implementing automatic bomb planting for the Retakes game mode.
/// Handles lifecycle events, game event registration, and core round logic.
/// </summary>
public partial class RetakesInstantPlant : BasePlugin
{
    // The retrievable information about the plugin itself
    public override string ModuleName => "[Custom] Map Modifier (Hoejhus92)";
    public override string ModuleAuthor => "Manifest @Road To Glory";
    public override string ModuleDescription => "Improves the gameplay experience by improving the maps through slight enhancements.";
    public override string ModuleVersion => "V. 1.0.0 [Beta]";



    /// <summary>
    /// Called when the plugin is loaded.
    /// Registers all necessary game events.
    /// </summary>
    /// <param name="hotReload">Indicates if the plugin is reloaded without server restart.</param>
    public override void Load(bool hotReload)
    {
        // Registers and subscribes to the game events we intend to use
        RegisterEvents();
    }


    /////////////////////
    // - Game Events - //
    /////////////////////

    /// <summary>
    /// Registers game event handlers.
    /// </summary>
    private void RegisterEvents()
    {
        // Registers and hooks in to the game events we intend to use
        RegisterEventHandler<EventRoundStart>(Event_RoundStart, HookMode.Post);
        RegisterEventHandler<EventRoundFreezeEnd>(Event_RoundFreezeEnd, HookMode.Post);
    }


    /// <summary>
    /// Event handler invoked when a new round starts.
    /// Sets server configuration to prevent dropped C4 on death.
    /// </summary>
    /// <param name="event">The round start event data.</param>
    /// <param name="info">Additional information about the game event.</param>
    /// <returns>A <see cref="HookResult"/> indicating how to continue event processing.</returns>
    public HookResult Event_RoundStart(EventRoundStart @event, GameEventInfo info)
    {
        RoundController roundController = RoundController.GetInstance();
        roundController.SetCurrentRoundCounter(roundController.GetCurrentRoundCounter() + 1);

        return HookResult.Continue;
    }


    /// <summary>
    /// Event handler invoked when the freeze time expires and the round fully begins for players.
    /// If not a warmup round, starts the auto-bomb planting process.
    /// </summary>
    /// <param name="event">The freeze end event data.</param>
    /// <param name="info">Additional information about the game event.</param>
    /// <returns>A <see cref="HookResult"/> indicating how to continue event processing.</returns>
    public HookResult Event_RoundFreezeEnd(EventRoundFreezeEnd @event, GameEventInfo info)
    {
        RoundController roundController = RoundController.GetInstance();
        int roundWhenInitiated = roundController.GetCurrentRoundCounter();

        AddTimer(30.0f, () =>
        {
            MapController mapController = MapController.GetInstance();
            mapController.PerformMapSpecificAction(roundWhenInitiated);
        }, TimerFlags.STOP_ON_MAPCHANGE);

        return HookResult.Continue;
    }
}