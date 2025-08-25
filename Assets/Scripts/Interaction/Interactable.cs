using UnityEngine;

namespace Interaction
{
    /// <summary>
    /// Base class for all interactable objects in the world.  Provides a
    /// prompt that can be displayed to the player and exposes the
    /// <see cref="Interact"/> method to perform the specific action.
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Text to display when the player can interact with this object.")]
        private string prompt = "Press E";

        /// <summary>
        /// Prompt text for UI display when the player is inside the trigger.
        /// </summary>
        public string Prompt => prompt;

        /// <summary>
        /// Perform the object's interaction behaviour. Override in subclasses
        /// to implement custom logic.
        /// </summary>
        public abstract void Interact();
    }
}
