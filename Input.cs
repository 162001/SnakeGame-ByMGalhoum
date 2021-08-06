using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace SnakeGame
{
    class Input
    {
        private static Hashtable KeyTabl = new Hashtable();

        public static bool KeyPress(Keys key)
        {
            if (KeyTabl[key] == null)
            {
                return false;
            }
            return (bool)KeyTabl[key];
        }
        public static void changeState(Keys key,bool state)
        {
            KeyTabl[key] = state;
        }
    }
}
