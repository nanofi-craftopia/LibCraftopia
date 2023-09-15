using UnityEngine;

namespace LibCraftopia.Item
{
    public interface IItemAssetHandler
    {
        bool HasPrefab(int id);
        GameObject LoadPrefab(int id);
    }
}