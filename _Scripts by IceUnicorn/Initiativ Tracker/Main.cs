Page.Text = Config.Name;

Page.Size = new Size(500, 500);
var pageSize = Page.Size;

var lblAuthor = new Label();
lblAuthor.Name = "lblAuthor";
lblAuthor.Text = $"Made By; {Config.Author}";
lblAuthor.Location = new Point(pageSize.Width - 200, pageSize.Height - 200);
lblAuthor.Size = new Size(195, lblAuthor.Size.Height);
lblAuthor.Anchor = (AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right));

var lblNameAndVersion = new Label();
lblNameAndVersion.Name = "lblNameAndVersion";
lblNameAndVersion.Text = $"{Config.Name} | {Config.Version}";
lblNameAndVersion.Location = new Point(pageSize.Width - 200, pageSize.Height - 170);
lblNameAndVersion.Size = new Size(195, lblNameAndVersion.Size.Height);
lblNameAndVersion.Anchor = (AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right));

var tbDescription = new TextBox();
tbDescription.Name = "tbDescription";
tbDescription.Text = Config.Description;
tbDescription.Multiline = true;
tbDescription.Location = new Point(pageSize.Width - 200, pageSize.Height - 140);
tbDescription.Size = new Size(195, 135);
tbDescription.Anchor = (AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right));
tbDescription.ReadOnly = true;

Page.Controls.Add(lblAuthor);
Page.Controls.Add(lblNameAndVersion);
Page.Controls.Add(tbDescription);


(string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) description =
	StreamDeckStatics.CreateDescription("Init   Tracker");

description.ActionDescription[0,0] = "Next Player";
StreamDeckStatics.ActionList.Add(("Next   Player", "Next Player", new Action(() =>
{
	btnNext.Invoke(new Action(() => btnNext.PerformClick()));
})));