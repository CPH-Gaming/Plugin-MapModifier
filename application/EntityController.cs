namespace MapModifier;


/// <summary>
/// Handles logic for finding and managing in-map entities, such as 
/// trigger_hurt zones, and activating them when necessary.
/// Typically used for enabling map-based hazards or anti-camping mechanics.
/// Implements the singleton pattern to ensure a single shared instance.
/// </summary>
public class EntityController
{
    // Used by the singleton and the double-checked locking pattern 
    private static EntityController? _instance = null;
    private static readonly object _lock = new object();


    /// <summary>
    /// Initializes a new instance of the <see cref="EntityController"/> class.
    /// The constructor is private to enforce the singleton pattern.
    /// </summary>
    private EntityController()
    {
        
    }


    /// <summary>
    /// Retrieves the singleton instance of the <see cref="EntityController"/>.
    /// Uses double-checked locking to guarantee thread-safe initialization.
    /// </summary>
    /// <returns>The shared <see cref="EntityController"/> instance.</returns>
    public static EntityController GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new EntityController();
                }
            }
        }

        return _instance;
    }



    /// <summary>
    /// Searches the current map for all entities of type trigger_hurt.
    /// If an entity is valid and has the name "ManahCore Damage", the entity
    /// is activated by sending it an enable input.
    /// All other trigger_hurt entities are ignored.
    /// </summary>
    public void FindTriggerHurtEntities()
    {
        IEnumerable<CBaseEntity> listOfEntitites = Utilities.FindAllEntitiesByDesignerName<CBaseEntity>("trigger_hurt");

        foreach (CBaseEntity entity in listOfEntitites)
        {
            // if the entity is not a valid entity then execute this section
            if (!IsValid.Entity(entity))
            {
                continue;
            }

            // Retrieves the entity's name from the game engine
            string entityName = entity.Entity.Name;

            // Only activate the entity if it matches the specific target name
            if (entityName != "ManahCore Damage")
            {
                continue;
            }

            EnableTriggerHurt(entity);

            continue;
        }
    }


    /// <summary>
    /// Sends an enable input to the specified trigger_hurt entity,
    /// causing it to activate and start affecting players within its volume.
    /// </summary>
    /// <param name="entity">The <see cref="CBaseEntity"/> representing the trigger_hurt zone to enable.</param>
    private void EnableTriggerHurt(CBaseEntity entity)
    {
        entity.AcceptInput("enable");
    }
}