using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRing0Input.Drivers;

namespace WinRing0Input
{
    class Program
    {
        /**
         * 1. 使用管理员权限运行。
         * 2. Properties => Build => disable "prefer 32-bits" (use x64 dll)
         * 3. Properties => Build Events => Post-build => copy all DLLs to bin
         * 4. 输入法调整为美式英文键盘（待改进？）
         * */
        static void Main(string[] args)
        {
            var keyInpt = new WinRing0KeyInput();

            // 必须使用管理员权限进行初始化，否则失败。
            keyInpt.Initialize();

            keyInpt.Input("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()[]{};:,./<>?");
            Console.Read();
        }
    }
}
