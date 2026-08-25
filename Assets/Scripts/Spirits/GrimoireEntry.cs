using UnityEngine;

namespace GraveyardShift.Grimoire
{
    public enum GrimoireEntryType
    {
        RitualRecipe,
        SpiritFragment,
        ChoreLogNote
    }

    /// <summary>
    /// One page of the grimoire. The grimoire is the diegetic pause menu, quest log, and recipe book
    /// </summary>
    [CreateAssetMenu(fileName = "NewGrimoirePage", menuName = "GraveyardShift/Grimoire Entry")]
    public class GrimoireEntry : ScriptableObject
    {
        public string entryId;
        public GrimoireEntryType entryType;
        [TextArea(3, 10)] public string pageText;

        [Header("If RitualRecipe")]
        [Tooltip("Matches RitualVerb.VerbId on the verb this page unlocks (e.g. 'salt_line').")]
        public string unlocksRitualVerbId;
        public Sprite diagramImage;
    }
}