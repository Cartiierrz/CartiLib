using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CartiLib.Checker.Module_Tools
{
    public class Functions //Stuff used for making modules
    {
        public static int CountStringOccurrences(string input, string text)
        {
            int count = 0;
            int i = 0;
            while ((i = input.IndexOf(text, i)) != -1)
            {
                i += text.Length;
                count++;
            }
            return count;
        }
        public static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }
        public static string GenerateRandom1NumberString(int length = 1)
        {
            Random Random = new Random();
            return new string((from s in Enumerable.Repeat<string>("0123456789", length)
                               select s[Random.Next(s.Length)]).ToArray<char>());
        }
        public static string GenerateRandom32NumberHashString(int length = 32)
        {
            Random Random = new Random();
            return new string((from s in Enumerable.Repeat<string>("0123456789abcdefghijklmnopqrstuvwxyz", length)
                               select s[Random.Next(s.Length)]).ToArray<char>());
        }
        public static string GenerateLowerCaseGuid()
        {
            return Guid.NewGuid().ToString().ToLower();
        }
        public static string RandomString(string randomize)
        {
            string Str = "";
            string Str2 = "123456789abcdef";
            string Str3 = "abcdefghijklmnopqrstuvwxyz";
            string Str4 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string Str5 = "1234567890";
            string Str6 = "!@#$%^&*()_+";
            string Str7 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string Str8 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random Random = new Random();
            for (int Index = 0; Index < randomize.Length - 1; Index++)
            {
                string str = randomize[Index].ToString();
                string Str9 = randomize[Index + 1].ToString();
                if ((str + Str9).Equals("?h"))
                {
                    string str2 = Str;
                    string Str10 = Str2[Random.Next(0, Str2.Length)].ToString();
                    Str = str2 + Str10;
                }
                else
                {
                    string str3 = randomize[Index].ToString();
                    string Str11 = randomize[Index + 1].ToString();
                    if ((str3 + Str11).Equals("?l"))
                    {
                        string str4 = Str;
                        string Str12 = Str3[Random.Next(0, Str3.Length)].ToString();
                        Str = str4 + Str12;
                    }
                    else
                    {
                        string str5 = randomize[Index].ToString();
                        string Str13 = randomize[Index + 1].ToString();
                        if ((str5 + Str13).Equals("?u"))
                        {
                            string str6 = Str;
                            string Str14 = Str4[Random.Next(0, Str4.Length)].ToString();
                            Str = str6 + Str14;
                        }
                        else
                        {
                            string str7 = randomize[Index].ToString();
                            string Str15 = randomize[Index + 1].ToString();
                            if ((str7 + Str15).Equals("?d"))
                            {
                                string str8 = Str;
                                string Str16 = Str5[Random.Next(0, Str5.Length)].ToString();
                                Str = str8 + Str16;
                            }
                            else
                            {
                                string str9 = randomize[Index].ToString();
                                string Str17 = randomize[Index + 1].ToString();
                                if ((str9 + Str17).Equals("?m"))
                                {
                                    string str10 = Str;
                                    string Str18 = Str7[Random.Next(0, Str7.Length)].ToString();
                                    Str = str10 + Str18;
                                }
                                else
                                {
                                    string str11 = randomize[Index].ToString();
                                    string Str19 = randomize[Index + 1].ToString();
                                    if ((str11 + Str19).Equals("?i"))
                                    {
                                        string str12 = Str;
                                        string Str20 = Str8[Random.Next(0, Str8.Length)].ToString();
                                        Str = str12 + Str20;
                                    }
                                    else
                                    {
                                        string str13 = randomize[Index].ToString();
                                        string Str21 = randomize[Index + 1].ToString();
                                        if ((str13 + Str21).Equals("?s"))
                                        {
                                            string str14 = Str;
                                            string Str22 = Str6[Random.Next(0, Str6.Length)].ToString();
                                            Str = str14 + Str22;
                                        }
                                        else if (randomize[Index].ToString().Contains("-"))
                                        {
                                            Str += "-";
                                        }
                                        else
                                        {
                                            int Num;
                                            if (randomize[Index - 1].ToString().Equals("-"))
                                            {
                                                Num = ((!randomize[Index].ToString().Equals("?")) ? 1 : 0);
                                            }
                                            else
                                            {
                                                Num = 0;
                                            }
                                            if (Num != 0)
                                            {
                                                string str15 = Str;
                                                string Str23 = randomize[Index].ToString();
                                                Str = str15 + Str23;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Str;
        }
        public static void ParseJson(string a, string b, List<KeyValuePair<string, string>> jsonlist)
        {
            jsonlist.Add(new KeyValuePair<string, string>(a, b));
            if (b.StartsWith("["))
            {
                JArray Jarray;
                try
                {
                    Jarray = JArray.Parse(b);
                }
                catch
                {
                    return;
                }
                foreach (object Child in Jarray.Children())
                {
                    ParseJson("", Child.ToString(), jsonlist);
                }
            }
            if (!b.Contains("{"))
            {
                return;
            }
            JObject Jobject;
            try
            {
                Jobject = JObject.Parse(b);
            }
            catch
            {
                return;
            }
            foreach (KeyValuePair<string, JToken> KeyValuePair in Jobject)
            {
                ParseJson(KeyValuePair.Key, KeyValuePair.Value.ToString(), jsonlist);
            }
        }
        public static IEnumerable<string> Json(string input, string field, bool recursive = false, bool useJToken = false)
        {
            List<string> Source = new List<string>();
            if (useJToken)
            {
                if (recursive)
                {
                    if (input.Trim().StartsWith("["))
                    {
                        Source.AddRange(from selectToken in JArray.Parse(input).SelectTokens(field, false)
                                        select selectToken.ToString());
                    }
                    else
                    {
                        Source.AddRange(from selectToken in JObject.Parse(input).SelectTokens(field, false)
                                        select selectToken.ToString());
                    }
                }
                else if (input.Trim().StartsWith("["))
                {
                    JArray Jarray = JArray.Parse(input);
                    Source.Add(Jarray.SelectToken(field, false).ToString());
                }
                else
                {
                    JObject Jobject = JObject.Parse(input);
                    Source.Add(Jobject.SelectToken(field, false).ToString());
                }
            }
            else
            {
                List<KeyValuePair<string, string>> Jsonlist = new List<KeyValuePair<string, string>>();
                ParseJson("", input, Jsonlist);
                Source.AddRange(Jsonlist.Where(delegate (KeyValuePair<string, string> KeyValuePair)
                {
                    KeyValuePair<string, string> keyValuePair = KeyValuePair;
                    return keyValuePair.Key == field;
                }).Select(delegate (KeyValuePair<string, string> KeyValuePair)
                {
                    KeyValuePair<string, string> keyValuePair = KeyValuePair;
                    return keyValuePair.Value;
                }));
                if (!recursive && Source.Count > 1)
                {
                    Source = new List<string> { Source.First<string>() };
                }
            }
            return Source;
        }
        public static string Parse(string source, string left, string right)
        {
            return source.Split(new string[] { left }, StringSplitOptions.None)[1].Split(new string[] { right }, StringSplitOptions.None)[0];
        }
        public static IEnumerable<string> LR(string input, string left, string right, bool recursive = false, bool useRegex = false)
        {
            IEnumerable<string> Result;
            if (left == string.Empty && right == string.Empty)
            {
                Result = new string[] { input };
            }
            else if ((left != string.Empty && !input.Contains(left)) || (right != string.Empty && !input.Contains(right)))
            {
                Result = new string[0];
            }
            else
            {
                string Text = input;
                List<string> List = new List<string>();
                if (recursive)
                {
                    if (useRegex)
                    {
                        try
                        {
                            string Pattern = BuildLrPattern(left, right);
                            foreach (object obj in Regex.Matches(Text, Pattern))
                            {
                                Match Match = (Match)obj;
                                List.Add(Match.Value);
                            }
                            goto IL_223;
                        }
                        catch
                        {
                            goto IL_223;
                        }
                    }
                    try
                    {
                        while (left == string.Empty || (Text.Contains(left) && (right == string.Empty || Text.Contains(right))))
                        {
                            int StartIndex = ((left == string.Empty) ? 0 : (Text.IndexOf(left) + left.Length));
                            Text = Text.Substring(StartIndex);
                            int Length = ((right == string.Empty) ? (Text.Length - 1) : Text.IndexOf(right));
                            string Post1 = Text.Substring(0, Length);
                            List.Add(Post1);
                            Text = Text.Substring(Post1.Length + right.Length);
                        }
                        goto IL_223;
                    }
                    catch
                    {
                        goto IL_223;
                    }
                }
                if (useRegex)
                {
                    string Pattern2 = BuildLrPattern(left, right);
                    MatchCollection MatchCollection2 = Regex.Matches(Text, Pattern2);
                    if (MatchCollection2.Count > 0)
                    {
                        List.Add(MatchCollection2[0].Value);
                    }
                }
                else
                {
                    try
                    {
                        int StartIndex2 = ((left == string.Empty) ? 0 : (Text.IndexOf(left) + left.Length));
                        Text = Text.Substring(StartIndex2);
                        int Length2 = ((right == string.Empty) ? Text.Length : Text.IndexOf(right));
                        List.Add(Text.Substring(0, Length2));
                    }
                    catch
                    {
                    }
                }
            IL_223:
                Result = List;
            }
            return Result;
        }

        private static string BuildLrPattern(string ls, string rs)
        {
            string Text = (string.IsNullOrEmpty(ls) ? "^" : Regex.Escape(ls));
            string Post1 = (string.IsNullOrEmpty(rs) ? "$" : Regex.Escape(rs));
            return string.Concat(new string[] { "(?<=", Text, ").+?(?=", Post1, ")" });
        }
    }
}
