using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GraveyardShift.Chores
{
    /// <summary>
    /// Holds tonight's chore list and fires events when chores complete.
    /// </summary>
    public class ChoreManager : MonoBehaviour
    {
        public static ChoreManager Instance { get; private set; }

        [SerializeField] private List<ChoreTask> tonightsChores = new List<ChoreTask>();

        public UnityEvent<ChoreTask> onChoreCompleted;
        public UnityEvent onAllChoresCompleted;

        private readonly HashSet<string> _completedIds = new HashSet<string>();

        public IReadOnlyList<ChoreTask> TonightsChores => tonightsChores;

        private void Awake()
        {
            Instance = this;
        }

        public void CompleteChore(string choreId)
        {
            if (_completedIds.Contains(choreId)) return;

            ChoreTask chore = tonightsChores.Find(c => c.choreId == choreId);
            if (chore == null)
            {
                Debug.LogWarning($"ChoreManager: no chore with id '{choreId}' in tonight's list.");
                return;
            }

            _completedIds.Add(choreId);
            Debug.Log($"[ChoreManager] Completed: {chore.displayText}");
            onChoreCompleted?.Invoke(chore);

            if (_completedIds.Count >= tonightsChores.Count)
                onAllChoresCompleted?.Invoke();
        }

        public bool IsComplete(string choreId) => _completedIds.Contains(choreId);
    }
}