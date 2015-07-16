using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;
using System.IO;
using System.Net;
using Codeplex.Data;

using Rhino;
using Rhino.Geometry;
using Rhino.Display;



namespace json_dynamicjson01
{
    class Loop : Rhino_Processing
    {
        List<Point3d> posList = new List<Point3d>();
        List<Line> lnList_area = new List<Line>();
        List<Line> lnList_age = new List<Line>();

        public override void Setup()
        {
            var url = "http://statdb.nstac.go.jp/api/1.0b/app/json/getStatsData?appId=";

            var appId = "c4027947d9d064ddbc034e3520f0a95d2935ade5";  // 自分のアプリケーション ID を貼り付けて下さい。
            var statsDataId = "&statsDataId=" + "0003104197";

            var cdCat01 = "&cdCat01=" + "000";
            var cdCat02 = "&cdCat02From=" + "01001" + "&cdCat02To=" + "04018";
            var cdArea = "&cdAreaFrom=" + "01000" + "&cdAreaTo=" + "47000";
            var options = cdCat01 + cdCat02 + cdArea;

            var requestUrl = url + appId + statsDataId + options;

            var json = RequestToAPI(requestUrl);

            var dic = Json.Parse(json) as Dictionary<string, object>;
            var values = dic.GetValue("GET_STATS_DATA/STATISTICAL_DATA/DATA_INF/VALUE");

            object[] objects = null;

            if (values.GetType().IsArray)
            {
                objects = values as object[];
            }
            else
            {
                objects = new object[] { objects };
            }

            var valueList = objects != null ? objects.OfType<Dictionary<string, object>>().ToArray() : null;
            if (valueList == null)
            {
                throw new NullReferenceException();
            }

            int Xcd = 0;
            int Ycd = 0;
            int temp = 0;
            int temp_area = 0;
            int count_area = 0;

            foreach (var value in valueList)
            {
                var cat02 = value.GetValue("@cat02") as string;
                var area = value.GetValue("@area") as string;
                var _value = value.GetValue("$") as string;
                
                if (int.Parse(cat02) != temp && temp != 0)
                {
                    Xcd += 100000;
                    Ycd = 0;
                }

                posList.Add(new Point3d(Xcd, Ycd, double.Parse(_value) * 1000));
                Ycd += 100000;

                temp = int.Parse(cat02);

                if (count_area == 0 || temp_area == int.Parse(area))
                {
                    temp_area = int.Parse(area);
                    count_area += 1;
                }
            }

            MakeLine(posList, count_area);
        }

        private void MakeLine(List<Point3d> list, int number)
        {
            int a = 1;

            for (int i = 1; i < list.Count; i++)
            {
                if ((i / a) * number != list.Count)
                {
                    lnList_age.Add(new Line(list[i - 1], list[i]));
                }
                else
                {
                    a += 1;
                }

                if (i > list.Count / number - 1)
                {
                    lnList_area.Add(new Line(list[i - list.Count / number], list[i]));
                }
            }
        }

        private static string RequestToAPI(string url)
        {
            WebRequest req = WebRequest.Create(url);
            HttpWebResponse response = null;
            string rxText = string.Empty;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                Stream resStream = response.GetResponseStream();
                var sr = new StreamReader(resStream, Encoding.UTF8);
                rxText = sr.ReadToEnd();
                sr.Close();
                resStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            return rxText;
        }

        
        public override void Draw()
        {
            foreach (var lnag in lnList_age)
            {
                doc.Objects.AddLine(lnag);
            }

            foreach (var lnar in lnList_area)
            {
                doc.Objects.AddLine(lnar);
            }
        }
    }

    public static class JsonExtensions
    {
        public static object GetValue(this Dictionary<string, object> node, string key)
        {
            // "/" を含む場合
            var index = key.IndexOf("/");
            if (index > 0)
            {
                var sub_key = key.Substring(0, index);
                key = key.Substring(index + 1);
                object sub_obj = null;
                if (node.TryGetValue(sub_key, out sub_obj))
                {
                    var dic = sub_obj as Dictionary<string, object>;
                    if (dic != null)
                    {
                        return dic.GetValue(key);
                    }
                }
            }
            else
            {
                object obj = null;
                if (node.TryGetValue(key, out obj))
                {
                    return obj;
                }
            }
 
            return null;
        }
    }
}

//http://kisuke0303.sakura.ne.jp/blog/?p=214
//http://neue.cc/2010/04/30_256.html
