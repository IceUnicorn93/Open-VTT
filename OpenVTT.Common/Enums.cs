namespace OpenVTT.Common
{
    [Documentation("To use this Object use var dt = DisplayType.Player;")]
    public enum DisplayType
    {
        [Documentation("Used for the PlayerScreen")]
        Player,
        [Documentation("Used for the Player ArtworkDisplay")]
        InformationDisplayPlayer,
        [Documentation("Used for the DM ArtworkDisplay")]
        InformationDisplayDM
    }
    internal enum PictureBoxMode
    {
        Rectangle,
        Poligon,
        Ping
    }
    [Documentation("To use this Object use var fs = FogState.Add;")]
    public enum FogState
    {
        [Documentation("Used to show that FogOfWar is Added")]
        Add,
        [Documentation("Used to Show that FogOfWar is Removed")]
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
