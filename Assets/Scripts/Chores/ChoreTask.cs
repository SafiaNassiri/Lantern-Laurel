using UnityEngine;

namespace GraveyardShift.Chores
{
    /// <summary>
    /// One chore definition. Create via Assets > Create > GraveyardShift > Chore Task. 
    /// </summary>
    [CreateAssetMenu(fileName = "NewChore", menuName = "GraveyardShift/Chore Task")]
    public class ChoreTask : ScriptableObject
    {
        [Header("Identity")]
        public string choreId;
        [TextArea] public string displayText; // e.g. "Sweep the east path"

        [Header("Supernatural Trigger (GDD 3.2)")]
        [Tooltip("Purely descriptive for designers — the actual link is wired via ChoreManager.onChoreCompleted in the scene, not here.")]
        public bool triggersSupernaturalEvent;
    }
}