﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto.Protocolos.Chat.Entidades
{
    [Serializable]
    public class Posicion
    {
        int x;
        int y;
        public int X
        {
            get {
                return x;
            }
            set {
                this.x = value;
            }
            
        }
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                this.y = value;
            }

        }
        public Posicion()
        { 
        }
        public Posicion(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
    }
}
