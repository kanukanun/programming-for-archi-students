using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json_dynamicjson01
{
    public class Json
    {
        #region static
        /// <summary>
        /// JSON 形式のデータを解析します。
        /// </summary>
        /// <param name="srcJson">JSON 形式のデータ</param>
        /// <returns><paramref name="srcJson"/> から解析したデータ</returns>
        public static object Parse(string srcJson)
        {
            Json json = new Json();
            json.InitializeParse(srcJson);
            return json.Parse();
        }

        /// <summary>
        /// <see cref="System.Object"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Object"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcObject"/></returns>
        public static string Serialize(System.Collections.Generic.IList<object> srcObject)
        {
            Json json = new Json();
            json.InitializeSerialize(srcObject);
            return json.Serialize();
        }

        /// <summary>
        /// <see cref="System.Object"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Object"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcObject"/></returns>
        public static string Serialize(System.Collections.Generic.Dictionary<string, object> srcObject)
        {
            Json json = new Json();
            json.InitializeSerialize(srcObject);
            return json.Serialize();
        }
        #endregion

        /// <summary>
        /// 現在の読み込み位置を取得、設定します。
        /// </summary>
        private int Position
        {
            get;
            set;
        }

        /// <summary>
        /// JSON 形式の文字列を取得、設定します。
        /// </summary>
        private System.Text.StringBuilder JsonString
        {
            get;
            set;
        }

        /// <summary>
        /// JSON 形式に変換する <see cref="System.Object"/> を取得、設定します。
        /// </summary>
        private object JsonObject
        {
            get;
            set;
        }

        /// <summary>
        /// 読み込み可能かどうかを取得します。
        /// </summary>
        private bool IsRead
        {
            get
            {
                return this.Position < this.JsonString.Length;
            }
        }

        #region Parse
        /// <summary>
        /// 解析の初期化を行います。
        /// </summary>
        /// <param name="srcJson"></param>
        private void InitializeParse(string srcJson)
        {
            this.Position = 0;
            this.JsonString = new System.Text.StringBuilder(srcJson);
        }

        /// <summary>
        /// JSON 形式のデータを解析します。
        /// </summary>
        private object Parse()
        {
            while (this.IsRead)
            {
                char c1 = Read();

                if (c1 == '[')
                {
                    return ParseArray();
                }
                else if (c1 == '{')
                {
                    return ParseObject();
                }
            }

            return "";
        }

        /// <summary>
        /// JSON 形式の配列を解析します。
        /// </summary>
        /// <returns>解析したデータ</returns>
        private object ParseArray()
        {
            System.Collections.Generic.List<object> sb = new System.Collections.Generic.List<object>();

            while (this.IsRead)
            {
                char c1 = Read();

                if (c1 == ']')
                {
                    break;
                }
                else if (c1 == ' ' || c1 == '\n' || c1 == '\r' || c1 == '\t' || c1 == ',')
                {
                    continue;
                }

                sb.Add(ParseValue(c1));
            }

            return sb.ToArray();
        }

        /// <summary>
        /// JSON 形式の配列、オブジェクト以外の値を解析します。
        /// </summary>
        /// <returns>解析したデータ</returns>
        private object ParseNumber()
        {
            this.Position--;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            while (this.IsRead)
            {
                char c1 = Read();

                if (c1 == ',' || c1 == ':' || c1 == '}' || c1 == ']')
                {
                    this.Position--;
                    break;
                }

                sb.Append(c1);
            }

            return ToNumeric(sb.ToString());
        }

        /// <summary>
        /// JSON 形式のオブジェクトを解析します。
        /// </summary>
        /// <returns>解析したデータ</returns>
        private object ParseObject()
        {
            System.Collections.Generic.Dictionary<string, object> hashMap = new System.Collections.Generic.Dictionary<string, object>();
            string key = null;

            while (this.IsRead)
            {
                char c1 = Read();

                if (c1 == '}')
                {
                    break;
                }
                else if (c1 == ' ' || c1 == '\n' || c1 == '\r' || c1 == '\t' || c1 == ',' || c1 == ':')
                {
                    continue;
                }
                else if (key == null)
                {
                    key = ParseString();
                    continue;
                }

                hashMap[key] = ParseValue(c1);
                key = null;
            }

            return hashMap;
        }

        /// <summary>
        /// JSON 形式の文字列を解析します。
        /// </summary>
        /// <returns>解析したデータ</returns>
        private string ParseString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            while (this.IsRead)
            {
                char c1 = Read();

                if (c1 == '"')
                {
                    break;
                }

                if (c1 == '\\')
                {
                    int remain = this.JsonString.Length - this.Position;
                    char c2 = Read();

                    switch (c2)
                    {
                        case 'b':
                        case 'f':
                        case 'n':
                        case 'r':
                        case 't':
                            sb.AppendFormat("{0}{1}", c1, c2);
                            break;
                        case 'u':
                            if (remain >= 4)
                            {
                                char c = System.Convert.ToChar(System.Convert.ToInt32(this.JsonString.ToString().Substring(this.Position, 4), 16), System.Globalization.CultureInfo.InvariantCulture);
                                sb.Append(c);
                                this.Position += 4;
                            }
                            break;
                        case '"':
                        case '\\':
                        case '/':
                        default:
                            sb.Append(c2);
                            break;
                    }
                }
                else
                {
                    sb.Append(c1);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// JSON 形式のデータを解析します。
        /// </summary>
        /// <param name="srcChar">識別文字</param>
        /// <returns>解析したデータ</returns>
        private object ParseValue(char srcChar)
        {
            switch (srcChar)
            {
                case '[':
                    return ParseArray();
                case '{':
                    return ParseObject();
                case '"':
                    return ParseString();
                case 't':
                    this.Position += 3;
                    return true;
                case 'f':
                    this.Position += 4;
                    return false;
                case 'n':
                    this.Position += 3;
                    return null;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return ParseNumber();
                case '-':
                    int remain = this.JsonString.Length - this.Position;
                    char c2 = (remain > 0) ? this.JsonString[this.Position + 1] : default(char);

                    if (0x30 <= c2 && c2 <= 0x39)
                    {
                        return ParseNumber();
                    }

                    break;
            }

            return null;
        }

        /// <summary>
        /// 1 文字だけ読み進めます。
        /// </summary>
        /// <returns></returns>
        private char Read()
        {
            if (!IsRead)
            {
                return '\0';
            }

            char c = this.JsonString[this.Position];
            this.Position++;

            return c;
        }
        #endregion

        #region Serialize
        /// <summary>
        /// シリアライズの初期化を行います。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Object"/></param>
        private void InitializeSerialize(object srcObject)
        {
            this.JsonObject = srcObject;
        }

        /// <summary>
        /// <see cref="System.Object"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <returns></returns>
        private string Serialize()
        {
            return SerializeValue(this.JsonObject);
        }

        /// <summary>
        /// <see cref="System.Array"/> もしくは <see cref="System.Collections.Generic.List{Object}"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Array"/> もしくは <see cref="System.Collections.Generic.List{Object}"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcObject"/></returns>
        private string SerializeArray(object srcObject)
        {
            int count = 0;
            System.Collections.Generic.IList<object> srcList = srcObject as System.Collections.Generic.IList<object>;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("[");

            for (int i = 0; i < srcList.Count; i++)
            {
                if (count != 0)
                {
                    sb.Append(", ");
                }

                sb.Append(SerializeValue(srcList[i]));

                count++;
            }

            sb.Append("]");

            return sb.ToString();
        }

        /// <summary>
        /// <see cref="System.String"/> を JSON 形式の数値にシリアライズします。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Object"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcObject"/></returns>
        private string SerializeNumber(object srcObject)
        {
            return ToNumeric(srcObject.ToString()).ToString();
        }

        /// <summary>
        /// <see cref="System.Collections.Generic.Dictionary{String, Object}"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Collections.Generic.Dictionary{String, Object}"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcObject"/></returns>
        private string SerializeObject(object srcObject)
        {
            int count = 0;
            System.Collections.Generic.Dictionary<string, object> srcDictionary = srcObject as System.Collections.Generic.Dictionary<string, object>;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("{");

            foreach (var key in srcDictionary.Keys)
            {
                if (count != 0)
                {
                    sb.Append(", ");
                }

                sb.AppendFormat("{0}:{1}", SerializeString(key), (SerializeValue(srcDictionary[key])));

                count++;
            }

            sb.Append("}");

            return sb.ToString();
        }

        /// <summary>
        /// <see cref="System.String"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <param name="srcString">JSON 形式にシリアライズする <see cref="System.String"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcString"/></returns>
        private string SerializeString(object srcObject)
        {
            string srcString = srcObject.ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("\"");

            for (int i = 0; i < srcString.Length; i++)
            {
                char c1 = srcString[i];

                switch (c1)
                {
                    case '"':
                    case '\\':
                    case '/':
                    case '\b':
                    case '\f':
                    case '\n':
                    case '\r':
                    case '\t':
                        sb.AppendFormat("\\{0}", c1);
                        break;
                    default:
                        if (c1 != 0x3C && c1 != 0x3E && 0x20 <= c1 && c1 <= 0x7E)
                        {
                            sb.Append(c1);
                        }
                        else
                        {
                            sb.Append("\\u" + System.Convert.ToString(c1, 16).PadLeft(4, '0'));
                        }
                        break;
                }
            }

            sb.Append("\"");

            return sb.ToString();
        }

        /// <summary>
        /// <see cref="System.Object"/> を JSON 形式にシリアライズします。
        /// </summary>
        /// <param name="srcObject">JSON 形式にシリアライズする <see cref="System.Object"/></param>
        /// <returns>JSON 形式にシリアライズされた <paramref name="srcObject"/></returns>
        private string SerializeValue(object srcObject)
        {
            if (srcObject == null)
            {
                return "null";
            }

            System.TypeCode typeCode = System.Type.GetTypeCode(srcObject.GetType());

            switch (typeCode)
            {
                case System.TypeCode.Byte:
                case System.TypeCode.SByte:
                case System.TypeCode.Int16:
                case System.TypeCode.Int32:
                case System.TypeCode.Int64:
                case System.TypeCode.UInt16:
                case System.TypeCode.UInt32:
                case System.TypeCode.UInt64:
                case System.TypeCode.Single:
                case System.TypeCode.Double:
                    return SerializeNumber(srcObject);
                case System.TypeCode.Boolean:
                    return srcObject.ToString().ToLower();
                case System.TypeCode.String:
                    return SerializeString(srcObject);
                default:
                    if (srcObject is System.Collections.Generic.Dictionary<string, object>)
                    {
                        return SerializeObject(srcObject);
                    }
                    else if (srcObject is System.Collections.Generic.IList<object>)
                    {
                        return SerializeArray(srcObject);
                    }
                    else
                    {
                        return SerializeString(srcObject);
                    }
            }
        }
        #endregion

        /// <summary>
        /// 文字列を数値に変換します。
        /// </summary>
        /// <param name="srcString">数値に変換する文字列</param>
        /// <returns>数値に変換された <paramref name="srcString"/></returns>
        private object ToNumeric(string srcString)
        {
            System.Int64 dstInt64;
            if (System.Int64.TryParse(srcString, out dstInt64))
            {
                return dstInt64;
            }

            System.UInt64 dstUInt64;
            if (System.UInt64.TryParse(srcString, out dstUInt64))
            {
                return dstUInt64;
            }

            System.Double dstDouble;
            if (System.Double.TryParse(srcString, out dstDouble))
            {
                return dstDouble;
            }

            return 0;
        }
    }
}
