using System;

namespace ServiceFramework
{
    public class Validation
    {
        public static bool ValidateString(string name)
        {
            bool output = true;

            char[] invalidCharacter = "'~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();

            if ((name.Length < 2) || (name.IndexOfAny(invalidCharacter)) >= 0)
                output = false;

            return output;
        }
    }
}