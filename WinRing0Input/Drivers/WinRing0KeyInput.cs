using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinRing0Input.Drivers
{
    public class WinRing0KeyInput
    {
        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(int keyCode);

        [StructLayout(LayoutKind.Explicit)]
        struct Helper
        {
            [FieldOffset(0)] public short Value;
            [FieldOffset(0)] public byte Low;
            [FieldOffset(1)] public byte High;
        }

        public void Initialize()
        {
            var status = WinRing0.init();
            if (status != Ols.Status.NO_ERROR)
                throw new Exception("WinRing0 init failed. Error Code: " + status.ToString());
        }

        public void Input(string str)
        {
            if (this.IsCapsLockOn())
                KeyPress((char)Keys.CapsLock);

            foreach (var c in str)
            {
                var key = CharToKey(c);

                if (key.Shift)
                {
                    KeyDown((char)Keys.LShiftKey);
                    KeyDown((char)key.Key);
                    Thread.Sleep(10);
                    KeyUp((char)key.Key);
                    KeyUp((char)Keys.LShiftKey);
                }
                else
                {
                    KeyPress((char)((Keys)key.Key));
                }
            }
        }

        private WinRing0Key CharToKey(char c)
        {
            var helper = new Helper { Value = VkKeyScan(c) };
            byte virtualKeyCode = helper.Low;
            byte shiftState = helper.High;

            return new WinRing0Key
            {
                Shift = (shiftState & 1) != 0,
                Key = (Keys)virtualKeyCode
            };
        }


        private void KeyDown(char key)
        {
            WinRing0.KeyDown(key);
        }
        
        private void KeyUp(char key)
        {
            WinRing0.KeyUp(key);
        }

        private void KeyPress(char key)
        {
            KeyDown(key);
            Thread.Sleep(100);
            KeyUp(key);
        }

        private bool IsCapsLockOn()
        {
            return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }
    }


    class WinRing0Key
    {
        public bool Shift { get; set; }
        public Keys Key { get; set; }
    }
}
