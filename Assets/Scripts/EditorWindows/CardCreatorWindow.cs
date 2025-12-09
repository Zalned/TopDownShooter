//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEditor;
//using UnityEngine;

//public class CardCreatorWindow : EditorWindow {
//    private string _cardName = "New Card";
//    private string _description = "";
//    private string _statDescription = "";

//    private List<ModSO> _mods = new();
//    private Vector2 _scroll;

//    private GameObject _cardPrefabTemplate;
//    private string _createPath = "Assets/Cards";

//    private Type[] _availableModTypes;
//    private int _selectedModTypeIndex = 0;

//    [MenuItem( "Tools/Card Creator" )]
//    public static void OpenWindow() {
//        var window = GetWindow<CardCreatorWindow>( "Card Creator" );
//        window.minSize = new Vector2( 520, 320 );
//        window.LoadModTypes();
//    }

//    private void OnEnable() {
//        LoadModTypes();
//    }

//    private void LoadModTypes() {
//        var modBase = typeof( ModSO );
//        var all = AppDomain.CurrentDomain.GetAssemblies()
//            .SelectMany( a => a.GetTypes() )
//            .Where( t => modBase.IsAssignableFrom( t ) && !t.IsAbstract && t.IsClass )
//            .ToArray();
//        _availableModTypes = all;
//        if ( _availableModTypes.Length == 0 ) _availableModTypes = new Type[ 0 ];
//    }

//    private void OnGUI() {
//        EditorGUILayout.LabelField( "Card Creator", EditorStyles.boldLabel );
//        EditorGUILayout.Space();

//        using ( var scope = new EditorGUILayout.HorizontalScope() ) {
//            _createPath = EditorGUILayout.TextField( "Create Path", _createPath );
//            if ( GUILayout.Button( "Browse", GUILayout.Width( 70 ) ) ) {
//                var path = EditorUtility.OpenFolderPanel( "Select Create Folder", "Assets", "" );
//                if ( !string.IsNullOrEmpty( path ) && path.StartsWith( Application.dataPath ) ) {
//                    _createPath = "Assets" + path.Substring( Application.dataPath.Length );
//                }
//            }
//        }

//        EditorGUILayout.Space();

//        _cardName = EditorGUILayout.TextField( "Card Name", _cardName );
//        _description = EditorGUILayout.TextField( "Description", _description );
//        _statDescription = EditorGUILayout.TextField( "Stat Description", _statDescription );

//        EditorGUILayout.Space();
//        EditorGUILayout.LabelField( "Mods", EditorStyles.boldLabel );

//        _scroll = EditorGUILayout.BeginScrollView( _scroll, GUILayout.Height( 120 ) );
//        for ( int i = 0; i < _mods.Count; i++ ) {
//            using ( new EditorGUILayout.HorizontalScope() ) {
//                _mods[ i ] = (ModSO)EditorGUILayout.ObjectField( _mods[ i ], typeof( ModSO ), false );
//                if ( GUILayout.Button( "X", GUILayout.Width( 24 ) ) ) {
//                    _mods.RemoveAt( i );
//                    i--;
//                    continue;
//                }
//            }
//        }
//        EditorGUILayout.EndScrollView();

//        using ( new EditorGUILayout.HorizontalScope() ) {
//            if ( _availableModTypes.Length > 0 ) {
//                string[] names = _availableModTypes.Select( t => t.Name ).ToArray();
//                _selectedModTypeIndex = EditorGUILayout.Popup( _selectedModTypeIndex, names );
//                if ( GUILayout.Button( "Create Mod", GUILayout.Width( 120 ) ) ) {
//                    CreateNewModAsset( _availableModTypes[ _selectedModTypeIndex ] );
//                }
//            } else {
//                EditorGUILayout.LabelField( "No ModSO subclasses found in project." );
//            }

//            if ( GUILayout.Button( "Add Existing Mod (ObjectField)", GUILayout.Width( 260 ) ) ) {
//                var path = EditorUtility.OpenFilePanel( "Select ModSO asset", Application.dataPath, "asset" );
//                if ( !string.IsNullOrEmpty( path ) && path.StartsWith( Application.dataPath ) ) {
//                    var rel = "Assets" + path.Substring( Application.dataPath.Length );
//                    var asset = AssetDatabase.LoadAssetAtPath<ModSO>( rel );
//                    if ( asset != null ) _mods.Add( asset );
//                }
//            }
//        }

//        EditorGUILayout.Space();
//        EditorGUILayout.LabelField( "Prefab (Card View Template)", EditorStyles.boldLabel );
//        _cardPrefabTemplate = (GameObject)EditorGUILayout.ObjectField( _cardPrefabTemplate, typeof( GameObject ), false );

//        EditorGUILayout.Space();
//        using ( new EditorGUILayout.HorizontalScope() ) {
//            if ( GUILayout.Button( "Create CardSO Asset", GUILayout.Height( 36 ) ) ) CreateCardSoAsset();
//            if ( GUILayout.Button( "Create Prefab With CardSO", GUILayout.Height( 36 ) ) ) CreatePrefabWithCardSo();
//        }

//        EditorGUILayout.Space();
//        EditorGUILayout.HelpBox( "Workflow: create or select ModSO assets, press Create CardSO Asset. Optionally assign a Card prefab and create a prefab instance with the created CardSO assigned.", MessageType.Info );
//    }

//    private void CreateNewModAsset( Type modType ) {
//        var inst = ScriptableObject.CreateInstance( modType ) as ModSO;
//        if ( inst == null ) return;

//        var safeFolder = EnsureFolderExists( _createPath );
//        var assetPath = AssetDatabase.GenerateUniqueAssetPath( $"{safeFolder}/{modType.Name}.asset" );
//        AssetDatabase.CreateAsset( inst, assetPath );
//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();

//        Selection.activeObject = inst;
//        _mods.Add( inst );
//    }

//    private string EnsureFolderExists( string path ) {
//        if ( string.IsNullOrEmpty( path ) ) path = "Assets";
//        if ( !AssetDatabase.IsValidFolder( path ) ) {
//            AssetDatabase.CreateFolder( "Assets", path.TrimStart( '/' ) );
//        }
//        return path;
//    }

//    private void CreateCardSoAsset() {
//        var card = ScriptableObject.CreateInstance<CardSO>();
//        var folder = EnsureFolderExists( _createPath );
//        var assetPath = AssetDatabase.GenerateUniqueAssetPath( $"{folder}/{_cardName}.asset" );
//        AssetDatabase.CreateAsset( card, assetPath );

//        // fill serialized fields
//        var so = new SerializedObject( card );
//        so.FindProperty( "cardName" ).stringValue = _cardName;
//        so.FindProperty( "description" ).stringValue = _description;
//        so.FindProperty( "statDescription" ).stringValue = _statDescription;

//        var modsProp = so.FindProperty( "mods" );
//        modsProp.arraySize = _mods.Count;
//        for ( int i = 0; i < _mods.Count; i++ ) modsProp.GetArrayElementAtIndex( i ).objectReferenceValue = _mods[ i ];
//        so.ApplyModifiedProperties();

//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();

//        EditorUtility.FocusProjectWindow();
//        Selection.activeObject = card;
//        Debug.Log( $"CardSO created at: {assetPath}" );
//    }

//    private void CreatePrefabWithCardSo() {
//        if ( _cardPrefabTemplate == null ) {
//            Debug.LogWarning( "Card prefab template is null" );
//            return;
//        }

//        // Create CardSO first
//        CreateCardSoAsset();

//        // find created asset
//        var created = AssetDatabase.FindAssets( $"t:CardSO {_cardName}" );
//        if ( created == null || created.Length == 0 ) {
//            Debug.LogWarning( "Can't find created CardSO asset" );
//            return;
//        }

//        var path = AssetDatabase.GUIDToAssetPath( created[ 0 ] );
//        var cardSo = AssetDatabase.LoadAssetAtPath<CardSO>( path );
//        if ( cardSo == null ) { Debug.LogWarning( "CardSO not loaded" ); return; }

//        // instantiate template
//        var temp = (GameObject)PrefabUtility.InstantiatePrefab( _cardPrefabTemplate );
//        if ( temp == null ) { Debug.LogWarning( "Failed to instantiate template prefab" ); return; }

//        var cardComp = temp.GetComponent<Card>();
//        if ( cardComp == null ) {
//            Debug.LogWarning( "Prefab template doesn't contain Card component" );
//            DestroyImmediate( temp );
//            return;
//        }

//        // assign via serialized object to private field
//        var so = new SerializedObject( cardComp );
//        var prop = so.FindProperty( "_cardSO" );
//        if ( prop == null ) {
//            Debug.LogWarning( "Card component field '_cardSO' not found (check field name)" );
//            DestroyImmediate( temp );
//            return;
//        }

//        prop.objectReferenceValue = cardSo;
//        so.ApplyModifiedProperties();

//        var folder = EnsureFolderExists( _createPath );
//        var prefabPath = AssetDatabase.GenerateUniqueAssetPath( $"{folder}/{_cardName}.prefab" );
//        PrefabUtility.SaveAsPrefabAsset( temp, prefabPath );
//        DestroyImmediate( temp );

//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();

//        Debug.Log( $"Prefab created at {prefabPath} with CardSO assigned" );
//    }
//}
