using System.Collections.Generic;
using UnityEngine;

namespace GraveyardShift.Rituals
{
    /// <summary>
    /// The first ritual verb taught to the player.
    /// Places a line of salt points while the player holds DrawRitual and moves; used to ward/contain/redirect spirits. 
    /// </summary>
    public class SaltLineRitual : RitualVerb
    {
        [SerializeField] private float minPointDistance = 0.35f;
        [SerializeField] private GameObject saltPointPrefab;

        public override string VerbId => "salt_line";

        private readonly List<Vector3> _points = new List<Vector3>();
        private bool _isDrawing;

        public override void BeginRitual()
        {
            if (!IsUnlocked) return;
            _isDrawing = true;
            _points.Clear();

            Debug.Log("[SaltLineRitual] BeginRitual called — starting salt line.");
        }

        public override void UpdateRitual()
        {
            if (!_isDrawing) return;

            Vector3 currentPos = transform.position; // replace with hand/reticle position later
            if (_points.Count == 0 || Vector3.Distance(_points[^1], currentPos) >= minPointDistance)
            {
                _points.Add(currentPos);
                if (saltPointPrefab != null)
                    Instantiate(saltPointPrefab, currentPos, Quaternion.identity, transform);
            }
        }

        public override bool CompleteRitual()
        {
            _isDrawing = false;
            bool success = _points.Count >= 3;
            Debug.Log($"[SaltLineRitual] Completed with {_points.Count} points — success: {success}");
            return success;
        }
    }
}