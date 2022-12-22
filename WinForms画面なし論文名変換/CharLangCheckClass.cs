using System.Text.RegularExpressions;
using System.Collections.Generic;

using System.Diagnostics;

namespace WinForms画面なし論文名変換
{
    internal class CharLangCheckClass
    {

        /// <summary>
        /// Check for alphabetic characters at the beginning of a sentence
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string EngorNot(string text) 
        {
            string en ="en";
            string not = "not";
            int txtleng = text.Length;
            if (txtleng < 3) return "short";
            int checkCount = 3;

            if (3 < txtleng && txtleng < checkCount)
            {
                for (int i = 0; i <= txtleng; i++)
                {
                    if (Regex.IsMatch(text[i].ToString(), @"^[a-zA-Z]+$")) 
                    {
                        
                        return en;
                    }
                        
                }
            }
            else 
            {
                for (int i = 0; i <= checkCount; i++)
                {
                    
                    if (Regex.IsMatch(text[i].ToString(), @"^[a-zA-Z]+$"))
                    { 
                        return en;
                    }                  
                        
                }
            }
            
            return not;
        }

        /// <summary>
        /// For alphabetic characters, replace line feed codes with spaces.
        /// In other cases (e.g. Japanese), replace line feed codes with trailing spaces.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string MyReplacement(string text)
        {
            if (EngorNot(text) == "short") return "short";
            if (EngorNot(text) == "en")
            {
                
                text = text.Replace(Environment.NewLine, " ");
                text = text.Replace("\r", " ").Replace("\n", " ");
                
                return text;
            }
            else
            {
                text = AdvancedReplacementforJapaneseStrings(text);
                return text;
            }
        }

        /// <summary>
        /// file save mode
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxCharCount"></param>
        /// <returns></returns>
        public static string FileModeReplacement(string text,int maxCharCount) 
        {
            Regex reg = new(
                        "[\\x00-\\x1f<>:\"/\\\\|?*]|^(CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9]|CLOCK\\$)(\\.|$)|[\\. ]$"
                        , RegexOptions.IgnoreCase
                        );
            text = reg.Replace(text, "_");
            text = text.Replace(" ", "_").Replace("　", "_");
            int len = text.Length;
            if (len > maxCharCount)
            {
                text = text.Substring(0, maxCharCount);
            }
            return text;
        }


        private string AdvancedReplacementforJapaneseStrings(string jaText)
        {
            string replacedText = "";
            jaText = jaText.Replace("\n","\n").Replace("\r","\n");
            int len = jaText.Length;
            if (len < 4) return jaText;
            ///
            ///前後の連関を検索するために３文字でスライドさせる
            ///改行は、必要なところでとる。
            ///改行が必要なところは判断としては、改行コードの次が空白スペースかどうかで決定される。
            ///必要ではないところではとらない。
            
            for (int i = 0; i <= len -3; i++)
            {
                
                if (i < 2)
                {
                    replacedText += jaText[i];
                }
                else
                {
                    //Debug.WriteLine(i.ToString() + jaText[i - 1] + jaText[i] + jaText[i + 1]);
                    var prevChar = jaText[i - 1].ToString();
                    var currentChar = jaText[i].ToString();
                    var afterChar = jaText[i + 1].ToString();

                    string threeChar = jaText[(i-1)..(i+2)];
                    bool prBrank = prevChar == " " | prevChar == "　" | String.IsNullOrEmpty(prevChar);
                    bool curBrank = currentChar == " " | currentChar == "　" | String.IsNullOrEmpty(currentChar);
                    bool aftBlank = afterChar == " " | afterChar == "　" | String.IsNullOrEmpty(currentChar);
                    bool prNewline = prevChar == "\n";
                    bool curNewline = currentChar == "\n";
                    bool aftNewline = afterChar == "\n";

                    if (!prBrank && curBrank && !prBrank)
                    {

                        ///
                        ///ここでローマ字ならば入れる。日本語ならばcontinue
                        if (Regex.IsMatch(prevChar, @"^[a-zA-Z]+$") && Regex.IsMatch(afterChar, @"^[a-zA-Z]+$"))
                        {
                            replacedText += currentChar;
                            continue;
                        }
                        else
                        {

                            continue;
                        }

                    }
                    else if (curNewline & aftBlank)
                    {
                        
                        replacedText += "\n";
                        //continue;
                    }
                    else if (curBrank & prNewline)
                    {
                        replacedText += "　";
                        //continue;
                    }
                    else if(curNewline & (prevChar=="."| prevChar == "．" | prevChar == "。"))
                    {
                        replacedText += "\n";
                        replacedText += "　";

                    }
                    else if (!prBrank & curNewline & !aftBlank)
                    {
                        continue;
                    }
                    else
                    {
                        replacedText += currentChar;
                        
                    }
                }

            }
            return replacedText;
        }



    }
}
