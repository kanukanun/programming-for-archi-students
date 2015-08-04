using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rhino;
using Rhino.Geometry;
using Rhino.Display;

namespace _5.Classes
{
    class Loop : Rhino_Processing
    {
        List<Vertex> vertex = new List<Vertex>();

        public override void Setup()
        {
            //インスタンスを作成
            Random rnd = new Random();
            for (int i = 0; i < 30; i++)
            {
                int num_child = rnd.Next(1, 4);
                int[] id_child = new int[1];
                double[] dis_child = new double[1];
                int X_coordinate = rnd.Next(0, 200);
                int Y_coordinate = rnd.Next(0, 200);

                vertex.Add(new Vertex(i, num_child, rnd, id_child, dis_child, 0, 0, false, X_coordinate, Y_coordinate));

                vertex[i].MarkPoint();
            }

            //子オブジェクトのidとその間にノードを作成
            for (int i = 0; i < vertex.Count; i++)
            {
                vertex[i].SetChildId(vertex);
                vertex[i].MakeNode(vertex);
            }
            
            //ダイクストラ法の処理
            int probable_id = 0;
            vertex[0].condition = true;

            while (vertex[vertex.Count - 1].condition != true)//ゴールの状態がtrueになるまで繰り返す
            {
                for (int i = 0; i < vertex[probable_id].id_child.Length; i++)
                {
                    if (vertex[vertex[probable_id].id_child[i]].cost == 0) //子オブジェクトへの探索が行われていなければ処理を行う
                    {
                        vertex[vertex[probable_id].id_child[i]].cost = vertex[probable_id].cost + vertex[probable_id].dis_child[i];//自分の位置まで来るコストと対象の子オブジェクトとの距離をコストとする
                        vertex[vertex[probable_id].id_child[i]].parent_id = probable_id;//子オブジェクトの親を自分とする
                    }
                    else if (vertex[vertex[probable_id].id_child[i]].cost > vertex[probable_id].cost + vertex[probable_id].dis_child[i])//子オブジェクトの持っているコストより自分を通る際のコストが小さければ更新
                    {
                        vertex[vertex[probable_id].id_child[i]].cost = vertex[probable_id].cost + vertex[probable_id].dis_child[i];
                        vertex[vertex[probable_id].id_child[i]].parent_id = probable_id;
                    }
                }

                //探索が行われているvertex中で、最短経路である可能性が高いものをprobable_idとする。
                for (int i = 0; i < vertex.Count; i++)
                {
                    if (vertex[probable_id].condition == true && vertex[i].cost != 0)
                    {
                        probable_id =i;
                    }
                }

                //存在するすべてのコストより小さいものが存在したらprobable_idとする
                for (int i = 0; i < vertex.Count; i++)
                {
                    if (vertex[i].condition == false && vertex[i].cost != 0 && vertex[i].cost < vertex[probable_id].cost)
                    {
                        probable_id = i;
                    }
                }
                vertex[probable_id].condition = true;//probable_idの点にとって現在のコストが最少とする
            }

            //ゴールからparent_idを辿っていき、スタートになるまで処理を繰り返す
            int a = vertex.Count - 1;
            int b = 100;
            while (b != 0)
            {
                cst.AddLine(vertex[a].DrawShortest(vertex), Color.Red);
                b = vertex[a].ReturnParentId(b);
                a = b;
            }

            //スタートとゴールのカラーを変更
            cst.AddPoint(vertex[0].position, Color.Aqua);
            cst.AddPoint(vertex[vertex.Count - 1].position, Color.Blue);
        }
        

        public override void Draw()
        {
            for (int i = 0; i < vertex.Count; i++)
            {
                vertex[i].Display(doc);
            } 
        }
    }
}
