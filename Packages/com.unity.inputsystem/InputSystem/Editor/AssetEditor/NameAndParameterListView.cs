#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Experimental.Input.Utilities;

namespace UnityEngine.Experimental.Input.Editor.Lists
{
    /// <summary>
    /// A <see cref="ReorderableList"/> to manage a set of name-and-parameter pairs and a <see cref="ParameterListView"/>
    /// to edit the parameters of the currently selected pair.
    /// </summary>
    /// <remarks>
    /// Produces output that can be consumed by <see cref="NameAndParameters.ParseMultiple"/>.
    /// </remarks>
    internal abstract class NameAndParameterListView
    {
        protected NameAndParameterListView(SerializedProperty property, Action applyAction, string expectedControlLayout)
        {
            m_Property = property;
            m_Apply = applyAction;
            m_ListItems = new List<string>();
            m_ListOptions = GetOptions();
            m_EditableParametersForSelectedItem = new ParameterListView {onChange = OnParametersChanged};
            m_ParametersForEachListItem = NameAndParameters.ParseMultiple(m_Property.stringValue).ToArray();
            m_ExpectedControlLayout = expectedControlLayout;
            Type expectedValueType = null;
            if (!string.IsNullOrEmpty(m_ExpectedControlLayout))
                expectedValueType = EditorInputControlLayoutCache.GetValueType(m_ExpectedControlLayout);

            foreach (var nameAndParams in m_ParametersForEachListItem)
            {
                var name = ObjectNames.NicifyVariableName(nameAndParams.name);

                ////REVIEW: finding this kind of stuff should probably have better support globally on the asset; e.g. some
                ////        notification that pops up and allows fixing all occurrences in one click
                // Find out if we still support this option and indicate it in the list, if we don't.
                var type = m_ListOptions.LookupTypeRegistration(new InternedString(nameAndParams.name));
                if (type == null)
                    name += " (Obsolete)";
                else if (expectedValueType != null)
                {
                    var valueType = GetValueType(type);
                    if (!expectedValueType.IsAssignableFrom(valueType))
                        name += " (Ignored)";
                }

                m_ListItems.Add(name);
            }

            m_ListView = new ReorderableList(m_ListItems, typeof(string))
            {
                headerHeight = 3,
                onAddDropdownCallback = (rect, list) =>
                {
                    // Add only original names to the menu and not aliases.
                    var menu = new GenericMenu();
                    foreach (var name in m_ListOptions.internedNames.Where(x => !m_ListOptions.aliases.Contains(x)).OrderBy(x => x.ToString()))
                    {
                        // Skip if not compatible with value type.
                        if (expectedValueType != null)
                        {
                            var type = m_ListOptions.LookupTypeRegistration(name);
                            var valueType = GetValueType(type);
                            if (valueType != null && !expectedValueType.IsAssignableFrom(valueType))
                                continue;
                        }

                        var niceName = ObjectNames.NicifyVariableName(name);
                        menu.AddItem(new GUIContent(niceName), false, OnAddElement, name.ToString());
                    }
                    menu.ShowAsContext();
                },
                onRemoveCallback = list =>
                {
                    var index = list.index;
                    list.list.RemoveAt(index);
                    ArrayHelpers.EraseAt(ref m_ParametersForEachListItem, index);
                    m_EditableParametersForSelectedItem.Clear();
                    m_Apply();
                    list.index = -1;
                },
                onReorderCallbackWithDetails = (list, oldIndex, newIndex) =>
                {
                    MemoryHelpers.Swap(ref m_ParametersForEachListItem[oldIndex],
                        ref m_ParametersForEachListItem[newIndex]);
                    OnSelection(list);
                    m_Apply();
                },
                onSelectCallback = OnSelection
            };
        }

        protected abstract TypeTable GetOptions();
        protected abstract Type GetValueType(Type type);

        private void OnAddElement(object data)
        {
            var name = (string)data;

            m_ListItems.Add(ObjectNames.NicifyVariableName(name));
            ArrayHelpers.Append(ref m_ParametersForEachListItem,
                new NameAndParameters {name = name});
            m_Apply();
        }

        private void OnParametersChanged()
        {
            var selected = m_ListView.index;
            if (selected < 0)
                return;

            m_ParametersForEachListItem[selected] = new NameAndParameters
            {
                name = m_ParametersForEachListItem[selected].name,
                parameters = m_EditableParametersForSelectedItem.GetParameters(),
            };

            m_Apply();
        }

        private void OnSelection(ReorderableList list)
        {
            var index = list.index;
            if (index < 0)
            {
                m_EditableParametersForSelectedItem.Clear();
                return;
            }

            var typeName = m_ParametersForEachListItem[index].name;
            var rowType =  m_ListOptions.LookupTypeRegistration(typeName);

            m_EditableParametersForSelectedItem.Initialize(rowType, m_ParametersForEachListItem[index].parameters);
        }

        public void OnGUI()
        {
            // Use for debugging
            // EditorGUILayout.LabelField(m_Property.stringValue);

            var listRect = GUILayoutUtility.GetRect(200, m_ListView.GetHeight());
            listRect = EditorGUI.IndentedRect(listRect);
            m_ListView.DoList(listRect);
            if (m_EditableParametersForSelectedItem.hasUIToShow)
            {
                var label = $"Parameters for {ObjectNames.NicifyVariableName(m_ParametersForEachListItem[m_ListView.index].name)}";
                EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                m_EditableParametersForSelectedItem.OnGUI();
                EditorGUI.indentLevel--;
            }
        }

        public string ToSerializableString()
        {
            if (m_ParametersForEachListItem == null)
                return string.Empty;

            return string.Join(NamedValue.Separator,
                m_ParametersForEachListItem.Select(x => x.ToString()).ToArray());
        }

        private readonly List<string> m_ListItems;
        private readonly ReorderableList m_ListView;
        private SerializedProperty m_Property;
        private TypeTable m_ListOptions;
        private string m_ExpectedControlLayout;

        private NameAndParameters[] m_ParametersForEachListItem;
        private readonly ParameterListView m_EditableParametersForSelectedItem;
        private readonly Action m_Apply;
    }

    /// <summary>
    /// A list of processors and their parameters.
    /// </summary>
    internal class ProcessorsListView : NameAndParameterListView
    {
        public ProcessorsListView(SerializedProperty property, Action applyAction, string expectedControlLayout)
            : base(property, applyAction, expectedControlLayout)
        {
        }

        protected override TypeTable GetOptions()
        {
            return InputProcessor.s_Processors;
        }

        protected override Type GetValueType(Type type)
        {
            return InputProcessor.GetValueTypeFromType(type);
        }
    }

    /// <summary>
    /// A list view of interactions and their parameters.
    /// </summary>
    internal class InteractionsListView : NameAndParameterListView
    {
        public InteractionsListView(SerializedProperty property, Action applyAction, string expectedControlLayout)
            : base(property, applyAction, expectedControlLayout)
        {
        }

        protected override TypeTable GetOptions()
        {
            return InputInteraction.s_Interactions;
        }

        protected override Type GetValueType(Type type)
        {
            return InputInteraction.GetValueType(type);
        }
    }
}
#endif // UNITY_EDITOR
