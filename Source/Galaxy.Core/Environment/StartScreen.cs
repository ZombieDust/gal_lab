#region using

using System;

#endregion

namespace Galaxy.Core.Environment
{
    public class StartScreen : BaseLevel
    {
        public StartScreen()
        {
            FileName = @"Assets\StartScreen.png";
        }

        #region public properties

        public static Type LevelOne { get; set; }
        public static Type LevelTwo { get; set; }

        #endregion

        #region Overrides of BaseLevel

        public override void Update()
        {
            base.Update();

            if (IsPressed(VirtualKeyStates.Numpad1))
            {
                Activator.CreateInstance(LevelOne);
                Success = true;
            }

            if (IsPressed(VirtualKeyStates.Numpad2))
            {
                Activator.CreateInstance(LevelTwo);
                Success = true;
            }

            //if (IsPressed(VirtualKeyStates.Return))
            //  Success = true;
        }

        #endregion

        public override BaseLevel NextLevel()
        {
            if (IsPressed(VirtualKeyStates.Numpad1))
            {
                return (BaseLevel) Activator.CreateInstance(LevelOne);
            }

            if (IsPressed(VirtualKeyStates.Numpad2))
            {
                return (BaseLevel) Activator.CreateInstance(LevelTwo);
            }
            return NextLevel();
        }
    }
}
