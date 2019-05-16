using DirectoryListing.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryListing.Models
{
    public class Directory
    {
        public Directory(string rootDirectory, string fullPath)
        {
            var directoryInfo = new DirectoryInfo(fullPath);
            RootDirectory = rootDirectory;
            Current = new Info(rootDirectory, directoryInfo.FullName, directoryInfo.Name, 0);
            Directories =
                directoryInfo
                    .EnumerateDirectories()
                    .Where(x => !x.Attributes.HasFlag(FileAttributes.Hidden))
                    .OrderByNatural(x => x.Name)
                    .Select(x => new Info(rootDirectory, x.FullName, x.Name, 0))
                    .ToArray()
                ;
            Files =
                directoryInfo
                    .EnumerateFiles()
                    .Where(x => !x.Attributes.HasFlag(FileAttributes.Hidden))
                    .OrderByNatural(x => x.Name)
                    .OrderBy(x => x.Extension, StringComparer.CurrentCultureIgnoreCase)
                    .Select(x => new Info(rootDirectory, x.FullName, x.Name, x.Length))
                    .ToArray()
                ;
            var rootDirectoryInfo = new DirectoryInfo(rootDirectory);
            var ancestors = new List<Ancestor>();
            while (directoryInfo.FullName != rootDirectoryInfo.FullName)
            {
                ancestors.Add(new Ancestor(rootDirectory, directoryInfo.FullName, directoryInfo.Name));
                directoryInfo = directoryInfo.Parent;
            }
            Ancestors = ancestors.Reverse<Ancestor>().ToArray();
        }

        public Ancestor[] Ancestors
        {
            get;
            private set;
        }

        public Info Current
        {
            get;
            private set;
        }

        public Info[] Directories
        {
            get;
            private set;
        }

        public Info[] Files
        {
            get;
            private set;
        }

        public string RootDirectory
        {
            get;
            private set;
        }
    }
}
