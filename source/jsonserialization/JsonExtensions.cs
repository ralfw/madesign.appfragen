using System.Linq;
using System.Text;

namespace jsonserialization
{
    public static class JsonExtensions
    {
        public static dynamic FromJson(this string jsonObj)
        {
            var reader = new JsonFx.Json.JsonReader();
            return reader.Read(jsonObj);
        }

        public static T FromJson<T>(this string jsonObj)
        {
            var reader = new JsonFx.Json.JsonReader();
            return reader.Read<T>(jsonObj);
        }



        public static string ToJson(this object obj)
        {
            var writer = new JsonFx.Json.JsonWriter();
            var json = writer.Write(obj);
            return PrettifyJson(json);
        }


        // source: http://stackoverflow.com/questions/4580397/json-formatter-in-c
        private const string INDENT_STRING = "  ";

        public static string PrettifyJson(this string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
