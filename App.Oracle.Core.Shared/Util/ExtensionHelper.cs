namespace App.Oracle.Core.Shared.Util
{
    public static class ExtensionHelper
    {
        public static string Success(this string message) => $@"<div class=""alert alert-success fade show shadow-sm"">{message}</div>";

        public static string Danger(this string message) => $@"<div class=""alert alert-danger fade show shadow-sm"">{message}</div>";

        public static string SuccessBlazor(this string message) => $@"<div class=""alert alert-success fade show shadow-sm"">{message}</div>";

        public static string DangerBlazor(this string message) => $@"<div class=""alert alert-danger fade show shadow-sm"">{message}</div>";

        public static bool IsEmail(this string value)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

            return !IsNullOrEmptyOrWhiteSpaces(value) && regex.IsMatch(value);
        }

        public static bool IsPhoneNumber(this string number)
        {
            var result = false;
            if (number.StartsWith("9715"))
            {
                if (number.Length == 12 && number.IsAllDigits())
                    result = true;
            }
            return result;
        }

        public static bool IsAllDigits(this string str)
        {
            return str.All(char.IsDigit);
        }

        public static bool IsNullOrEmptyOrWhiteSpaces(this object value)
        {
            return value == null || (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()) || value.ToString().Trim().Length == 0);
        }

        public static string MaskEmail(this string emailAddress)
        {
            return $"{emailAddress[0]}{emailAddress[1]}*****{emailAddress.Substring(emailAddress.IndexOf('@') - 2)}";
        }

        public static string MaskMobileNo(this string mobileNo)
        {
            var maskedNumber = string.Empty;
            mobileNo = mobileNo.RepairNumber();
            if (mobileNo.StartsWith("971") && mobileNo.Length == 12)
                maskedNumber = $"{mobileNo.Substring(0, 5)}****{mobileNo.Substring(9)}";
            else if (mobileNo.StartsWith("05") && mobileNo.Length == 10)
                maskedNumber = $"{mobileNo.Substring(0, 3)}****{mobileNo.Substring(7)}";
            else if (mobileNo.StartsWith("5") && mobileNo.Length == 9)
                maskedNumber = $"{mobileNo.Substring(0, 2)}****{mobileNo.Substring(6)}";
            return maskedNumber;
        }

        public static string RepairNumber(this string mobileNumber)
        {
            var repairedNumber = mobileNumber;
            repairedNumber = repairedNumber.ToLower().ToLower().Replace(@"  ", ""); repairedNumber = repairedNumber.ToLower().Replace(@" ", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"a", ""); repairedNumber = repairedNumber.ToLower().Replace(@"b", ""); repairedNumber = repairedNumber.ToLower().Replace(@"c", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"d", ""); repairedNumber = repairedNumber.ToLower().Replace(@"e", ""); repairedNumber = repairedNumber.ToLower().Replace(@"f", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"g", ""); repairedNumber = repairedNumber.ToLower().Replace(@"h", ""); repairedNumber = repairedNumber.ToLower().Replace(@"i", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"j", ""); repairedNumber = repairedNumber.ToLower().Replace(@"k", ""); repairedNumber = repairedNumber.ToLower().Replace(@"l", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"m", ""); repairedNumber = repairedNumber.ToLower().Replace(@"n", ""); repairedNumber = repairedNumber.ToLower().Replace(@"o", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"p", ""); repairedNumber = repairedNumber.ToLower().Replace(@"q", ""); repairedNumber = repairedNumber.ToLower().Replace(@"r", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"s", ""); repairedNumber = repairedNumber.ToLower().Replace(@"t", ""); repairedNumber = repairedNumber.ToLower().Replace(@"u", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"v", ""); repairedNumber = repairedNumber.ToLower().Replace(@"w", ""); repairedNumber = repairedNumber.ToLower().Replace(@"x", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"y", ""); repairedNumber = repairedNumber.ToLower().Replace(@"z", ""); repairedNumber = repairedNumber.ToLower().Replace(@"{", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"}", ""); repairedNumber = repairedNumber.ToLower().Replace(@"[", ""); repairedNumber = repairedNumber.ToLower().Replace(@"]", "");
            repairedNumber = repairedNumber.ToLower().Replace("\"", ""); repairedNumber = repairedNumber.ToLower().Replace(@"'", ""); repairedNumber = repairedNumber.ToLower().Replace(@":", "");
            repairedNumber = repairedNumber.ToLower().Replace(@";", ""); repairedNumber = repairedNumber.ToLower().Replace("\\", ""); repairedNumber = repairedNumber.ToLower().Replace(@"|", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"/", ""); repairedNumber = repairedNumber.ToLower().Replace(@".", ""); repairedNumber = repairedNumber.ToLower().Replace(@",", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"+", ""); repairedNumber = repairedNumber.ToLower().Replace(@"-", ""); repairedNumber = repairedNumber.ToLower().Replace(@"=", "");
            repairedNumber = repairedNumber.ToLower().Replace(@")", ""); repairedNumber = repairedNumber.ToLower().Replace(@"(", ""); repairedNumber = repairedNumber.ToLower().Replace(@"*", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"&", ""); repairedNumber = repairedNumber.ToLower().Replace(@"^", ""); repairedNumber = repairedNumber.ToLower().Replace(@"%", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"$", ""); repairedNumber = repairedNumber.ToLower().Replace(@"#", ""); repairedNumber = repairedNumber.ToLower().Replace(@"@", "");
            repairedNumber = repairedNumber.ToLower().Replace(@"!", ""); repairedNumber = repairedNumber.ToLower().Replace(@"~", ""); repairedNumber = repairedNumber.ToLower().Replace(@"`", "");
            repairedNumber = repairedNumber.ToLower().Replace("\t", ""); repairedNumber = repairedNumber.ToLower().Replace("\n", "");


            if ((repairedNumber.Length == 7))
            {

                repairedNumber = "97150" + repairedNumber;

            }
            if ((repairedNumber.Length == 14) || (repairedNumber.Length == 12) || (repairedNumber.Length == 10))
            {
                if (repairedNumber.Length == 10)
                {
                    repairedNumber = "971" + (repairedNumber.Substring(repairedNumber.Length - 9)).ToString();
                }
                if (repairedNumber.Length == 14)
                {
                    if (repairedNumber.Substring(0, 2) == "00") { } else { repairedNumber = "0"; }
                }
            }
            if ((repairedNumber.Length == 13) || (repairedNumber.Length == 11) || (repairedNumber.Length == 9))
            {

                if ((repairedNumber.Length == 9) && (repairedNumber.Substring(0, 1) == "0"))
                {
                    repairedNumber = "971" + (repairedNumber.Substring(repairedNumber.Length - 8)).ToString();
                }
                else
                {
                    if (repairedNumber.Substring(0, 2) == "05")
                    {
                        repairedNumber = "971" + (repairedNumber.Substring(1, 9)).ToString();
                    }
                    else
                    {
                        repairedNumber = "971" + repairedNumber.ToString();
                    }
                }
            }
            if ((repairedNumber.Length > 14))
            {
                if (repairedNumber.Substring(0, 2) == "05")
                {
                    repairedNumber = "971" + (repairedNumber.Substring(1, 9)).ToString();
                }
                else { repairedNumber = "0"; }

            }
            if (repairedNumber == "") { repairedNumber = "0"; }


            return repairedNumber;
        }

        public static bool IsValidIBAN(this string bankAccount)
        {
            bankAccount = bankAccount.ToUpper();
            if (bankAccount.IsNullOrEmptyOrWhiteSpaces())
                return false;
            else if (Regex.IsMatch(bankAccount, "^[A-Z0-9]"))
            {
                bankAccount = bankAccount.Replace(" ", string.Empty);
                if (bankAccount.Length != 23)
                    return false;
                string bank =
                bankAccount.Substring(4, bankAccount.Length - 4) + bankAccount.Substring(0, 4);
                int asciiShift = 55;
                StringBuilder sb = new StringBuilder();
                foreach (char c in bank)
                {
                    int v;
                    if (char.IsLetter(c)) v = c - asciiShift;
                    else v = int.Parse(c.ToString());
                    sb.Append(v);
                }
                string checkSumString = sb.ToString();
                int checksum = int.Parse(checkSumString.Substring(0, 1));
                for (int i = 1; i < checkSumString.Length; i++)
                {
                    int v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }
                return checksum == 1;
            }
            else
                return false;
        }

        public static bool ValidatePassword(this string passWord, int minimumLength, int maximumLength, bool oneUpperCaseLetter, bool oneLowerCaseLetter, bool oneDigit, bool oneSpecialCharacter, char[] allowedSpeicalCharacters)
        {
            bool validCondition = false;
            if (passWord.IsNullOrEmptyOrWhiteSpaces())
                return false;

            if (passWord.Length < minimumLength)
                return false;

            if (passWord.Length > maximumLength)
                return false;

            if (oneLowerCaseLetter)
            {
                foreach (char c in passWord)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        validCondition = true;
                        break;
                    }
                }
            }

            if (!validCondition) return false;

            if (oneUpperCaseLetter)
            {
                validCondition = false;
                foreach (char c in passWord)
                {
                    if (c >= 'A' && c <= 'Z')
                    {
                        validCondition = true;
                        break;
                    }
                }
            }

            if (!validCondition) return false;

            if (oneDigit)
            {
                validCondition = false;
                foreach (char c in passWord)
                {
                    if (c >= '0' && c <= '9')
                    {
                        validCondition = true;
                        break;
                    }
                }
            }

            if (!validCondition) return false;

            if (oneSpecialCharacter)
            {
                validCondition = false;
                //char[] special = { '`', '~','!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '|', '[', '{',  }; // or whatever    
                if (passWord.IndexOfAny(allowedSpeicalCharacters) == -1) return false;
            }

            return true;
        }

        public static bool HasEngSpecialChar(this string input)
        {
            string specialChar = "\\|!#$%&/()=?»«@£§€{}.-;'\"<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public static bool HasArabicGlyphs(this string text)
        {
            char[] glyphs = text.ToCharArray();
            foreach (char glyph in glyphs)
            {
                if (glyph >= 0x600 && glyph <= 0x6ff) return true;
                if (glyph >= 0x750 && glyph <= 0x77f) return true;
                if (glyph >= 0xfb50 && glyph <= 0xfc3f) return true;
                if (glyph >= 0xfe70 && glyph <= 0xfefc) return true;
            }
            return false;
        }

        public static T ToObject<T>(this DataRow dataRow) where T : new()
        {
            T item = new T();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                PropertyInfo property = GetProperty(typeof(T), column.ColumnName);

                if (property != null && dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
                {
                    property.SetValue(item, ChangeType(dataRow[column], property.PropertyType), null);
                }
            }

            return item;
        }

        private static PropertyInfo GetProperty(Type type, string attributeName)
        {
            PropertyInfo property = type.GetProperty(attributeName);

            if (property != null)
            {
                return property;
            }

            return type.GetProperties()
                 .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name == attributeName)
                 .FirstOrDefault();
        }

        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
            }

            return Convert.ChangeType(value, type);
        }

        private static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        public static double CalculateSimilarity(this string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 100.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            var similarCount = (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
            return ((similarCount * 100) / 1.0);
        }

        public static string ToJSON(this object value)
        {
            string requestString;
            try
            {
                requestString = JsonConvert.SerializeObject(value);
            }
            catch (Exception)
            {
                throw;
            }
            return requestString;
        }

        public static string ToJSON(this object[] value)
        {
            string requestString;
            try
            {
                requestString = JsonConvert.SerializeObject(value);
            }
            catch (Exception)
            {
                throw;
            }
            return requestString;
        }

        public static T CastObject<T>(this object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof(T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
               .ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;
        }

        /// <summary>Trim all String properties of the given object</summary>
        public static TSelf TrimStringProperties<TSelf>(this TSelf input)
        {
            var stringProperties = input.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(input, null);
                if (currentValue != null)
                    stringProperty.SetValue(input, currentValue.Trim(), null);
            }
            return input;
        }
    }
}
