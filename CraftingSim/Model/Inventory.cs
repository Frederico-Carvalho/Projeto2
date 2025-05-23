using System.Collections.Generic;
using System.IO;

namespace CraftingSim.Model
{
    /// <summary>
    /// Inventory of materials and quantities the user has
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Dictionary that contains all the materials the user has
        /// and the respective amount
        /// </summary>
        private readonly Dictionary<IMaterial, int> materials;

        public Inventory()
        {
            materials = new Dictionary<IMaterial, int>();
        }

        /// <summary>
        /// Provides all materials in the inventory.
        /// </summary>
        public IEnumerable<IMaterial> Materials => materials.Keys;

        // <summary>
        /// provides the amount of the specified material in the inventory.
        /// </summary>
        /// <param name="material">The material to view corresponding amount.</param>
        /// <returns>The quantity in inventory, or 0 if not contained</returns>
        public int GetQuantity(IMaterial material) => materials.ContainsKey(material) ? materials[material] : 0;


        /// <summary>
        /// Adds or replaces the quantity for a specific material
        /// </summary>
        /// <param name="material">The material to add</param>
        /// <param name="quantity">The new amount to set</param>
        public void AddMaterial(IMaterial material, int quantity)
        {
            if (materials.ContainsKey(material))
                materials[material] += quantity;
            else
                materials[material] = quantity;
        }

        /// <summary>
        /// Removes a given amount of a material from inventory
        /// If theres not enough material it is not removed.
        /// </summary>
        /// <param name="material">The material we want to remove from</param>
        /// <param name="quantity">The amount to remove</param>
        /// <returns>True if removed successfuly, false if not enough material</returns>
        public bool RemoveMaterial(IMaterial material, int quantity)
        {
            if (materials.ContainsKey(material) && materials[material] >= quantity)
            {
                materials[material] -= quantity;
                if (materials[material] == 0)
                    materials.Remove(material);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get all the materials the user has in the inventory.
        /// </summary>
        /// <returns>A read only dictionary of materials</returns>
        public IReadOnlyDictionary<IMaterial, int> GetAllMaterials()
        {
            return materials;
        }

        /// <summary>
        /// Search and return a material by the Id.
        /// </summary>
        /// <param name="id">The material id</param>
        /// <returns>The material if it's in the inventory, if not returns null
        /// </returns>
        public IMaterial GetMaterial(int id)
        {
            foreach (var mat in materials.Keys)
            if (mat.Id == id)
                return mat;
        return null;
        }

        /// <summary>
        /// Loads the materials and their quantities from the text file.
        /// </summary>
        /// <param name="file">Path to the materials file</param>
        public void LoadMaterialsFromFile(string file)
        {
            var lines = System.IO.File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1].Trim();
                    int qty = int.Parse(parts[2]);
                    AddMaterial(new Material(id, name), qty);
                }
            }
        }
    }
}
