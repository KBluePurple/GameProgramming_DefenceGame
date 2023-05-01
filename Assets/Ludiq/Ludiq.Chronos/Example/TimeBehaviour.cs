using UnityEngine;

namespace Chronos.Example
{
    // A base class that provides a shortcut 
    // for accessing the timeline component.
    [RequireComponent(typeof(Timeline))]
    public abstract class TimeBehaviour : MonoBehaviour
    {
        public Timeline time => GetComponent<Timeline>();
    }
}