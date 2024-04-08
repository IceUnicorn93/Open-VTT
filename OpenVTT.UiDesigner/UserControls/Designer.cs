using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenVTT.Scripting;
using System.Threading.Tasks;
using OpenVTT.Logging;
using System.Text.Json;
using OpenVTT.UiDesigner.Interfaces;
using OpenVTT.UiDesigner.Classes;
using OpenVTT.UiDesigner.Forms;

namespace OpenVTT.UiDesigner.UserControls
{
    public partial class Designer : UserControl
    {
        public string LoadPath { get; set; }

        private bool isNotesDesigner = false;
        public bool IsNotesDesigner
        {
            get => isNotesDesigner;
            set
            {
                isNotesDesigner = value;
                if(isNotesDesigner)
                {
                    btnFromTemplate.Enabled = true;
                    btnStreanDeck.Enabled = false;
                }
                else
                {
                    btnFromTemplate.Enabled = false;
                    btnStreanDeck.Enabled = true;
                }
            }
        }

        public string NoteName = "Template";

        public Designer()
        {
            InitializeComponent();
        }

        // Setup Design Surface and CodeGeneration
        // https://stackoverflow.com/questions/59529839/hosting-windows-forms-designer-serialize-designer-at-runtime-and-generate-c-sh
        DesignSurface designSurface;

        private void Designer_Load(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | Designer_Load");

            if (string.IsNullOrEmpty(LoadPath) || string.IsNullOrWhiteSpace(LoadPath)) return;
            if (!Directory.Exists(LoadPath))
                Directory.CreateDirectory(LoadPath);

            designSurface = new DesignSurface(typeof(UserControl));
            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            if (isNotesDesigner == false)
            {
                var sd = (ScriptDescription)host.CreateComponent(typeof(ScriptDescription));
                root.Controls.Add(sd); 
            }

            var selectionService = (ISelectionService)host.GetService(typeof(ISelectionService));
            selectionService.SelectionChanged += (o, args) => { pgControl.SelectedObject = ((ISelectionService)o).PrimarySelection; };

            var ccs = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));
            ccs.ComponentChanged += (o, args) => { pgControl.SelectedObject = args.Component; };

            TypeDescriptor.GetProperties(root)["Name"].SetValue(root, "Main");

            var view = (Control)designSurface.View;
            view.Dock = DockStyle.Fill;
            view.BackColor = Color.White;
            view.KeyDown += (o, ea) =>
            {
                if (ea.KeyCode != Keys.Delete) return;
                if (!(pgControl.SelectedObject == root))
                {
                    foreach (Control item in selectionService.GetSelectedComponents())
                        host.DestroyComponent(item);

                    pgControl.SelectedObject = root;
                }
            };
            pnlDesigner.Controls.Add(view);

            FillItembox();
            lbControls.Enabled = !isNotesDesigner;

            if (IsNotesDesigner)
            {
                //if Template-Design-File exists, load it
                if (File.Exists(Path.Combine(LoadPath, $"_{NoteName}-Design.cs")))
                    LoadDesign(Path.Combine(LoadPath, $"_{NoteName}-Design.cs"));
            }
            else
            {
                //if Main-Design-File exists, load it
                if (File.Exists(Path.Combine(LoadPath, "Design-Main.cs")))
                    LoadDesign(Path.Combine(LoadPath, "Design-Main.cs"));
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | btnNew_Click");

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            while (root.Controls.Count > 0) //Cleanup all Components
                host.DestroyComponent(root.Controls[0]);

            foreach (listBoxItem item in lbControls.Items.OfType<listBoxItem>()) //Reset all counters
                item.countPressed = 0;

            TypeDescriptor.GetProperties(root)["Name"].SetValue(root, "newUC");
            MessageBox.Show("Please set the Name of the Main Control!");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | btnLoad_Click");

            //Choose file to load, if non is choosen, don't continue
            var path = "";
            using (var ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Path.GetDirectoryName(LoadPath);
                if (ofd.ShowDialog() == DialogResult.OK)
                    path = ofd.FileName;
                else
                    return;
            }

            LoadDesign(path);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | btnSave_Click");

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            var code = GenerateCSFromDesigner(designSurface);

            if (isNotesDesigner)
                File.WriteAllText(Path.Combine(LoadPath, $"_{NoteName}-Design.cs"), code);
            else
                File.WriteAllText(Path.Combine(LoadPath, $"Design-{root.Name}.cs"), code);

            if (isNotesDesigner == false)
            {
                ScriptConfig sc;
                if (File.Exists(Path.Combine(LoadPath, "ScriptConfig.xml"))) //File exists
                {
                    sc = ScriptConfig.Load(Path.Combine(LoadPath, "ScriptConfig.xml"));
                }
                else //New file
                {
                    sc = new ScriptConfig();

                    sc.isActive = true;
                    sc.isUI = true;

                    sc.Name = LoadPath.Split(Path.DirectorySeparatorChar).Last();
                    sc.Author = "Your Name here";
                    sc.Description = "Your Description here";
                    sc.Version = "1.0";

                    sc.File_References = new List<string>();
                    sc.DLL_References = new List<string>();
                }

                sc.File_References.Clear();
                sc.File_References.AddRange(Directory.GetFiles(LoadPath, "*.cs").Select(n => Path.GetFileName(n)));

                sc.Using_References = sc.Using_References.Distinct().ToList();

                sc.Save(Path.Combine(LoadPath, "ScriptConfig.xml"));
            }

            if (isNotesDesigner)
            { 
                CreateNotesImplementationFiles();

                //Read files for Data
                var templateFile = File.ReadAllText(Path.Combine(LoadPath, $"_{NoteName}.cs"));

                var completeFileText = templateFile + Environment.NewLine;

                var lines = new List<string>();
                lines.Add($"var tj = new {NoteName}();");
                lines.Add("var data = JsonSerializer.Serialize(tj);");
                lines.Add("using (var sw = new StreamWriter(@\"" + Path.Combine(LoadPath, $"_{NoteName}.json") + "\"))");
                lines.Add("sw.WriteLine(data);");
                lines.Add("null");
                completeFileText += string.Join(Environment.NewLine, lines);

                //Run Script!
                var n = ScriptEngine.RunUiScript<object>(completeFileText, null).Result;
            }
            else
                CreateImplementationFiles();
        }

        private void btnFromTemplate_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | btnFromTemplate_Click");

            var types = new List<string>
            {
                "Text",
                "ArtworkInformation"
            };

            var designFiles = new List<string>();
            designFiles.AddRange(Directory.GetFiles(LoadPath));
            designFiles.RemoveAll(n => n.Contains("Design-Main.cs"));
            designFiles.RemoveAll(n => n.Contains($"{NoteName}"));
            designFiles.RemoveAll(n => n.Contains($".json"));
            //types.AddRange(designFiles.Select(n => Path.GetFileName(n).Replace("Design-", "").Replace(".cs", "")));

            var structDesigner = new StructureDesigner();
            structDesigner.Types = types.ToArray();

            //Load already selected Scturctures into the designer
            if(isNotesDesigner & File.Exists(Path.Combine(LoadPath, $"_{NoteName}-JSON.json")))
            {
                var list = JsonSerializer.Deserialize<List<structureBase>>(File.ReadAllBytes(Path.Combine(LoadPath, $"_{NoteName}-JSON.json")));
                var structureList = new List<OpenVttFileStructure>();
                foreach (var item in list)
                {
                    var st = new OpenVttFileStructure();
                    st.Types = types.ToArray();
                    ((IStructureBase)st).Name = item.Name;
                    ((IStructureBase)st).Type = item.Type;
                    ((IStructureBase)st).SingleValue = item.SingleValue;
                    structureList.Add(st);
                }
                structDesigner.Structure = structureList.ToArray();
            }

            structDesigner.ShowDialog();

            //Create Items based on Selection in the Designer
            var localMyItems = lbControls.Items.OfType<listBoxItem>().ToList();
            foreach (IStructureBase type in structDesigner.Structure)
            {
                if ((type.Type == "Text" || type.Type == "Number" || type.Type == "Decimal Number") && type.SingleValue)
                {
                    CreateItem(localMyItems.Single(n => n.Text.Contains("Label")), $"lbl{type.Name.Replace(" ", "")}", $"{type.Name}", $"{type.Name}");
                    CreateItem(localMyItems.Single(n => n.Text.Contains("Textbox")), $"tb{type.Name.Replace(" ", "")}", "", $"{type.Name}");
                }
                else if (type.Type == "ArtworkInformation")
                {
                    var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
                    var root = (UserControl)host.RootComponent;

                    var lI = new listBoxItem
                    {
                        countPressed = 0,
                        DefaultName = "ArtworkInformation",
                        ItemType = typeof(ArtworkInformation),
                        Text = ""
                    };
                    lI.GetControl += () =>
                    {
                        var artInfo = (ArtworkInformation)host.CreateComponent(typeof(ArtworkInformation), "ArtworkInformation");
                        root.Controls.Add(artInfo);
                        return artInfo;
                    };
                    CreateItem(lI, "ArtworkInformation", "", "Artwork");
                }
                else if (type.SingleValue)
                {
                    CreateItem(localMyItems.Single(n => n.Text.Contains(type.Type)), $"uc{type.Name.Replace(" ", "")}", "", $"{type.Name}");
                }
                else
                {
                    CreateItem(localMyItems.Single(n => n.Text.Contains("Label")), $"lbl{type.Name.Replace(" ", "")}", $"{type.Name}", $"{type.Name}");
                    CreateItem(localMyItems.Single(n => n.Text.Contains("FlowLayoutPanel")), $"flp{type.Name.Replace(" ", "")}", "", $"{type.Name}");
                }
            }

            //Create the safe-file with the Current Selection
            if (isNotesDesigner)
            {
                var data = JsonSerializer.Serialize(structDesigner.Structure as IStructureBase[]);
                using (var sw = new StreamWriter(Path.Combine(LoadPath, $"_{NoteName}-JSON.json")))
                    sw.WriteLine(data);

                CreateTemplateCs();
            }
        }

        private void btnStreanDeck_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | btnStreanDeck_Click");

            List<Button> getButtons(List<Control> controlList)
            {
                var ret = new List<Button>();
                foreach (var ctrl in controlList)
                {
                    if(ctrl is Button) ret.Add(ctrl as Button);
                    else ret.AddRange(getButtons(ctrl.Controls.Cast<Control>().ToList()));
                }
                return ret;
            }

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            var controls = root.Controls.Cast<Control>().ToList();

            var buttons = getButtons(controls);

            var uiDesignStreamDeckConfigurator = new UiDesignStreamDeckConfigurator();
            uiDesignStreamDeckConfigurator.buttons = buttons;
            uiDesignStreamDeckConfigurator.ShowDialog();
            var selectedButtons = uiDesignStreamDeckConfigurator.selectedButtons;


            //Update ScriptConfig
            var sc = ScriptConfig.Load(Path.Combine(LoadPath, "ScriptConfig.xml"));
            sc.File_References.Insert(0, "aaStreamDeck.cs");
            sc.File_References.Add("zzStreamDeck.cs");
            sc.File_References = sc.File_References.Distinct().ToList();
            sc.Save(Path.Combine(LoadPath, "ScriptConfig.xml"));

            var pluginName = sc.Name;

            //First Part, Create the Static StreamDeck class for all Button-Actions and easy DescriptionAdding
            var buttonActions = new List<string>();
            buttonActions.Add("// Don't modify this code, please :>");
            buttonActions.Add("static class StreamDeck");
            buttonActions.Add("{");
            buttonActions.Add("\tpublic static (string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) Description;");
            buttonActions.Add("\t");
            for (int i = 0; i < selectedButtons.Count; i++)
                buttonActions.Add($"\tpublic static Action {selectedButtons[i].Name}Action;");
            buttonActions.Add("\t");
            buttonActions.Add("\tpublic static void AddPageingAction((string Name, Action action) data) => Description.PageingActions.Add(data);");
            buttonActions.Add("}");
            buttonActions.Add("");

            buttonActions.Add(
                $"(string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) description = StreamDeckStatics.CreateDescription(\"{pluginName}\");");
            buttonActions.Add("StreamDeck.Description = description;");
            buttonActions.Add("");
            File.WriteAllText(Path.Combine(LoadPath, "aaStreamDeck.cs"), string.Join(Environment.NewLine, buttonActions));

            //Second Part, Add Actions to the StreamDeckStatics-Class and easy Copy-Paste Stuff for ease of use
            buttonActions.Clear();
            buttonActions.Add("// Feel free to modify this code! :)");
            for (int i = 0; i < selectedButtons.Count; i++)
                buttonActions.Add($"StreamDeckStatics.ActionList.Add((\"DisplayText{i}\", \"{pluginName}.Action{i}\", StreamDeck.{selectedButtons[i].Name}Action));");
            buttonActions.Add("");
            buttonActions.Add("//Example of how to map Static Functions");
            buttonActions.Add($"//description.ActionDescription[0,0] = \"{pluginName}.Action0\";");
            buttonActions.Add("");
            buttonActions.Add("// Add this Code to the Implementation-Main.cs");
            buttonActions.Add("/*");
            for (int i = 0; i < selectedButtons.Count; i++)
                buttonActions.Add($"StreamDeck.{selectedButtons[i].Name}Action = new Action(async () => " + "{ this.Invoke((MethodInvoker) delegate {" + $"{selectedButtons[i].Name}.PerformClick();" + "}); });");
            buttonActions.Add("*/");
            File.WriteAllText(Path.Combine(LoadPath, "zzStreamDeck.cs"), string.Join(Environment.NewLine, buttonActions));
        }

        private void lbControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Designer | lbControls_SelectedIndexChanged");

            if (!(lbControls.SelectedItem is listBoxItem)) return;

            var item = (listBoxItem)lbControls.SelectedItem;
            CreateItem(item, $"{item.DefaultName}{item.countPressed}", "", "");
        }

        //----------------------------------------------------------------------------------------------------------------------------

        private void FillItembox()
        {
            Logger.Log("Class: Designer | FillItembox");

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            lbControls.Items.Clear();

            var lst = new List<listBoxItem>
            {
                //Button
                new listBoxItem
                {
                    Text = "Add Button",
                    DefaultName = "button",
                    ItemType = typeof(Button),
                    GetControl = new Func<Control>(() =>
                    {
                        var btn = (Button)host.CreateComponent(typeof(Button), "button");
                        root.Controls.Add(btn);
                        return btn;
                    })
                },
                //Textbox
                new listBoxItem
                {
                    Text = "Add Textbox",
                    DefaultName = "textBox",
                    ItemType = typeof(TextBox),
                    GetControl = new Func<Control>(() =>
                    {
                        var tb = (TextBox)host.CreateComponent(typeof(TextBox), "textBox");
                        root.Controls.Add(tb);
                        return tb;
                    })
                },
                //Label
                new listBoxItem
                {
                    Text = "Add Label",
                    DefaultName = "label",
                    ItemType = typeof(Label),
                    GetControl = new Func<Control>(() =>
                    {
                        var lbl = (Label)host.CreateComponent(typeof(Label), "label");
                        lbl.Text = "label";
                        lbl.AutoSize = true;
                        root.Controls.Add(lbl);
                        return lbl;
                    })
                },
                //Panel
                new listBoxItem
                {
                    Text = "Add Panel",
                    DefaultName = "panel",
                    ItemType = typeof(Panel),
                    GetControl = new Func<Control>(() =>
                    {
                        var pnl = (Panel)host.CreateComponent(typeof(Panel), "panel");
                        root.Controls.Add(pnl);
                        return pnl;
                    })
                },
                //FlowLayoutPanel
                new listBoxItem
                {
                    Text = "Add FlowLayoutPanel",
                    DefaultName = "flowLayoutPanel",
                    ItemType = typeof(FlowLayoutPanel),
                    GetControl = new Func<Control>(() =>
                    {
                        var flp = (FlowLayoutPanel)host.CreateComponent(typeof(FlowLayoutPanel), "flowLayoutPanel");
                        flp.AutoScroll = true;
                        root.Controls.Add(flp);
                        return flp;
                    })
                },
                //Checkbox
                new listBoxItem
                {
                    Text = "Add CheckBox",
                    DefaultName = "checkBox",
                    ItemType = typeof(CheckBox),
                    GetControl = new Func<Control>(() =>
                    {
                        var cb = (CheckBox)host.CreateComponent(typeof(CheckBox), "checkBox");
                        cb.Text = "checkbox";
                        root.Controls.Add(cb);
                        return cb;
                    })
                },
                //Combobox
                new listBoxItem
                {
                    Text = "Add Combobox",
                    DefaultName = "comboBox",
                    ItemType = typeof(ComboBox),
                    GetControl = new Func<Control>(() =>
                    {
                        var cb = (ComboBox)host.CreateComponent(typeof(ComboBox), "comboBox");
                        root.Controls.Add(cb);
                        return cb;
                    })
                },
                //Listbox
                new listBoxItem
                {
                    Text = "Add Listbox",
                    DefaultName = "listBox",
                    ItemType = typeof(ListBox),
                    GetControl = new Func<Control>(() =>
                    {
                        var lb = (ListBox)host.CreateComponent(typeof(ListBox), "listBox");
                        root.Controls.Add(lb);
                        return lb;
                    })
                },
                //NumericUpDown
                new listBoxItem
                {
                    Text = "Add NumericUpDown",
                    DefaultName = "numericUpDown",
                    ItemType = typeof(NumericUpDown),
                    GetControl = new Func<Control>(() =>
                    {
                        var nud = (NumericUpDown)host.CreateComponent(typeof(NumericUpDown), "numericUpDown");
                        root.Controls.Add(nud);
                        return nud;
                    })
                },
                //PictureBox
                new listBoxItem
                {
                    Text = "Add Picturebox",
                    DefaultName = "pictureBox",
                    ItemType = typeof(PictureBox),
                    GetControl = new Func<Control>(() =>
                    {
                        var pb = (PictureBox)host.CreateComponent(typeof(PictureBox), "pictureBox");
                        root.Controls.Add(pb);
                        return pb;
                    })
                },
                //RadioButton
                new listBoxItem
                {
                    Text = "Add Radiobutton",
                    DefaultName = "radioButton",
                    ItemType = typeof(RadioButton),
                    GetControl = new Func<Control>(() =>
                    {
                        var rb = (RadioButton)host.CreateComponent(typeof(RadioButton), "radioButton");
                        rb.Text = "radioButton";
                        root.Controls.Add(rb);
                        return rb;
                    })
                },
            };

            lst = lst.OrderBy(n => n.Text).ToList();

            lbControls.Items.AddRange(lst.ToArray());

            lbControls.Items.Add("----------");

            var designFiles = new List<string>();
            designFiles.AddRange(Directory.GetFiles(LoadPath, "Design-*.cs"));
            designFiles.RemoveAll(n => n.Contains("Design-Main.cs"));

            foreach (var designFile in designFiles)
            {
                var fileName = Path.GetFileName(designFile).Replace("Design-", "").Replace(".cs", "");

                var item = new listBoxItem
                {
                    Text = $"Add UC {fileName}",
                    DefaultName = $"uc{fileName}",
                    GetControl = new Func<Control>(() =>
                    {
                        if (root.Name == fileName) return new Control();

                        var statementList = new List<string>
                        {
                            $"var uc = ({fileName})host.CreateComponent(typeof({fileName}), \"uc{fileName}\");",
                            $"root.Controls.Add(uc);",
                            "uc"
                        };

                        var componentCode = string.Join(Environment.NewLine, statementList);

                        var sh = new UiScriptHost { host = host, root = root };

                        return runScript<UserControl>(componentCode, sh).Result;
                    })
                };

                var sample = item.GetControl();
                item.ItemType = sample.GetType();

                host.DestroyComponent(sample);

                lbControls.Items.Add(item);
            }
        }

        private void LoadDesign(string path)
        {
            Logger.Log("Class: Designer | LoadDesign");

            this.Text = "Designer - Working on: " + path;

            if (File.Exists(path) == false) return;

            var code = File.ReadAllText(path);

            var lines = CleanUpCode(code); //Remove unneccessary stuff like "this."

            var processed = ProcessLines(lines); //Process lines and get Componenets and InitScript
            var componentDeclarations = processed.componentDeclarations;
            var initComponents = processed.initComponents;

            var componentNameWithType = TransformComponents(componentDeclarations); //Transform Components so we get the Name and the Type

            var mainControlInit = RemoveUsedLinesFromInit(componentNameWithType, initComponents); //Get the Instructions for the main Component

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            //"Clean up" -> Set Default BackColor and remove all old components
            root.BackColor = Color.FromKnownColor(KnownColor.Control);
            while (root.Controls.Count > 0) //Cleanup all Components
                host.DestroyComponent(root.Controls[0]);

            //Fill DesignSurface!
            var sh = new UiScriptHost { host = host, root = root };
            ProcessMainControl(mainControlInit, sh); // first run Main Control/Component
            TypeDescriptor.GetProperties(root)["Name"].SetValue(root, root.Name); //For whatever reason, the Name Property has to be set extra
            ProcessComponents(componentNameWithType, initComponents, sh); // then the Child-Controls

            //Refill the Textbox
            FillItembox();

            //Set Counters to highest Number
            Logger.Log("Class: Designer | Cast to Control and Clean up");
            var lst = root.Controls.Cast<Control>().ToList();
            lbControls.Items.Remove("----------");

            foreach (listBoxItem item in lbControls.Items)
            {
                Logger.Log("Class: Designer | For each Item");
                var t = item.ItemType;
                var sameType = lst.Where(n => n.GetType().ToString() == t.ToString()).ToList();
                if (sameType.Any())
                {
                    Logger.Log("Class: Designer | SameType Any");
                    var max = sameType.Max(n =>
                    {
                        var componentNameWithOnlyDigits = n.Name.ToCharArray().Where(m => char.IsDigit(m)).ToList();

                        if (componentNameWithOnlyDigits.Any())
                            return int.Parse(string.Join("", componentNameWithOnlyDigits)) + 1;
                        else
                            return 0;
                    });
                    item.countPressed = max;
                    Logger.Log("Class: Designer | Set Max");
                }
                else
                    item.countPressed = 0;
            }

            Logger.Log("Class: Designer | Done");
            GC.Collect(Bottom);
        }

        private void CreateImplementationFiles()
        {
            Logger.Log("Class: Designer | CreateImplementationFiles");

            var designFiles = Directory.GetFiles(LoadPath, "Design-*.cs");
            foreach (var file in designFiles)
            {
                var fileName = Path.GetFileName(file);
                fileName = fileName.Replace("Design-", "");
                fileName = "Implementation-" + fileName;

                if (File.Exists(Path.Combine(LoadPath, fileName)))
                    continue;
                else
                {
                    var designFileContent = File.ReadAllLines(file).ToList();
                    var buttonNames = new List<string>();
                    designFileContent.ForEach(n =>
                    {
                        if (n.Contains("private System.Windows.Forms.Button "))
                        {
                            var buttonLine = n;
                            buttonLine = buttonLine.Trim();
                            var buttonNameWithSemicolon = buttonLine.Replace("private System.Windows.Forms.Button ", "");
                            var buttonName = buttonNameWithSemicolon.Replace(";", "").Trim();
                            buttonNames.Add(buttonName);
                        }
                    });

                    var fileCode = "";
                    fileCode += $"public partial class {Path.GetFileName(file).Replace("Design-", "").Replace(".cs", "")} : System.Windows.Forms.UserControl" + Environment.NewLine;
                    fileCode += "{" + Environment.NewLine;
                    fileCode += $"\tpublic {Path.GetFileName(file).Replace("Design-", "").Replace(".cs", "")}(object o)" + Environment.NewLine;
                    fileCode += "\t{" + Environment.NewLine;
                    fileCode += $"\t\tthis.InitializeComponent();" + Environment.NewLine;
                    fileCode += $"\t\t" + Environment.NewLine;

                    foreach (var button in buttonNames)
                    {
                        fileCode += $"\t\t{button}.Click += (s, e) =>" + Environment.NewLine;
                        fileCode += "\t\t{" + Environment.NewLine;
                        fileCode += "\t\t\t" + Environment.NewLine;
                        fileCode += "\t\t};" + Environment.NewLine;
                    }

                    fileCode += "\t}" + Environment.NewLine;
                    fileCode += "}" + Environment.NewLine;

                    File.WriteAllText(Path.Combine(LoadPath, fileName), fileCode);
                }
            }

            if (!File.Exists(Path.Combine(LoadPath, "Implementation.cs")))
            {
                var fileContent = "";

                fileContent += $"Page.Text = Config.Name;{Environment.NewLine}";
                fileContent += $"{Environment.NewLine}";
                fileContent += $"var main = new Main(null);{Environment.NewLine}";
                fileContent += $"main.Dock = DockStyle.Fill;{Environment.NewLine}";
                fileContent += $"Page.Controls.Add(main);{Environment.NewLine}";

                File.WriteAllText(Path.Combine(LoadPath, "Implementation.cs"), fileContent);
            }
        }

        private void CreateNotesImplementationFiles()
        {
            Logger.Log("Class: Designer | CreateNotesImplementationFiles");

            var file = Path.Combine(LoadPath, $"_{NoteName}-Implementation.cs");

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;
            var textBoxes = root.Controls.Cast<Control>().Where(n => n is TextBox).ToList();
            var artInfo = root.Controls.Cast<Control>().Where(n => n is ArtworkInformation).ToList().SingleOrDefault();

            var fileCode = "";
            fileCode += $"public partial class {root.Name} : System.Windows.Forms.UserControl" + Environment.NewLine;
            fileCode += "{" + Environment.NewLine;
            fileCode += $"\tpublic {root.Name}({NoteName} tj)" + Environment.NewLine;
            fileCode += "\t{" + Environment.NewLine;
            fileCode += $"\t\tthis.InitializeComponent();" + Environment.NewLine;
            fileCode += $"\t\t" + Environment.NewLine;

            foreach (var tb in textBoxes)
            {
                fileCode += $"\t\t{tb.Name}.DataBindings.Add(nameof({tb.Name}.Text), tj, nameof(tj.{tb.Tag}));" + Environment.NewLine;
                fileCode += $"" + Environment.NewLine;
            }

            if(artInfo != null)
            {
                fileCode += $"\t\t{artInfo.Name}.DataBindings.Add(nameof({artInfo.Name}.data), tj, nameof(tj.artInfo));" + Environment.NewLine;
                fileCode += $"" + Environment.NewLine;
            }

            fileCode += "\t}" + Environment.NewLine;
            fileCode += "}" + Environment.NewLine;

            File.WriteAllText(file, fileCode);
        }

        private void CreateTemplateCs()
        {
            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;
            var textBoxes = root.Controls.Cast<Control>().Where(n => n is TextBox).ToList();

            var path = Path.Combine(LoadPath, $"_{NoteName}.cs");

            var lines = new List<string>();
            lines.Add($"public class {NoteName} : INotifyPropertyChanged");
            lines.Add("{");
            lines.Add("\tpublic event PropertyChangedEventHandler PropertyChanged;");
            lines.Add("\tprivate string _path = \"\";");
            lines.Add("\tpublic string path { get => _path; set { _path = value; _artInfo.Path = _path; }}");
            lines.Add("\tprivate OpenVTT.UiDesigner.Classes.ArtInfo _artInfo = new OpenVTT.UiDesigner.Classes.ArtInfo();");
            lines.Add("\tpublic OpenVTT.UiDesigner.Classes.ArtInfo artInfo { get => _artInfo; set { _artInfo = value; _artInfo.Path = path; _artInfo.pChange += () => NotifyPropertyChanged(\"\"); } }");
            lines.Add("");
            lines.Add("\tprivate void NotifyPropertyChanged([CallerMemberName] String propertyName = \"\")");
            lines.Add("\t{");
            lines.Add("\t\tif(path == \"\") return;");
            lines.Add("\t\tvar data = JsonSerializer.Serialize(this);");
            lines.Add($"\t\tusing (var sw = new StreamWriter(path))");
            lines.Add("\t\t\tsw.WriteLine(data);");
            lines.Add("\t\t");
            lines.Add("\t\tif (PropertyChanged == null) return;");
            lines.Add("\t\tPropertyChanged(this, new PropertyChangedEventArgs(propertyName));");
            lines.Add("\t}");
            lines.Add("\t");
            foreach (var item in textBoxes)
            {

                lines.Add($"\tprivate string _{item.Tag};");
                lines.Add("\tpublic string " + item.Tag + " { get => _" + item.Tag + "; set { _" + item.Tag + " = value; NotifyPropertyChanged(); } }");
                lines.Add("\t");
            }
            lines.Add("}");

            File.WriteAllText(path, string.Join(Environment.NewLine, lines.ToArray()));
        }

        private string GenerateCSFromDesigner(DesignSurface designSurface)
        {
            Logger.Log("Class: Designer | GenerateCSFromDesigner");

            CodeTypeDeclaration type;
            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = host.RootComponent;
            var manager = new DesignerSerializationManager(host);
            using (manager.CreateSession())
            {
                var serializer = (TypeCodeDomSerializer)manager.GetSerializer(root.GetType(),
                    typeof(TypeCodeDomSerializer));
                type = serializer.Serialize(manager, root, host.Container.Components);
                type.IsPartial = true;
                type.Members.OfType<CodeConstructor>()
                    .FirstOrDefault().Attributes = MemberAttributes.Public;
            }

            var builder = new StringBuilder();
            CodeGeneratorOptions option = new CodeGeneratorOptions();
            option.BracingStyle = "C";
            option.BlankLinesBetweenMembers = false;
            using (var writer = new StringWriter(builder, CultureInfo.InvariantCulture))
            {
                using (var codeDomProvider = new CSharpCodeProvider())
                {
                    codeDomProvider.GenerateCodeFromType(type, writer, option);
                }
                return builder.ToString();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------

        //Interprete & run Code
        private List<string> CleanUpCode(string code)
        {
            Logger.Log("Class: Designer | CleanUpCode");

            return code
                .Replace("this.", "") //Replace this. (won't be needed later)
                .Trim() //Remove spaces
                .Split(Environment.NewLine.ToCharArray()).ToList() //Split at new lines
                .Where(n => n != "").ToList(); //only lines that are not empty
        }
        private (List<string> componentDeclarations, List<string> initComponents) ProcessLines(List<string> lines)
        {
            Logger.Log("Class: Designer | ProcessLines");

            var state = "WaitForTypes";

            var componentDeclarations = new List<string>(); //Contains for example: private Open_VTT_Logo_Color_Test.userCtrl uc1;
            var initComponents = new List<string>(); //Contains for example: this.button1.Location = new System.Drawing.Point(12, 34);

            for (int i = 0; i < lines.Count; i++) //Process line for line
            {
                switch (state)
                {
                    case "break":
                        break;
                    case "ReadInitializeComponent":
                        if (!lines[i].Contains("{"))
                            if (lines[i].Contains("}"))
                                state = "break";
                            else
                                initComponents.Add(lines[i]);
                        else
                            state = "ReadInitializeComponent";
                        break;
                    case "WaitForInitializeComponent":
                        if (lines[i].Contains("private void InitializeComponent()"))
                            state = "ReadInitializeComponent";
                        break;
                    case "ReadTypes":
                        if (lines[i].Contains("public"))
                            state = "WaitForInitializeComponent";
                        else
                            componentDeclarations.Add(lines[i]);
                        break;
                    case "WaitForTypes":
                        if (lines[i].Contains("{"))
                            state = "ReadTypes";
                        break;
                }
            }

            return (componentDeclarations, initComponents);
        }
        private List<(string Name, string Type)> TransformComponents(List<string> componentDeclarations)
        {
            Logger.Log("Class: Designer | TransformComponents");

            var componentNameWithType = new List<(string Name, string Type)>();
            componentDeclarations.ForEach(n =>
            {
                var cleanedUp = n.Replace(";", "").Trim(); //Remove ";" and make the line short
                var splits = cleanedUp.Split(' '); //Split at " " -> 0 = Modifyer ; 1 = Type ; 2 = Name
                componentNameWithType.Add((splits[2], splits[1]));
            });
            return componentNameWithType;
        }
        private List<string> RemoveUsedLinesFromInit(List<(string Name, string Type)> componentNameWithType, List<string> initComponents)
        {
            Logger.Log("Class: Designer | RemoveUsedLinesFromInit");

            var ret = new List<string>();
            ret.AddRange(initComponents.ToList());

            for (int i = 0; i < componentNameWithType.Count; i++)
            {
                var name = componentNameWithType[i].Name;

                ret.RemoveAll(n => n.Contains(name));
            }

            ret.RemoveAll(n => n.Contains("//") || n.Contains("SuspendLayout();") || n.Contains("ResumeLayout(false);") || n.Contains("|"));

            return ret;
        }
        private void ProcessMainControl(List<string> mainControlInit, UiScriptHost sh)
        {
            Logger.Log("Class: Designer | ProcessMainControl");

            var statementList = new List<string>();
            foreach (var mci in mainControlInit)
                statementList.Add($"root.{mci.Trim()}");
            statementList.Add(null);

            var code = string.Join(Environment.NewLine, statementList);
            var o = runScript<object>(code, sh).Result;
        }
        private void ProcessComponents(List<(string Name, string Type)> componentNameWithType, List<string> initComponents, UiScriptHost sh)
        {
            Logger.Log("Class: Designer | ProcessComponents");

            var statementList = new List<string>();
            for (int i = 0; i < componentNameWithType.Count; i++)
            {
                statementList.Add($"var {componentNameWithType[i].Name} = ({componentNameWithType[i].Type})host.CreateComponent(typeof({componentNameWithType[i].Type}), \"{componentNameWithType[i].Name}\");");

                //Sometimes lines are incomplete! we must complete them to get no error!
                var component = componentNameWithType[i].Name;
                var componentLines = initComponents.Where(n => n.Contains($"{component}.")).ToList();
                if (componentLines.Any(n => !n.Contains(";")))
                {
                    var incompleteLines = componentLines.Where(n => !n.Contains(";")).ToList();
                    foreach (var incompleteLine in incompleteLines)
                    {
                        var index = initComponents.IndexOf(incompleteLine);
                        var neededLines = new List<string>();
                        var hasFoundEnd = false;
                        while (hasFoundEnd == false)
                        {
                            index += 1;
                            neededLines.Add(initComponents[index]);
                            if (initComponents[index].Contains(";"))
                                hasFoundEnd = true;
                        }

                        var newValue = incompleteLine + string.Join("", neededLines);

                        var componentLineListIndex = componentLines.IndexOf(incompleteLine);
                        componentLines[componentLineListIndex] = newValue;
                    }
                }

                statementList.AddRange(componentLines);

                statementList.Add($"root.Controls.Add({componentNameWithType[i].Name});");
            }

            statementList.Add("null");
            var componentCode = string.Join(Environment.NewLine, statementList);
            var o = runScript<object>(componentCode, sh);
        }

        //Code Execution
        private async Task<T> runScript<T>(string code, UiScriptHost host = null)
        {
            Logger.Log("Class: Designer | runScript<T>");

            var script = "";

            //Put all the files in!
            var designFiles = new List<string>();
            designFiles.AddRange(Directory.GetFiles(LoadPath, "Design-*.cs"));

            foreach (var file in designFiles)
                script += File.ReadAllText(file) + Environment.NewLine;

            Logger.Log("Class: Designer | Read all files");

            script += Environment.NewLine;

            script += code;
            script = script.Replace("Submission#0.", "");

            Logger.Log("Class: Designer | Run in Script Engine");
            return await ScriptEngine.RunUiScript<T>(script, host);
        }

        private void CreateItem(listBoxItem item, string Name, string Text, string Tag)
        {
            Logger.Log("Class: Designer | CreateItem");

            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            if (root.Controls.Cast<Control>().Count(n => n.Name == Name) > 0) return;

            var ctrl = item.GetControl();

            if (IsNotesDesigner)
                ctrl.Tag = Tag;

            item.countPressed++;
            if (Text != "")
                ctrl.Text = Text;
            TypeDescriptor.GetProperties(ctrl)["Name"].SetValue(ctrl, $"{Name}");
        }
    }
}
