using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.UI
{

    public class UIController : MonoBehaviour
    {
        public DynamicJoystick DynamicJoystick;

        [Inject]
        public void Init(MovePlayer movePlayer)
        {
            movePlayer.BindDynamicJoystick(DynamicJoystick);
        }
    }
}

