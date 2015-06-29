using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;

namespace _5.Classes
{
    class Vertex : Rhino_Processing
    {
        //////////////
        //properties//
        //////////////
        private int id;
        private int num_child;
        private Random rnd;
        public int[] id_child;
        public double[] dis_child;
        public double cost;
        public int parent_id;
        public bool condition;
        private double X_coordinate;
        private double Y_coordinate;

        public Point3d position;
        private Line line1, line2, line3, Shortest_line;


        /////////////////
        //constructors//
        ////////////////

        public Vertex(
            int _id,
            int _num_child,
            Random _rnd,
            int[] _edge_id,
            double[] _dis_child,
            double _cost,
            int _parent_id,
            bool _condition,
            double _X_coordinate,
            double _Y_coordinate
            )
        {
            // set variables
            id = _id;
            num_child = _num_child;
            rnd = _rnd;
            id_child = _edge_id;
            dis_child = _dis_child;
            cost = _cost;
            parent_id = _parent_id;
            condition = _condition;
            X_coordinate = _X_coordinate;
            Y_coordinate = _Y_coordinate;
        }

        ///////////
        //methods//
        ///////////

        //
        public void MarkPoint()
        {
            position = new Point3d(X_coordinate, Y_coordinate, 0);
        }


        //子オブジェクトidとその数を決める
        public void SetChildId(List<Vertex> vertex)
        {
            //最後から3つのオブジェクトを個別に処理
            if (id == vertex.Count - 1)
            {
                id_child = new int[0] { };
            }
            else if (id == vertex.Count - 2)
            {
                int num1 = vertex.Count - 1;
                id_child = new int[1] { num1 };
            }
            else if (id == vertex.Count - 3)
            {
                if (num_child == 1)
                {
                    int num1 = rnd.Next(id + 1, vertex.Count);

                    id_child = new int[1] { num1 };
                }
                else if (num_child == 2 || num_child == 3)
                {
                    int num1 = vertex.Count - 2;
                    int num2 = vertex.Count - 1;

                    Array.Resize(ref id_child, 2);
                    id_child = new int[2] { num1, num2 };
                }
            }
            else //個別に処理したもの以外のオブジェクトの処理
            {
                if (num_child == 1)
                {

                    int num1 = rnd.Next(id + 1, vertex.Count);

                    id_child = new int[1] { num1 };
                }
                else if (num_child == 2)
                {
                    int num1 = rnd.Next(id + 1, vertex.Count - 1);
                    int num2 = rnd.Next(num1 + 1, vertex.Count);

                    Array.Resize(ref id_child, 2);
                    id_child = new int[2] { num1, num2 };
                }
                else if (num_child == 3)
                {
                    int num1 = rnd.Next(id + 1, vertex.Count - 2);
                    int num2 = rnd.Next(num1 + 1, vertex.Count - 1);
                    int num3 = rnd.Next(num2 + 1, vertex.Count);

                    Array.Resize(ref id_child, 3);
                    id_child = new int[3] { num1, num2, num3 };
                }
            }
        }

        //子オブジェクトとの間にノードを作成
        public void MakeNode(List<Vertex> vertex)
        {
            if (id_child.Length == 1)
            {
                int a = id_child[0];
                line1 = new Line(position, vertex[a].position);
                dis_child = new double[1]{line1.Length};
            }
            else if (id_child.Length == 2)
            {
                int a = id_child[0];
                int b = id_child[1];
                line1 = new Line(position, vertex[a].position);
                line2 = new Line(position, vertex[b].position);

                Array.Resize(ref dis_child, 2);
                dis_child = new double[2] { line1.Length, line2.Length };
            }
            else if (id_child.Length == 3)
            {
                int a = id_child[0];
                int b = id_child[1];
                int c = id_child[2];
                line1 = new Line(position, vertex[a].position);
                line2 = new Line(position, vertex[b].position);
                line3 = new Line(position, vertex[c].position);
                Array.Resize(ref dis_child, 3);
                dis_child = new double[3] { line1.Length, line2.Length, line3.Length };
            }
        }
       
        public Line DrawShortest(List<Vertex> vertex)
        {
            Point3d pos1 = new Point3d(position.X, position.Y, 1);
            Point3d pos2 = new Point3d(vertex[parent_id].position.X, vertex[parent_id].position.Y, 1);
            Shortest_line = new Line(pos1, pos2);
            return Shortest_line;
        }

        public int ReturnParentId(int b)
        {
            return parent_id;
        }

        //全ての点とノードを描画
        public void Display(RhinoDoc _doc)
        {
                _doc.Objects.AddLine(line1);//ノードを描画　３つノードがある場合を想定している。
                _doc.Objects.AddLine(line2);
                _doc.Objects.AddLine(line3);

        }
    }
}
