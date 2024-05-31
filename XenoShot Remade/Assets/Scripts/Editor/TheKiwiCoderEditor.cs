using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;
using System;

public class TheKiwiCoderEditor : EditorWindow
{
    public GameObject characterModel;
    public ThirdPersonConfiguration configuration;
    Editor configurationEditor;

    string validationMessage;
    const string sRigLayersName = "----- RigLayers -----";
    const string sCameraLayersName = "----- Cameras ------";
    Texture icon;

    GUIStyle headingStyle;
    GUIStyle subHeadingStyle;
    GUIStyle subSubHeadingStyle;
    GUIStyle linkStyle;

    [MenuItem("TheKiwiCoder/Create Character ...")]
    public static void ShowWindow() {
        TheKiwiCoderEditor window = GetWindow(typeof(TheKiwiCoderEditor)) as TheKiwiCoderEditor;
        window.titleContent = new GUIContent("Character Creator");
        window.LoadDefaultConfiguration();
    }

    void LoadDefaultConfiguration() {
        configuration = EditorUtils.LoadAsset<ThirdPersonConfiguration>("Assets/Data/Editor/default_character_configuration.asset");
        EditorUtils.CreateEditor(configuration, ref configurationEditor);
    }

    bool ValidateConfiguration() {
        if (configuration == null) {
            validationMessage = "Editor Configuration object not set. Assign a third person configuration, or LoadDefaults.";
            return false;
        }

        if (characterModel == null) {
            validationMessage = "Character model not assigned. Please assign a character model.";
            return false;
        }

        if (characterModel.GetComponent<Animator>() == null ||
            !characterModel.GetComponent<Animator>().isHuman) {
            validationMessage = "Character model assigned is not humanoid. Please convert to humanoid.";
            return false;
        }

        if (configuration.animationRig == null) {
            validationMessage = "Please assign an animation rig.";
            return false;
        }

        if (configuration.cameraRig == null) {
            validationMessage = "Please assign a camera rig.";
            return false;
        }

        return true;
    }

    void CreateStyles() {
        if (icon == null) {
            icon = AssetDatabase.LoadAssetAtPath("Assets/Textures/Editor/tex_thekiwicoder.png", typeof(Texture)) as Texture;
        }

        if (headingStyle == null) {
            headingStyle = new GUIStyle(EditorStyles.boldLabel);
            headingStyle.fontSize = 20;
            headingStyle.alignment = TextAnchor.UpperLeft;
        }

        if (subHeadingStyle == null) {
            subHeadingStyle = new GUIStyle(EditorStyles.label);
            subHeadingStyle.fontSize = 15;
            subHeadingStyle.fontStyle = FontStyle.Normal;
            subHeadingStyle.alignment = TextAnchor.LowerLeft;
            subHeadingStyle.wordWrap = true;
        }

        if (subSubHeadingStyle == null) {
            subSubHeadingStyle = new GUIStyle(EditorStyles.label);
            subSubHeadingStyle.fontSize = 15;
            subSubHeadingStyle.fontStyle = FontStyle.Bold;
            subSubHeadingStyle.alignment = TextAnchor.LowerLeft;
            subSubHeadingStyle.wordWrap = true;
        }

        if (linkStyle == null) {
            linkStyle = new GUIStyle(EditorStyles.linkLabel);
        }
    }
    
    void HorizontalLine(int i_height = 1) {
        EditorGUILayout.Space(2);
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.Space(2);
    }

    void DrawHeader() {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Box(icon);
        
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("TheKiwiCoder", headingStyle, GUILayout.MinHeight(20.0f));
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("Third Person Creator", subHeadingStyle);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }

    void OnGUI() {
        CreateStyles();

        DrawHeader();
        
        if (GUILayout.Button("youtube.com/thekiwicoder", linkStyle)) {
            Application.OpenURL("https://www.youtube.com/channel/UCjszZMwnOW4fO5VIDU_Wh1Q");
        }

        HorizontalLine(1);

        EditorGUILayout.LabelField("Character Configuration", subSubHeadingStyle);
        EditorGUILayout.Space(2);
        configuration = EditorGUILayout.ObjectField(configuration, typeof(ThirdPersonConfiguration), false) as ThirdPersonConfiguration;
        if (!configurationEditor || GUI.changed) {
            EditorUtils.CreateEditor(configuration, ref configurationEditor);
        }

        if (configurationEditor) {
            configurationEditor.OnInspectorGUI();
        }

        // Character configuration
        bool validConfiguration = ValidateConfiguration();
         
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Assign Character", subSubHeadingStyle);

        characterModel = EditorGUILayout.ObjectField("Character Model", characterModel, typeof(GameObject), false) as GameObject;

        // HELP!
        EditorGUILayout.Space(5);
        if (!validConfiguration) {
            EditorGUILayout.HelpBox(validationMessage, MessageType.Error);
        } else {
            validationMessage = "Press Create Character button below!";
            EditorGUILayout.HelpBox(validationMessage, MessageType.Info);
        }

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Load Defaults")) {
            LoadDefaultConfiguration();
        }

        GUI.enabled = validConfiguration;
        if (GUILayout.Button("Create Character")) {

            // Create base objects
            GameObject character = CreateCharacter();

            // Add required components
            ConfigureAnimator(character);
            ConfigureAnimationRigging(character);
            ConfigureCharacterAiming(character);
            ConfigureCharacterLocomotion(character);
            ConfigureActiveWeapon(character);
            ConfigureReloadWeapon(character);
            ConfigureCharacterController(character);
            ConfigurePlayerHealth(character);
            ConfigureRagdoll(character);
            Selection.activeObject = character;
        }
        GUI.enabled = true;
    }

    Vector3 CalcSpawnPosition() {
        SceneView view = SceneView.lastActiveSceneView;
        Camera cam = view.camera;
        RaycastHit info;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out info, 5)) {
            return info.point;
        }

        Vector3 position = cam.transform.position + cam.transform.forward * 5;
        if (Physics.Raycast(position, Vector3.down, out info)) {
            position = info.point;
        }
        return position;
    }

    Quaternion CalcSpawnRotation() {
        SceneView view = SceneView.lastActiveSceneView;
        Camera cam = view.camera;
        float cameraY = cam.transform.eulerAngles.y;
        cameraY = Mathf.RoundToInt(cameraY / 90.0f) * 90;
        return Quaternion.AngleAxis(cameraY, Vector3.up);
    }

    GameObject CreateCharacter() {

        Vector3 position = CalcSpawnPosition();
        Quaternion rotation = CalcSpawnRotation();

        GameObject character = (GameObject)PrefabUtility.InstantiatePrefab(characterModel);
        character.transform.position = position;
        character.transform.rotation = rotation;
        character.name = "Character ThirdPerson (" + characterModel.name + ")";
        character.tag = "Player";
        character.layer = LayerMask.NameToLayer("Character");

        RagdollBuilder builder = new RagdollBuilder();
        builder.humanoidRoot = character.transform;
        builder.Build();

        EditorUtils.InstantiateWithName(configuration.cameraRig, character.transform, sCameraLayersName);
        EditorUtils.InstantiateWithName(configuration.animationRig, character.transform, sRigLayersName);
        return character;
    }

    private void ConfigureCharacterController(GameObject character) {
        CharacterController controller = EditorUtils.SetComponent<CharacterController>(character);
        controller.minMoveDistance = 0;

        SkinnedMeshRenderer meshRenderer = character.GetComponentInChildren<SkinnedMeshRenderer>();
        float top = meshRenderer.bounds.max.y;
        float bottom = meshRenderer.bounds.min.y;
        float left = meshRenderer.bounds.min.x;
        float right = meshRenderer.bounds.max.x;

        controller.height = top - bottom;
        controller.radius = ((right - left) * 0.5f) * 0.3f; // 0.3f is because characters are usually in t-pose
        controller.center = new Vector3(0, controller.height * 0.5f + controller.skinWidth, 0);
    }

    private void ConfigurePlayerHealth(GameObject character) {
        EditorUtils.SetComponent<PlayerHealth>(character);
    }

    private void ConfigureRagdoll(GameObject character) {
        EditorUtils.SetComponent<Ragdoll>(character);
    }

    Animator FindRigController(GameObject character) {
        return EditorUtils.FindChild(character.transform, sRigLayersName).GetComponent<Animator>();
    }

    private void ConfigureReloadWeapon(GameObject character) {
        ReloadWeapon reload = EditorUtils.SetComponent<ReloadWeapon>(character);
        reload.animator = FindRigController(character);
        reload.animationEvents = character.GetComponentInChildren<WeaponAnimationEvents>();
        reload.leftHand = character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand);
    }

    private void ConfigureActiveWeapon(GameObject character) {
        ActiveWeapon weapon = EditorUtils.SetComponent<ActiveWeapon>(character);
        weapon.rigController = FindRigController(character);
        weapon.weaponSlots = new Transform[] {
            EditorUtils.FindChild(character.transform, "PrimaryWeapon"),
            EditorUtils.FindChild(character.transform, "SecondaryWeapon") 
        };
    }

    private void ConfigureCharacterLocomotion(GameObject character) {
        CharacterLocomotion locomotion = EditorUtils.SetComponent<CharacterLocomotion>(character);
        locomotion.rigController = FindRigController(character);
    }

    private void ConfigureCharacterAiming(GameObject character) {
        CharacterAiming aiming = EditorUtils.SetComponent<CharacterAiming>(character);
        aiming.cameraLookAt = EditorUtils.FindChild(character.transform, "CameraLookAt");
    }

    void ConfigureAnimator(GameObject go) {
        Animator animator = EditorUtils.SetComponent<Animator>(go);
        animator.runtimeAnimatorController = configuration.controller;
    }

    void ConfigureAnimationRigging(GameObject character) {
        // Rig builder
        RigBuilder rigBuilder = EditorUtils.SetComponent<RigBuilder>(character);
        rigBuilder.layers.Clear();
        Rig[] rigs = character.GetComponentsInChildren<Rig>();
        foreach(var rig in rigs) {
            RigLayer rigLayer = new RigLayer(rig);
            rigBuilder.layers.Add(rigLayer);
        }

        Animator animator = character.GetComponent<Animator>();
        animator.Update(0.0f);
        Transform aimLookAt = EditorUtils.FindChild(character.transform, "AimLookAt");

        {
            MultiAimConstraint constraint = EditorUtils.FindChildType<MultiAimConstraint>(character.transform, "AimSpine1");
            Transform boneTransform = animator.GetBoneTransform(HumanBodyBones.Spine);
            constraint.SetConstrained(boneTransform);
            constraint.SetSource(aimLookAt);
            constraint.data.aimAxis = FindForwardAxis(character.transform, boneTransform);
        }

        {
            MultiAimConstraint constraint = EditorUtils.FindChildType<MultiAimConstraint>(character.transform, "AimSpine2");
            Transform boneTransform = animator.GetBoneTransform(HumanBodyBones.Spine).GetChild(0);
            constraint.SetConstrained(boneTransform);
            constraint.SetSource(aimLookAt);
            constraint.data.aimAxis = FindForwardAxis(character.transform, boneTransform);
        }

        {
            OverrideTransform constraint = EditorUtils.FindChildType<OverrideTransform>(character.transform, "BodyRecoil");
            constraint.SetConstrained(animator.GetBoneTransform(HumanBodyBones.RightShoulder));
        }

        {
            MultiAimConstraint constraint = EditorUtils.FindChildType<MultiAimConstraint>(character.transform, "AimHead");
            Transform boneTransform = animator.GetBoneTransform(HumanBodyBones.Head);
            constraint.SetConstrained(boneTransform);
            constraint.SetSource(aimLookAt);
            constraint.data.aimAxis = FindForwardAxis(character.transform, boneTransform);
        }

        {
            MultiParentConstraint constraint = EditorUtils.FindChildType<MultiParentConstraint>(character.transform, "WeaponSlot_Primary");            
            constraint.SetSource(animator.GetBoneTransform(HumanBodyBones.Spine).GetChild(0));
        }

        {
            MultiParentConstraint constraint = EditorUtils.FindChildType<MultiParentConstraint>(character.transform, "WeaponSlot_Secondary");
            constraint.SetSource(animator.GetBoneTransform(HumanBodyBones.Hips));
        }

        {
            MultiPositionConstraint constraint = EditorUtils.FindChildType<MultiPositionConstraint>(character.transform, "WeaponPose");
            constraint.SetSource(animator.GetBoneTransform(HumanBodyBones.RightShoulder));
        }

        {
            MultiAimConstraint constraint = EditorUtils.FindChildType<MultiAimConstraint>(character.transform, "WeaponPose");
            constraint.SetSource(aimLookAt);
        }

        {
            TwoBoneIKConstraint constraint = EditorUtils.FindChildType<TwoBoneIKConstraint>(character.transform, "RightHandIK");
            constraint.data.root = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
            constraint.data.mid = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
            constraint.data.tip = animator.GetBoneTransform(HumanBodyBones.RightHand);
        }

        {
            TwoBoneIKConstraint constraint = EditorUtils.FindChildType<TwoBoneIKConstraint>(character.transform, "LeftHandIk");
            constraint.data.root = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            constraint.data.mid = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            constraint.data.tip = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        }
    }

    MultiAimConstraintData.Axis FindForwardAxis(Transform transform, Transform boneTransform) {

        MultiAimConstraintData.Axis[] axis = {
            MultiAimConstraintData.Axis.Z,
            MultiAimConstraintData.Axis.Z_NEG,
            MultiAimConstraintData.Axis.X,
            MultiAimConstraintData.Axis.X_NEG,
            MultiAimConstraintData.Axis.Y,
            MultiAimConstraintData.Axis.Y_NEG
        };

        float[] angles = {
            Vector3.Angle(transform.forward, boneTransform.transform.forward),
            Vector3.Angle(transform.forward, -boneTransform.transform.forward),
            Vector3.Angle(transform.forward, boneTransform.transform.right),
            Vector3.Angle(transform.forward, -boneTransform.transform.right),
            Vector3.Angle(transform.forward, boneTransform.transform.up),
            Vector3.Angle(transform.forward, -boneTransform.transform.up)
        };

        int smallestIndex = 0;
        for (int i = 1; i < angles.Length; ++i) {
            if (angles[i] < angles[smallestIndex]) {
                smallestIndex = i;
            }
        }

        return axis[smallestIndex];
    }
}
