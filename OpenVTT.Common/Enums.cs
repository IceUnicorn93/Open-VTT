namespace OpenVTT.Common
{
    [Documentation("To use this Object use var dt = DisplayType.Player;", Name = "DisplayType")]
    public enum DisplayType
    {
        [Documentation("Used for the PlayerScreen", Name = "Player", IsField = true, DataType = "DisplayType")]
        Player,
        [Documentation("Used for the Player ArtworkDisplay", Name = "InformationDisplayPlayer", IsField = true, DataType = "DisplayType")]
        InformationDisplayPlayer,
        [Documentation("Used for the DM ArtworkDisplay", Name = "InformationDisplayDM", IsField = true, DataType = "DisplayType")]
        InformationDisplayDM
    }
    internal enum PictureBoxMode
    {
        Rectangle,
        Poligon,
        Ping
    }
    [Documentation("To use this Object use var fs = FogState.Add;", Name = "FogState")]
    public enum FogState
    {
        [Documentation("Used to show that FogOfWar is Added", Name = "Add", IsField = true, DataType = "FogState")]
        Add,
        [Documentation("Used to Show that FogOfWar is Removed", Name = "Remove", IsField = true, DataType = "FogState")]
        Remove
    }
    internal enum TreeViewDisplayItemType
    {
        Node,
        Item
    }
    public enum CustomControlType
    {
        Label,
        Textbox,
        Picturebox
    }
}
