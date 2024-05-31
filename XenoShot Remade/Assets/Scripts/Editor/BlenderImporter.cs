using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class BlenderImporter : AssetPostprocessor
{
    // Disabling automatic blender settings for now.
    // It's a bit hard to come up with rules when using disparate 
    // assets from multiple packages.
    // For now these settings can just be set manually as usual :)

    /*void OnPreprocessAnimation() {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        var sourceAnimations = modelImporter.defaultClipAnimations;
        if (sourceAnimations.Length == 0) {
            return;
        }
        
        foreach (var clip in sourceAnimations) {
            if (!clip.name.Contains("character_")) {
                continue;
            }

            clip.loopTime = true;
            clip.keepOriginalPositionY = true;
            clip.keepOriginalOrientation = true;
            clip.keepOriginalPositionXZ = true;

            clip.lockRootRotation = true;
            clip.lockRootHeightY = true;
        }
        modelImporter.clipAnimations = sourceAnimations;
    }

    void OnPreprocessModel() {
        ModelImporter modelImporter = assetImporter as ModelImporter;
        if (assetPath.Contains("character")) {
            modelImporter.animationType = ModelImporterAnimationType.Human;
        }
        modelImporter.animationCompression = ModelImporterAnimationCompression.Off;
    }*/
}
#endif