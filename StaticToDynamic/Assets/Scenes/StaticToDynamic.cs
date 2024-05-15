using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

[ShowOdinSerializedPropertiesInInspector]
public class StaticToDynamic : OdinEditorWindow
{
    [MenuItem("Tools/Optimization/ALLDynamicToStatic")]
    private static void DynamicToStaticALL()
    {
        GameObject[] constructable;
        constructable = GameObject.FindGameObjectsWithTag("constructable");

        GameObject[] usual;
        usual = GameObject.FindGameObjectsWithTag("usual");

        //LayerMask usualLayer = -1;

        //string shaderName = "ArcadiaVR.BakeryURPSHSpec";

        //string shaderName02 = "ArcadiaVR.Lit.Transparent";

        foreach (GameObject item in constructable)
        {
            var rend = item.GetComponent<Renderer>();

            StaticEditorFlags disableflag = GameObjectUtility.GetStaticEditorFlags(item);
            disableflag = disableflag & ~(StaticEditorFlags.ContributeGI | StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic |
            StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic);
            GameObjectUtility.SetStaticEditorFlags(item, disableflag);

            rend.shadowCastingMode = ShadowCastingMode.Off;

            rend.enabled = false;

            /*
            if (xxxLayer == (xxxLayer | (1 << item.layer)) && rend.sharedMaterial.shader.name == shaderName02 && rend != null)  
            {
                rend.shadowCastingMode = ShadowCastingMode.Off;
            }
            */
        }

        foreach (GameObject item in usual)
        {
            var rend = item.GetComponent<Renderer>();

            rend.enabled = true;

            var enableflag = StaticEditorFlags.ContributeGI | StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic |
            StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic;
            GameObjectUtility.SetStaticEditorFlags(item, enableflag);

            rend.shadowCastingMode = ShadowCastingMode.Off;

            /*
            if (xxxLayer == (xxxLayer | (1 << item.layer)) && rend.sharedMaterial.shader.name == shaderName && rend != null)  
            {
                rend.shadowCastingMode = ShadowCastingMode.Off;
            }
            */
        }

        EditorSceneManager.MarkAllScenesDirty();
    }

    [MenuItem("Tools/Optimization/ALLStaticToDynamic")]
    private static async void StaticToDynamicAll()
    {
        GameObject[] constructable;
        constructable = GameObject.FindGameObjectsWithTag("constructable");

        GameObject[] usual;
        usual = GameObject.FindGameObjectsWithTag("usual");

        foreach (GameObject item in constructable)
        {
            var rend = item.GetComponent<Renderer>();

            StaticEditorFlags disableflag = GameObjectUtility.GetStaticEditorFlags(item);
            disableflag = disableflag & ~(StaticEditorFlags.ContributeGI | StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic |
            StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic);
            GameObjectUtility.SetStaticEditorFlags(item, disableflag);

            rend.enabled = true;
        }

        foreach (GameObject item in usual)
        {
            var rend = item.GetComponent<Renderer>();

            rend.enabled = false;

            rend.shadowCastingMode = ShadowCastingMode.On;
        }

        EditorSceneManager.MarkAllScenesDirty();
    }
}
