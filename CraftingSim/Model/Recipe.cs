using System;
using System.Collections.Generic;

namespace CraftingSim.Model
{

    public class Recipe : IRecipe
    {
        public string Name { get; }
        public double SuccessRate { get; }
        public IReadOnlyDictionary<IMaterial, int> RequiredMaterials { get; }

        public Recipe(string name, double successRate, Dictionary<IMaterial, int> requiredMaterials)
        {
            Name = name.Trim();
            SuccessRate = successRate;
            RequiredMaterials = requiredMaterials;
        }

        public int CompareTo(IRecipe other)
            => string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }
}