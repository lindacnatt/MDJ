using UnityEngine;

/// <summary>
/// All MonoBehaviours that control the spells should have this interface.
/// TODO: think of a slighty better way to do this?
/// </summary>
public interface ISpell
{
    /// <summary>
    /// Function to call when a spell is primed.
    /// This can be useful for spells that don't have any target (healer spells and so on)
    /// </summary>
    /// <returns>True if the spell was consumed or false otherwise</returns>
    bool OnSpellPrimed();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">World coordinates of the destination</param>
    void SetDestination(Vector2 target);
}