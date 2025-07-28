namespace MapModifier;


/// <summary>
/// Provides round-related logic and state tracking for the game.
/// Manages the current round count, retrieves game rules, and determines whether
/// the current round is in warmup mode.
/// Implements the singleton pattern to ensure only one shared instance is used.
/// </summary>
public class RoundController
{
    // Used by the singleton and the double-checked locking pattern 
    private static RoundController? _instance = null;
    private static readonly object _lock = new object();


    // Tracks the current round number in progress
    private int _currentRoundCounter = 0;


    /// <summary>
    /// Initializes a new instance of the <see cref="RoundController"/> class.
    /// Retrieves and stores the active game rules entity.
    /// </summary>
    private RoundController()
    {

    }


    /// <summary>
    /// Gets the singleton instance of the <see cref="RoundController"/>.
    /// Uses the double-checked locking pattern to ensure thread safety.
    /// </summary>
    /// <returns>The singleton <see cref="RoundController"/> instance.</returns>
    public static RoundController GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new RoundController();
                }
            }
        }

        return _instance;
    }


    /// <summary>
    /// Gets the current round counter.
    /// This counter tracks how many rounds have been started since the match began.
    /// </summary>
    /// <returns>An <see cref="int"/> representing the current round number.</returns>
    public int GetCurrentRoundCounter()
    {
        return this._currentRoundCounter;
    }


    /// <summary>
    /// Sets the current round counter to the specified value.
    /// Useful for manually incrementing or resetting the round number.
    /// </summary>
    /// <param name="roundCounter">
    /// The new round number to store.
    /// </param>
    public void SetCurrentRoundCounter(int roundCounter)
    {
        this._currentRoundCounter = roundCounter;
    }
}