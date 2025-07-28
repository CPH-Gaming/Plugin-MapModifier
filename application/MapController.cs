namespace MapModifier;


/// <summary>
/// Provides map-specific logic and actions triggered during gameplay.
/// Responsible for detecting the current map, activating special map elements
/// (such as anti-camping zones), and notifying players via announcements.
/// Implements the singleton pattern to ensure only one shared instance exists.
/// </summary>
public class MapController
{
    // Used by the singleton and the double-checked locking pattern 
    private static MapController? _instance = null;
    private static readonly object _lock = new object();


    /// <summary>
    /// Initializes a new instance of the <see cref="MapController"/> class.
    /// The constructor is private to enforce the singleton pattern.
    /// </summary>
    private MapController()
    {
        
    }


    /// <summary>
    /// Returns the singleton instance of the <see cref="MapController"/>.
    /// Uses double-checked locking to ensure thread safety when initializing.
    /// </summary>
    /// <returns>The shared <see cref="MapController"/> instance.</returns>
    public static MapController GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new MapController();
                }
            }
        }

        return _instance;
    }



    /// <summary>
    /// Performs map-specific logic depending on the current map and round.
    /// Will only execute actions if the current round matches <paramref name="roundWhenInitiated"/>.
    /// Typical behavior includes sending announcements to all players about anti-camping zones soon activating.
    /// </summary>
    /// <param name="roundWhenInitiated">
    /// The round number during which the map-specific actions should occur.
    /// If the current round does not match, no actions are performed.
    /// </param>
    public void PerformEarlyMapSpecificAction(int roundWhenInitiated)
    {
        RoundController roundController = RoundController.GetInstance();

        if (roundController.GetCurrentRoundCounter() != roundWhenInitiated)
        {
            return;
        }

        // Gets the name of the current map and stores it within the variable
        string nameOfCurrentMap = NativeAPI.GetMapName();

        // If the current map is hoejhus92_remastered then execute this section
        if (nameOfCurrentMap == "hoejhus92_remastered")
        {
            SendEarlyAntiCampingNotice();

            return;
        }

        // If the current map is hoejhus92_orange then execute this section
        else if (nameOfCurrentMap == "hoejhus92_orange")
        {
            SendEarlyAntiCampingNoticeOrange();

            return;
        }

        // If the current map is hoejhus92_revive then execute this section
        else if (nameOfCurrentMap == "hoejhus92_revive")
        {
            SendEarlyAntiCampingNotice();

            return;
        }

        // If the current map is hoejhus92_prism then execute this section
        else if (nameOfCurrentMap == "hoejhus92_prism")
        {
            SendEarlyAntiCampingNotice();

            return;
        }

        // If the current map is hoejhus92_ascend then execute this section
        else if (nameOfCurrentMap == "hoejhus92_ascend")
        {
            SendEarlyAntiCampingNotice();

            return;
        }

        return;
    }



    /// <summary>
    /// Sends a global announcement to all players notifying them that
    /// the anti-camping zone at the terrorist’s roof is soon active.
    /// </summary>
    private void SendEarlyAntiCampingNotice()
    {
        AnnouncementController announcementController = AnnouncementController.GetInstance();
        announcementController.PrintToCenterHtmlAll("<font color='#FFFFFF' class='fontSize-m horizontal-center'>The anti-camping zone at the <font color='#34C0F7'>terrorist's roof</font> activates in 5 seconds.</font>");
    }


    /// <summary>
    /// Sends a global announcement to all players notifying them that
    /// the anti-camping zones at the terrorist’s roof and the flying box are soon active.
    /// Used specifically on hoejhus92_orange.
    /// </summary>
    private void SendEarlyAntiCampingNoticeOrange()
    {
        AnnouncementController announcementController = AnnouncementController.GetInstance();
        announcementController.PrintToCenterHtmlAll("<font color='#FFFFFF' class='fontSize-m horizontal-center'>The anti-camping zones at the <font color='#34C0F7'>terrorist's roof</font> and <font color='#34C0F7'>flying box</font> activates in 5 seconds.</font>");
    }



    /// <summary>
    /// Performs map-specific logic depending on the current map and round.
    /// Will only execute actions if the current round matches <paramref name="roundWhenInitiated"/>.
    /// Typical behavior includes activating trigger_hurt entities and sending
    /// announcements to all players about active anti-camping zones.
    /// </summary>
    /// <param name="roundWhenInitiated">
    /// The round number during which the map-specific actions should occur.
    /// If the current round does not match, no actions are performed.
    /// </param>
    public void PerformMapSpecificAction(int roundWhenInitiated)
    {
        RoundController roundController = RoundController.GetInstance();

        if (roundController.GetCurrentRoundCounter() != roundWhenInitiated)
        {
            return;
        }

        // Gets the name of the current map and stores it within the variable
        string nameOfCurrentMap = NativeAPI.GetMapName();

        // If the current map is hoejhus92_remastered then execute this section
        if (nameOfCurrentMap == "hoejhus92_remastered")
        {
            EntityController entityController = EntityController.GetInstance();
            entityController.FindTriggerHurtEntities();

            SendAntiCampingNotice();

            return;
        }

        // If the current map is hoejhus92_orange then execute this section
        else if (nameOfCurrentMap == "hoejhus92_orange")
        {
            EntityController entityController = EntityController.GetInstance();
            entityController.FindTriggerHurtEntities();

            SendAntiCampingNoticeOrange();

            return;
        }

        // If the current map is hoejhus92_revive then execute this section
        else if (nameOfCurrentMap == "hoejhus92_revive")
        {
            EntityController entityController = EntityController.GetInstance();
            entityController.FindTriggerHurtEntities();

            SendAntiCampingNotice();

            return;
        }

        // If the current map is hoejhus92_prism then execute this section
        else if (nameOfCurrentMap == "hoejhus92_prism")
        {
            EntityController entityController = EntityController.GetInstance();
            entityController.FindTriggerHurtEntities();

            SendAntiCampingNotice();

            return;
        }

        // If the current map is hoejhus92_ascend then execute this section
        else if (nameOfCurrentMap == "hoejhus92_ascend")
        {
            EntityController entityController = EntityController.GetInstance();
            entityController.FindTriggerHurtEntities();

            SendAntiCampingNotice();

            return;
        }

        return;
    }


    /// <summary>
    /// Sends a global announcement to all players notifying them that
    /// the anti-camping zone at the terrorist’s roof is now active.
    /// </summary>
    private void SendAntiCampingNotice()
    {
        AnnouncementController announcementController = AnnouncementController.GetInstance();
        announcementController.PrintToCenterHtmlAll("<font color='#FFFFFF' class='fontSize-m horizontal-center'>The anti-camping zone at the <font color='#34C0F7'>terrorist's roof</font> is now active.</font>");
    }


    /// <summary>
    /// Sends a global announcement to all players notifying them that
    /// the anti-camping zones at the terrorist’s roof and the flying box are now active.
    /// Used specifically on hoejhus92_orange.
    /// </summary>
    private void SendAntiCampingNoticeOrange()
    {
        AnnouncementController announcementController = AnnouncementController.GetInstance();
        announcementController.PrintToCenterHtmlAll("<font color='#FFFFFF' class='fontSize-m horizontal-center'>The anti-camping zones at the <font color='#34C0F7'>terrorist's roof</font> and <font color='#34C0F7'>flying box</font> is now active.</font>");
    }
}