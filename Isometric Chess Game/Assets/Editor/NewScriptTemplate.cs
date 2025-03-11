using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

using UnityEditor;

namespace CheckmateInnovations
{
    public static class NewScriptTemplate
    {
        [MenuItem("Assets/Create/Code/CheckmateBehavior", priority = 40)]
        public static void CreateMonoBehaviourMenuItem()
        {
            string templatePath = "Assets/Editor Settings/Templates/Monobehaviour.cs.txt";

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "NewScript.cs");
        }
    }
}
