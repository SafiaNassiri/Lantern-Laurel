using UnityEngine;

namespace GraveyardShift.Rituals
{
    /// <summary>
    /// Base class for the four ritual verbs: Chalk Circle, Salt Line, Dowsing Rod, Brew.
    /// </summary>
    public abstract class RitualVerb : MonoBehaviour
    {
        public abstract string VerbId { get; }

        /// <summary>Whether the player has unlocked this verb yet (via a grimoire page).</summary>
        public bool IsUnlocked { get; private set; }

        public void Unlock() => IsUnlocked = true;

        /// <summary>Called when the player begins performing this ritual.</summary>
        public abstract void BeginRitual();

        /// <summary>Called continuously while the player is performing it (tracing, swinging, etc).</summary>
        public abstract void UpdateRitual();

        /// <summary>Called when the player releases/finishes the input. Return true if successful.</summary>
        public abstract bool CompleteRitual();
    }
}