using HarmonyLib;
using LibCraftopia.Helper;
using Oc;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LibCraftopia.Item
{
    public class ItemFamilyBuilder
    {
        private readonly SoItemFamily family = ScriptableObject.CreateInstance<SoItemFamily>();
        private readonly Traverse traverse;

        private ItemFamilyBuilder()
        {
            traverse = new Traverse(family);
            FamilyDesc(string.Empty);
        }

        public static ItemFamilyBuilder Create()
        {
            return new ItemFamilyBuilder();
        }

        public ItemFamilyBuilder Id(int id)
        {
            traverse.Field<int>("pKey").Value = id;
            return this;
        }
        public ItemFamilyBuilder NewId()
        {
            return Id(ItemHelper.Inst.NewFamilyId());
        }
        public ItemFamilyBuilder NewId(out int id) {
            var builder = NewId();
            id = builder.family.FamilyId;
            return builder;
        }

        public ItemFamilyBuilder FamilyDesc(string value)
        {
            traverse.Field<string>("familyDesc").Value = value;
            return this;
        }

        public ItemFamilyBuilder CameraPosition(Vector3 value)
        {
            traverse.Field<Vector3>("cameraPosition").Value = value;
            return this;
        }
        public ItemFamilyBuilder CameraRotation(Vector3 value)
        {
            traverse.Field<Vector3>("cameraRotation").Value = value;
            return this;
        }
        public ItemFamilyBuilder Fov(float value)
        {
            traverse.Field<float>("fov").Value = value;
            return this;
        }
        public ItemFamilyBuilder ItemPosition(Vector3 value)
        {
            traverse.Field<Vector3>("itemPosition").Value = value;
            return this;
        }
        public ItemFamilyBuilder ItemRotation(Vector3 value)
        {
            traverse.Field<Vector3>("itemRotation").Value = value;
            return this;
        }
        public ItemFamilyBuilder ItemScale(Vector3 value)
        {
            traverse.Field<Vector3>("itemScale").Value = value;
            return this;
        }
        public ItemFamilyBuilder Dl01Rotation(Vector3 value)
        {
            traverse.Field<Vector3>("dl01_Rotation").Value = value;
            return this;
        }
        public ItemFamilyBuilder Dl01Color(Color value)
        {
            traverse.Field<Color>("dl01_Color").Value = value;
            return this;
        }
        public ItemFamilyBuilder Dl01Intensity(float value)
        {
            traverse.Field<float>("dl01_Intensity").Value = value;
            return this;
        }
        public ItemFamilyBuilder Dl02Rotation(Vector3 value)
        {
            traverse.Field<Vector3>("dl02_Rotation").Value = value;
            return this;
        }
        public ItemFamilyBuilder Dl02Color(Color value)
        {
            traverse.Field<Color>("dl02_Color").Value = value;
            return this;
        }
        public ItemFamilyBuilder Dl02Intensity(float value)
        {
            traverse.Field<float>("dl02_Intensity").Value = value;
            return this;
        }

        public SoItemFamily Build()
        {
            return family;
        }
    }
}
