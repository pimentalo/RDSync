namespace RDSync.Core
{
    public abstract class Directory
    {
        protected Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent;
        }
        public Directory Parent { get;}
        public virtual string Name { get; set; }

        public abstract IEnumerable<File> GetFiles();

        public string FullPath { get { return (Parent == null ? "" : Parent.FullPath) + $"/{Name}"; } }
    }
}