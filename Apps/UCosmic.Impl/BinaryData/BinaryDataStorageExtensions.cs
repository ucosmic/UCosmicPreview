using System;
using System.Linq;

namespace UCosmic.Impl.BinaryData
{
    internal static class BinaryDataStorageExtensions
    {
        internal static void ValidateAsBinaryStoragePath(this string path)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Value cannot be empty or whitespace", "path");

            var exceptionFormat = "The value '{0}' is not a valid path. Paths must contain directory names " +
                "separated by forward slashes, and at least one directory must come before the file name.";
            var exceptionMessage = string.Format(exceptionFormat, path);

            // paths must use foward slashes
            if (path.Contains("\\")) throw new FormatException(exceptionMessage);
            if (!path.Contains("/")) throw new FormatException(exceptionMessage);
            if (path.EndsWith("/")) throw new FormatException(exceptionMessage);
            if (path.StartsWith("/") && path.ToCharArray().Count(x => x == '/') < 2)
                throw new FormatException(exceptionMessage);

            // http://msdn.microsoft.com/en-us/library/windowsazure/dd135715.aspx
            exceptionFormat = "The first directory in a path must be at least 3 characters long, can only contain " +
                "numbers, lowercase letters and hypens, and each hyphen must be preceeded by at least one lowercase letter " +
                "or number. Directories also must end with a lowercase letter or number (cannot end with a hyphen character).";
            exceptionMessage = string.Format(exceptionFormat, path);
            var validCharacters = new[] { '-', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var pathParts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var containerName = pathParts.First();
            if (containerName.Length < 3) throw new FormatException(exceptionMessage);
            if (containerName.StartsWith("-") || containerName.EndsWith("-"))
                throw new FormatException(exceptionMessage);
            var pathPartChars = containerName.ToCharArray();
            foreach (var pathPartChar in pathPartChars)
                if (!validCharacters.Contains(pathPartChar))
                    throw new FormatException(exceptionMessage);
            var lastChar = pathPartChars[0];
            foreach (var pathPartChar in pathPartChars.Skip(1))
            {
                if (pathPartChar == '-' && lastChar == '-')
                    throw new FormatException(exceptionMessage);
                lastChar = pathPartChar;
            }
        }
    }
}
