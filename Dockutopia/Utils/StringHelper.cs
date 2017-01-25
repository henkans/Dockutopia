using System;

namespace Dockutopia.Utils
{
    public class StringHelper
    {
        public static string RemoveDockerFirstOccurrence(string input)
        {
            var find = "docker";
            var place = input.Trim().IndexOf(find, StringComparison.Ordinal);
            if(place > -1 && place < 2) return input.Remove(place, find.Length).Trim();
            return input.Trim();
        }

    }
}
