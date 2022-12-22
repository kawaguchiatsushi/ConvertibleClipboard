using System.Text.RegularExpressions;

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
            int checkCount = 5;

            if (0 < txtleng && txtleng > checkCount)
            {
                for (int i = 0; i <= txtleng; i++)
                {
                    if (Char.IsLetter(text, i))
                        return en;
                }
            }
            else 
            {
                for (int i = 0; i <= checkCount; i++)
                {
                    if (Char.IsLetter(text, i))                  
                        return en;
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
            if (EngorNot(text) == "en")
            {
                text = text.Replace(Environment.NewLine, " ");
                text = text.Replace("\r", " ").Replace("\n", " ");
                return text;
            }
            else
            {
                text = text.Replace(Environment.NewLine, "");
                text = text.Replace("\r", "").Replace("\n", "");
                return text;
            }
        }

        /// <summary>
        /// file save mode
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxCharCount"></param>
        /// <returns></returns>
        public string FileModeReplacement(string text,int maxCharCount) 
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
    }
}
