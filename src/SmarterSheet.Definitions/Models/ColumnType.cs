namespace SmarterSheet.Definitions.Models;

[EnumExtensions, JsonConverter(typeof(ColumnTypeConverter))]
public enum ColumnType
{
    NULL,

    [Display(Name = "ABSTRACT_DATETIME")]
    AbstractDateTime,

    [Display(Name = "CHECKBOX")]
    CheckBox,

    [Display(Name = "CONTACT_LIST")]
    ContactList,

    [Display(Name = "DATE")]
    Date,

    [Display(Name = "DATETIME")]
    DateTime,

    [Display(Name = "DURATION")]
    Duration,

    [Display(Name = "MULTI_CONTACT_LIST")]
    MultiContactList,

    [Display(Name = "MULTI_PICKLIST")]
    MultiPickList,

    [Display(Name = "PICKLIST")]
    PickList,

    [Display(Name = "PREDECESSOR")]
    Predecessor,

    [Display(Name = "TEXT_NUMBER")]
    TextNumber
}