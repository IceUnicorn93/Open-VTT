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

namespace OpenVTT.UiDesigner
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

        public Designer()
        {
            InitializeComponent();
        }

        private void Designer_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoadPath) || string.IsNullOrWhiteSpace(LoadPath)) return;
            if (!Directory.Exists(LoadPath))
                Directory.CreateDirectory(LoadPath);

            designSurface = new DesignSurface(typeof(UserControl));
            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

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
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
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

            this.Text = "Designer - Working on: " + path;

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
            var lst = root.Controls.Cast<Control>().ToList();
            lbControls.Items.Remove("----------");
            foreach (listBoxItem item in lbControls.Items)
            {
                var t = item.ItemType;
                var sameType = lst.Where(n => n.GetType().ToString() == t.ToString()).ToList();
                if (sameType.Any())
                {
                    var max = sameType.Max(n =>
                    {
                        var componentNameWithOnlyDigits = n.Name.ToCharArray().Where(m => char.IsDigit(m)).ToList();

                        if (componentNameWithOnlyDigits.Any())
                            return int.Parse(string.Join("", componentNameWithOnlyDigits));
                        else
                            return 0;
                    });
                    item.countPressed = max;
                }
                else
                    item.countPressed = 0;
            }

            GC.Collect(Bottom);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var host = (IDesignerHost)designSurface.GetService(typeof(IDesignerHost));
            var root = (UserControl)host.RootComponent;

            var code = GenerateCSFromDesigner(designSurface);

            File.WriteAllText(Path.Combine(LoadPath, $"{root.Name}-Code.cs"), code);

            Console.Beep();
        }

        private void btnFromTemplate_Click(object sender, EventArgs e)
        {
            var types = new List<string>
            {
                "Text",
                "Number",
                "Decimal Number"
            };

            var designFiles = new List<string>();
            designFiles.AddRange(Directory.GetFiles(LoadPath));
            designFiles.RemoveAll(n => n.Contains("Main-Code.cs"));
            types.AddRange(designFiles.Select(n => Path.GetFileName(n).Replace("-Code.cs", "")));

            var structDesigner = new StructureDesigner();
            structDesigner.Types = types.ToArray();

            structDesigner.ShowDialog();

            var localMyItems = lbControls.Items.OfType<listBoxItem>().ToList();
            foreach (var type in structDesigner.Structure)
            {
                if (type.Type == "Text" || type.Type == "Number" || type.Type == "Decimal Number")
                {
                    CreateItem(localMyItems.Single(n => n.Text.Contains("Label")), $"lbl{type.Name}", $"{type.Name}");
                    CreateItem(localMyItems.Single(n => n.Text.Contains("Textbox")), $"tb{type.Name}", "");
                }
                else if (type.SingleValue)
                {
                    CreateItem(localMyItems.Single(n => n.Text.Contains(type.Type)), $"uc{type.Name}", "");
                }
                else
                {
                    CreateItem(localMyItems.Single(n => n.Text.Contains("Label")), $"lbl{type.Name}", $"{type.Name}");
                    CreateItem(localMyItems.Single(n => n.Text.Contains("FlowLayoutPanel")), $"flp{type.Name}", "");
                }
            }
        }

        private void btnStreanDeck_Click(object sender, EventArgs e)
        {

        }

        private void lbControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lbControls.SelectedItem is listBoxItem)) return;

            var item = (listBoxItem)lbControls.SelectedItem;
            CreateItem(item, $"{item.DefaultName}{item.countPressed}", "");
        }



        // Setup Design Surface and CodeGeneration
        // https://stackoverflow.com/questions/59529839/hosting-windows-forms-designer-serialize-designer-at-runtime-and-generate-c-sh
        DesignSurface designSurface;
        private void FillItembox()
        {
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
            designFiles.AddRange(Directory.GetFiles(LoadPath));
            designFiles.RemoveAll(n => n.Contains("Main-Code.cs"));

            foreach (var designFile in designFiles)
            {
                var fileName = Path.GetFileName(designFile).Replace("-Code.cs", "");

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

                        var obj = runScript<UserControl>(componentCode, sh);
                        return obj;
                    })
                };

                var sample = item.GetControl();
                item.ItemType = sample.GetType();

                host.DestroyComponent(sample);

                lbControls.Items.Add(item);
            }
        }

        string GenerateCSFromDesigner(DesignSurface designSurface)
        {
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

        //Interprete & run Code
        private List<string> CleanUpCode(string code)
        {
            return code
                .Replace("this.", "") //Replace this. (won't be needed later)
                .Trim() //Remove spaces
                .Split(Environment.NewLine.ToCharArray()).ToList() //Split at new lines
                .Where(n => n != "").ToList(); //only lines that are not empty
        }
        private (List<string> componentDeclarations, List<string> initComponents) ProcessLines(List<string> lines)
        {
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
            var ret = new List<string>();
            ret.AddRange(initComponents.ToList());

            for (int i = 0; i < componentNameWithType.Count; i++)
            {
                var name = componentNameWithType[i].Name;

                ret.RemoveAll(n => n.Contains(name));
            }

            ret.RemoveAll(n => n.Contains("//") || n.Contains("SuspendLayout();") || n.Contains("ResumeLayout(false);"));

            return ret;
        }
        private void ProcessMainControl(List<string> mainControlInit, UiScriptHost sh)
        {
            var statementList = new List<string>();
            foreach (var mci in mainControlInit)
                statementList.Add($"root.{mci.Trim()}");
            statementList.Add(null);

            var code = string.Join(Environment.NewLine, statementList);
            runScript<object>(code, sh);
        }
        private void ProcessComponents(List<(string Name, string Type)> componentNameWithType, List<string> initComponents, UiScriptHost sh)
        {

            var statementList = new List<string>();
            for (int i = 0; i < componentNameWithType.Count; i++)
            {
                statementList.Add($"var {componentNameWithType[i].Name} = ({componentNameWithType[i].Type})host.CreateComponent(typeof({componentNameWithType[i].Type}), \"{componentNameWithType[i].Name}\");");
                var componentLines = initComponents.Where(n => n.Contains($"{componentNameWithType[i].Name}.")).ToList();
                statementList.AddRange(componentLines);
                statementList.Add($"root.Controls.Add({componentNameWithType[i].Name});");
            }

            statementList.Add("null");
            var componentCode = string.Join(Environment.NewLine, statementList);
            runScript<object>(componentCode, sh);
        }

        //Code Execution
        private T runScript<T>(string code, UiScriptHost host = null)
        {
            var script = "";

            //Put all the files in!
            var designFiles = new List<string>();
            designFiles.AddRange(Directory.GetFiles(LoadPath));
            designFiles.RemoveAll(n => n.Contains("ScriptConfig.xml") || n.Contains("StreamDeck.cs"));

            foreach (var file in designFiles)
                script += File.ReadAllText(file) + Environment.NewLine;

            script += Environment.NewLine;

            script += code;
            script = script.Replace("Submission#0.", "");

            return ScriptEngine.RunUiScript<T>(script, host);
        }

        private void CreateItem(listBoxItem item, string Name, string Text)
        {
            var ctrl = item.GetControl();
            item.countPressed++;
            if (Text != "")
                ctrl.Text = Text;
            TypeDescriptor.GetProperties(ctrl)["Name"].SetValue(ctrl, $"{Name}");
        }
    }
}
