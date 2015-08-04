﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _5.Classes
{
    class Loop : Rhino_Processing
    {
        CrvonSrf cos;

        //一度処理を行えればいいので、Setup関数に記述した
        public override void Setup()
        {
            cos = new CrvonSrf(10, 300, 300);
            cos.MakePoint();
            cos.CreateCrv();
            cos.DecideCoplanar();
            
        }
        
        public override void Draw()
        {
            cos.Display(doc);
        }
    }
}
