using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rhino;
using Rhino.Geometry;

namespace _5.Classes
{
    class Loop : Rhino_Processing
    {
        //AstroObject型の空のリストを生成
        List<AstroObject> astroobjs = new List<AstroObject>();

        public override void Setup()
        {
            //0個目のオブジェクトを追加
            astroobjs.Add(new AstroObject(63.71, new Point3d(), 1496.0, 365.4));

            //rndにRandom関数を格納
            Random rnd = new Random();

            for (int i = 1; i < 10; i++)
            {
                //60から300の範囲でランダムに決まった周知をrnd_daysに格納
                double rnd_days = rnd.Next(60, 300);

                //く繰り返す毎にオブジェクトを追加
                astroobjs.Add(new AstroObject(17.37, astroobjs[0].Center(), 384.0 + i * 100, rnd_days));
            }
        }

        public override void Draw()
        {
            //0番目のみ公転の中心を原点としているため個別に処理
            astroobjs[0].Rotate();
            astroobjs[0].Display(doc);

            for (int i = 1; i < astroobjs.Count; i++)
            {
                //i番目のオブジェクトの公転の中心を更新
                astroobjs[i].UpdateCenter(astroobjs[0].Center());
    
                astroobjs[i].Rotate();
                astroobjs[i].Display(doc);
            }
        }

    }
}
