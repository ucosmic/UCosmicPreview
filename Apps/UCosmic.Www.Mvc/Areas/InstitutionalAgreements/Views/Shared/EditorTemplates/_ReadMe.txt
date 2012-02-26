Most of the editor template files have been moved out of this folder.

There is now an EditorTemplates folder in controller-specific views folders.

For example, InstitutionalAgreementParticipantForm.cshtml can now be found in ~/Areas/InstitutionalAgreements/Views/ManagementForms/EditorTemplates.

From now on, only put edit templates in this Shared folder if they are used in more than 1 controller category.

For example, the TypeComboBox.cshtml editor template is used as a UIHint in both the manager & configuration forms -- the manager forms show the combobox as an input element, and the configuration forms show the combobox as a preview of what the input element will look like with the current configuration settings.

As a rule of thumb, create editor & display templates in the controller-specific views folder first (~/Areas/AreaName/Views/ControllerName/EditorTemplates).

If you later find out that you could reuse this editor template in a different controller within the same area, move it to this Shared EditorTemplates folder.
