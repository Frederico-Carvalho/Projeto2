using System;

namespace CraftingSim.Model
{

    public class Material : IMaterial
    {
        public int Id { get; }
        public string Name { get; }
        public string Type { get; }

        public Material(int id, string name, string type = "")
        {
            Id = id;
            Name = name.Trim();
            Type = type.Trim();
        }

        public bool Equals(IMaterial other)
        {
            if (other == null) return false;
            return Id == other.Id || Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj) => Equals(obj as IMaterial);

        public override int GetHashCode() => Id.GetHashCode() ^ Name.ToLowerInvariant().GetHashCode();
    }
}