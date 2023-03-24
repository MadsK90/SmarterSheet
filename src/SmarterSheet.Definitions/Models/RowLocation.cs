namespace SmarterSheet.Definitions.Models;

public enum RowLocation
{
    ToBottom = 1,       // Bottom of sheet
    ToTop = 2,          // Top of sheet
    Parent = 3,         // Top of an indented section aka first child row
    ParentBottom = 4,   // Bottom of an indented section aka last child row
    Sibling = 5,        // Below a specific row at the same indent level
    SiblingAbove = 6,   // Above the specific row at the same ident level
    Indent = 7,         // Indent one existing row, must have a value of "1"
    Outdent = 8,        // Outdent one existing row must have value of "1"
}