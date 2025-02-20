using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN.ShooterAssessment.ScriptTags
{
    /// <summary>
    /// Game Views essentially define each screen. They have a reference to the next Game View.
    /// </summary>
    public class GameView : MonoBehaviour
    {
        public GameView Next;

        public GameView SwitchNext()
        {
            TurnOff();
            Next.TurnOn();
            return Next;
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}
