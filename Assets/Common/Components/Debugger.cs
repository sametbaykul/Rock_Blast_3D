using UnityEngine;

namespace Common.Components
{
    // --------------------------------------------------
    // Debugger.cs
    // --------------------------------------------------

    public class Debugger : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR
        // --------------------------------------------------


        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------


        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void MESSAGE(string message)
        {
            print("- > " + message);
        }

        public void WARNING(string warning_text)
        {
            print("! WARNING > " + warning_text);
        }

        public void ERROR(string text)
        {
            print("X ERROR > " + text);
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

    }
}
