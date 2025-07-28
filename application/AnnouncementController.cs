namespace MapModifier;


/// <summary>
/// Manages the display of center-screen announcements to players,
/// using styled HTML formatting for headings and messages.
/// Implements the singleton pattern to ensure only one global announcer instance.
/// </summary>
public class AnnouncementController
{
    // Used by the singleton and the double-checked locking pattern 
    private static AnnouncementController? _instance = null;
    private static readonly object _lock = new object();

    // Stores the heading and paragraph used in announcements
    private string _messageHeading = string.Empty;
    private string _messageParagraph = string.Empty;


    /// <summary>
    /// Initializes a new instance of the <see cref="AnnouncementController"/> class.
    /// Sets a default heading style for all announcements.
    /// Private to enforce the singleton pattern.
    /// </summary>
    private AnnouncementController()
    {
        _messageHeading = "<font color='#FFA500' class='fontSize-ml horizontal-center'> <b>Game Announcer</b></font>";
        _messageParagraph = string.Empty;
    }


    /// <summary>
    /// Retrieves the singleton instance of the <see cref="AnnouncementController"/>.
    /// Uses double-checked locking to ensure thread-safe initialization.
    /// </summary>
    /// <returns>The shared <see cref="AnnouncementController"/> instance.</returns>
    public static AnnouncementController GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new AnnouncementController();
                }
            }
        }

        return _instance;
    }


    /// <summary>
    /// Sends a multi-line styled hint message to all players aside from the specified player using HTML-style formatting.
    /// The first line will be bold and orange, and the second line will be regular white text.
    /// </summary>
    /// <param name="message">The second line of the message, shown in normal white text.</param>
    public void PrintToCenterHtmlAll(string message)
    {
        foreach (CCSPlayerController player in Utilities.GetPlayers())
        {
            // If the 'CCSPlayerController' does not meet the criteria for allowing safe manipulation of itself and its associated 'pawn', 'index' and 'userid' then execute this section
            if (!IsValid.PlayerController(player))
            {
                continue;
            }

            if (player.IsBot)
            {
                continue;
            }

            SetMessageParagraph(message);

            string fullMessage = GetMessageHeading() + "<br>" + GetMessageParagraph();

            player.PrintToCenterHtml(fullMessage, 5, false);

            break;
        }
    }


    /// <summary>
    /// Gets the current HTML-styled heading used for all announcements.
    /// </summary>
    /// <returns>The heading as an HTML string.</returns>
    private string GetMessageHeading()
    {
        return this._messageHeading;
    }


    /// <summary>
    /// Gets the current HTML-styled message paragraph, the second line of announcements.
    /// </summary>
    /// <returns>The message paragraph as an HTML string.</returns>
    private string GetMessageParagraph()
    {
        return this._messageParagraph;
    }


    /// <summary>
    /// Gets the current HTML-styled message paragraph, the second line of announcements.
    /// </summary>
    /// <returns>The message paragraph as an HTML string.</returns>
    private void SetMessageParagraph(string messageParagraph)
    {
        this._messageParagraph = messageParagraph;
    }
}