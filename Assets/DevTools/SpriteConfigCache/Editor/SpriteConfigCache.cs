using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DevTools
{
    public class SpriteConfigCache : ScriptableObject
    {
        public SpriteMetaData[] spritesheet;
        public TextureImporterSettings textureSettings;
        public TextureImporterPlatformSettings iosSettings;
        public TextureImporterPlatformSettings androidSettings;
        public TextureImporterPlatformSettings standaloneSettings;
        public bool isFilled { get; private set; }

        public void SetSpriteSheet(SpriteMetaData[] spritesheet)
        {
            this.spritesheet = spritesheet;
            Save();
        }

        public void SetTextureSettings(TextureImporterSettings settings)
        {
            textureSettings = settings;
            Save();
        }

        public void SetIosSettings(TextureImporterPlatformSettings settings)
        {
            iosSettings = settings;
            Save();
        }

        public void SetAndroidSettings(TextureImporterPlatformSettings settings)
        {
            androidSettings = settings;
            Save();
        }

        public void SetStandaloneSettings(TextureImporterPlatformSettings settings)
        {
            standaloneSettings = settings;
            Save();
        }

        void Save()
        {
            isFilled = true;
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}

