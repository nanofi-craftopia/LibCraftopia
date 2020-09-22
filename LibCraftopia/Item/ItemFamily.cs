using HarmonyLib;
using LibCraftopia.Registry;
using Oc;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemFamily : IRegistryEntry
    {
        private readonly SoItemFamily inner;
        public int Id { get => inner.pKey; set => inner.pKey = value; }
        public ref string FamilyDesc { get => ref AccessTools.FieldRefAccess<SoItemFamily, string>(inner, "familyDesc"); }
        public ref Vector3 CameraPosition { get => ref inner.cameraPosition; }
        public ref Vector3 CameraRotation { get => ref inner.cameraRotation; }
        public float Fov { get => inner.fov; set => inner.fov = value; }
        public ref Vector3 ItemPosition { get => ref inner.itemPosition; }
        public ref Vector3 ItemRotation { get => ref inner.itemRotation; }
        public ref Vector3 ItemScale { get => ref inner.itemScale; }
        public ref Vector3 Dl01Rotation { get => ref inner.dl01_Rotation; }
        public ref Color Dl01Color { get => ref inner.dl01_Color; }
        public float Dl01Intensity { get => inner.dl01_Intensity; set => inner.dl01_Intensity = value; }
        public ref Vector3 Dl02Rotation { get => ref inner.dl02_Rotation; }
        public ref Color Dl02Color { get => ref inner.dl02_Color; }
        public float Dl02Intensity { get => inner.dl02_Intensity; set => inner.dl02_Intensity = value; }

        public ItemFamily()
        {
            inner = ScriptableObject.CreateInstance<SoItemFamily>();
        }
        public ItemFamily(SoItemFamily family)
        {
            inner = family;
        }

        public static implicit operator SoItemFamily(ItemFamily family)
        {
            return family.inner;
        }
        public static implicit operator ItemFamily(SoItemFamily family)
        {
            return new ItemFamily(family);
        }
    }
}
