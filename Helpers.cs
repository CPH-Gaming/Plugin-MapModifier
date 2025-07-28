// Specifies the namespace of the plugin, this should match the name of the plugin file
namespace MapModifier;


/////////////////////////////
/// - Validation Classes - //
/////////////////////////////


/// <summary>
/// Provides static helper methods to validate Counter-Strike player and entity references.
/// </summary>
internal static class IsValid
{
    /// <summary>
    /// Validates that a player's index (unsigned integer) is within the allowed range [1,64].
    /// </summary>
    /// <param name="playerIndex">The player's index.</param>
    /// <returns><c>true</c> if the index is valid; otherwise, <c>false</c>.</returns>
	public static bool PlayerIndex(uint playerIndex)
	{
		// If the player's index is 0 then execute this section
		if (playerIndex == 0)
		{
			return false;
		}

		// If the client's index value is not within the range it should be then execute this section
		if (!(1 <= playerIndex && playerIndex <= 64))
		{
			return false;
		}

		return true;
	}


    /// <summary>
    /// Validates that a player's index (signed integer) is within the allowed range [1,64].
    /// </summary>
    /// <param name="playerIndex">The player's index.</param>
    /// <returns><c>true</c> if the index is valid; otherwise, <c>false</c>.</returns>
	public static bool PlayerIndex(int playerIndex)
	{
		// If the player's index is 0 then execute this section
		if (playerIndex == 0)
		{
			return false;
		}

		// If the client's index value is not within the range it should be then execute this section
		if (!(1 <= playerIndex && playerIndex <= 64))
		{
			return false;
		}

		return true;
	}


    /// <summary>
    /// Determines whether a specified <see cref="CCSPlayerController"/> is safe to manipulate.
    /// This checks that the player is connected, has a valid pawn, index, and userid, and is not HLTV.
    /// </summary>
    /// <param name="playerController">The player controller to validate.</param>
    /// <returns><c>true</c> if the player controller is valid; otherwise, <c>false</c>.</returns>
    public static bool PlayerController(CCSPlayerController playerController)
	{
		// If the CCSPlayerController is null then execute this section
		if (playerController == null)
		{
			return false;
		}

		// If the CCSPlayerController associated "handle" and "pointer" does not point to a valid entity then execute this section
		if (!playerController.IsValid)
		{
			return false;
		}

		// If the playerController's connection state is not perceived as "currently connected to server" then execute this section
		if (playerController.Connected != PlayerConnectedState.PlayerConnected)
		{
			return false;
		}

		// If the CCSPlayerController belongs to the HLTV then execute this section
		if (playerController.IsHLTV)
		{
			return false;
		}

		// If the playerController's associated 'pawn' is invalid then execute this section
		if (!playerController.PlayerPawn.IsValid)
		{
			return false;
		}

		// If the value of the entity instance this handle refers to is null then execute this section
		if (playerController.PlayerPawn.Value is null)
		{
			return false;
		}

		// If the playerController's associated 'client index' is invalid then execute this section
		if (!IsValid.PlayerIndex(playerController.Index))
		{
			return false;
		}

		// If the playerController's associated 'userid' is invalid then execute this section
		if (playerController.UserId == -1)
		{
			return false;
		}

		// If the playerController's associated 'userid' is '65535' which indicates already disconnected userids then execute this section
		// - Note this can still occur despite checking if the CCSPlayerController is connected.
		if (playerController.UserId == 65535)
		{
			return false;
		}

		return true;
	}


    /// <summary>
    /// Determines whether a specified <see cref="CEntityInstance"/> is safe to manipulate.
    /// This checks that the entity handle and pointer are valid, and that the index is non-zero.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
    public static bool Entity(CEntityInstance entity)
	{
		// If the CEntityInstance is null then execute this section
		if (entity == null)
		{
			return false;
		}

		// If the CEntityInstance associated "handle" and "pointer" does not point to a valid entity then execute this section
		if (!entity.IsValid)
		{
			return false;
		}

		// If the entity's index is invalid is invalid then execute this section
		if (entity.Index == 0)
		{
			return false;
		}

		return true;
	}
}