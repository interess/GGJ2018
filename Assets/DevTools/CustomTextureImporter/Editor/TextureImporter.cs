using System.IO;
using UnityEditor;
using UnityEngine;

namespace DevTools
{
    class CustomTextureImporter : AssetPostprocessor
    {
        void OnPreprocessTexture()
        {
            var configsPrefix = "-config-";
            var configsSuffix = "-assets";
            var assetsSuffix = "-assets";
            var assetExtension = ".asset";

            var myTextureImporter = (TextureImporter) assetImporter;

            if (myTextureImporter == null) { return; }

            var sprite = assetPath.Contains("Sprites/") && assetPath.Contains(assetsSuffix);

            if (sprite)
            {
                var textureSettings = new TextureImporterSettings();
                myTextureImporter.ReadTextureSettings(textureSettings);

                var indexOfCategory = 0;
                var localTexturePath = assetPath;
                var localTexturePathArr = localTexturePath.Split('/');
                var localConfigsDirectoryPath = "";
                var globalConfigsDirectoryPath = "";
                var localConfigPath = "";
                var globalConfigPath = "";
                var configCache = default(SpriteConfigCache);
                var isValid = false;

                for (int i = 0; i < localTexturePathArr.Length; i++)
                {
                    if (localTexturePathArr[i].Contains(assetsSuffix))
                    {
                        indexOfCategory = i;

                        var regex = new System.Text.RegularExpressions.Regex("-assets");
                        var psdFileName = regex.Split(localTexturePathArr[i]) [0];
                        localConfigsDirectoryPath = localConfigsDirectoryPath + configsPrefix + psdFileName + configsSuffix;

                        if (localTexturePathArr.Length < i + 1)
                        {
                            Debug.LogError("Sprite path has unsopported format: " + localTexturePath);
                            isValid = false;
                            break;
                        }

                        var restOfPath = "";
                        var configFileName = "";

                        for (int n = i + 1; n < localTexturePathArr.Length; n++)
                        {
                            if (n == localTexturePathArr.Length - 1)
                            {
                                configFileName = localTexturePathArr[n];
                                configFileName = configFileName.Split('.') [0] + assetExtension;
                            }
                            else
                            {
                                restOfPath += Path.DirectorySeparatorChar + localTexturePathArr[n];
                            }
                        }

                        globalConfigsDirectoryPath = Path.GetFullPath(localConfigsDirectoryPath);

                        localConfigPath = Path.Combine(localConfigsDirectoryPath + restOfPath, configFileName);
                        globalConfigPath = Path.Combine(globalConfigsDirectoryPath + restOfPath, configFileName);

                        if (!Directory.Exists(globalConfigsDirectoryPath + restOfPath))
                        {
                            Directory.CreateDirectory(globalConfigsDirectoryPath + restOfPath);
                        }

                        if (!File.Exists(globalConfigPath))
                        {
                            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(typeof(SpriteConfigCache)), localConfigPath);
                        }

                        configCache = AssetDatabase.LoadAssetAtPath<SpriteConfigCache>(localConfigPath);

                        if (configCache == null)
                        {
                            Debug.LogError("Could not create asset for " + localConfigPath);
                            isValid = false;
                            break;
                        }

                        isValid = true;
                    }
                    else
                    {
                        localConfigsDirectoryPath += localTexturePathArr[i] + Path.DirectorySeparatorChar;
                    }
                }

                if (isValid)
                {

                    // If empty - it's a new texture. Need to import settings
                    if (string.IsNullOrEmpty(myTextureImporter.spritePackingTag) && configCache.isFilled)
                    {
                        myTextureImporter.spritesheet = configCache.spritesheet;
                        myTextureImporter.SetPlatformTextureSettings(configCache.iosSettings);
                        myTextureImporter.SetPlatformTextureSettings(configCache.androidSettings);
                        myTextureImporter.SetPlatformTextureSettings(configCache.standaloneSettings);
                        textureSettings = configCache.textureSettings;
                        myTextureImporter.spritePackingTag = "__imported__";
                    }
                    else
                    {
                        configCache.SetSpriteSheet(myTextureImporter.spritesheet);
                        configCache.SetTextureSettings(textureSettings);
                        configCache.SetIosSettings(myTextureImporter.GetPlatformTextureSettings("iPhone"));
                        configCache.SetAndroidSettings(myTextureImporter.GetPlatformTextureSettings("Android"));
                        configCache.SetStandaloneSettings(myTextureImporter.GetPlatformTextureSettings("Standalone"));
                        myTextureImporter.spritePackingTag = "__imported__";
                    }

                }
                else
                {
                    Debug.LogError("TextureImporter failed. Config was not created for sprite: " + localTexturePath);
                }

                // Save property
                myTextureImporter.SetTextureSettings(textureSettings);
            }
        }
    }
}
