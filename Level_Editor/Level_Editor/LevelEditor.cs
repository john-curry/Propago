using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
namespace WindowsGame1
{
    public enum EditorState
    {
        Draw, Edit
    }

    public partial class LevelEditor : Form
    {
        #region editorvariables
        EditorState editorState;
        public EditorState CurrentState { get { return editorState; } }
        public ComboBox TypeBox { get { return typeBox; } } 
        GraphicsDevice graphics;
        Microsoft.Xna.Framework.Rectangle bounds;
        public Edit edit;
        string directory;
        string name;


        #endregion
        
        #region static properties
        public static Texture2D Background 
        {
            get 
            { 
                return background; 
            }
            set
            {
                background = value;
            }
        }
        public static Edit EditState
        {
            get
            {
                return le.edit;
            }
        }

        public static string Type
        {
            get
            {
                return le.typeBox.Text;
            }
        }
        public static string Coloring
        {
            get
            {
                return le.ColorBox.Text;
            }
        }
        public static string Texturing
        {
            get
            {
                return le.TextureBox.Text;
            }
        }
        public static Texture2D Test
        {
            get
            {
                return test;
            }
        }
        public static Dictionary<string, Texture2D> Textures 
        { 
            get 
            {
                return TexturesByName; 
            }
        }
        public static LevelEditor Editor 
        {
            get
            {
                return le; 
            }
        }
        public static EditorState State 
        {
            get
            { 
                return le.editorState; 
            } 
        }
        public static Texture2D Scene
        {
            get
            {
                return scene;
            }
            set
            {
                scene = value;
            }
        }
        public static List<string> ObjectTypes
        {
            get
            {
                return objecttypes;
            }
        }

        #endregion

        #region private property varables
        private static List<string> objecttypes = new List<string>();
        private static List<GameObject> GO = new List<GameObject>();
        private static LevelEditor le;
        
       
        private static Texture2D test;
        private static Texture2D background;
        //used to populate Textures dropdown box

        private static Dictionary<string, Texture2D> TexturesByName =
            new Dictionary<string, Texture2D>();
        
        //used to change the behaviour of the editor


        private static Texture2D scene;

        #endregion

        #region MouseEventDelegateStateRegion
        public delegate void state_Click(MouseState ms);
        public delegate void state_Release(MouseState ms);
        public delegate void state_Dragging(MouseState ms);

        public static event state_Click sc;
        public static event state_Release sr;
        public static event state_Dragging sd;
        
        static bool isDown;
        #endregion

        private LevelEditor(Microsoft.Xna.Framework.Rectangle bounds, GraphicsDevice graphics)
        {
            InitializeComponent();
            this.graphics = graphics;
            this.bounds = bounds;

            GO = new List<GameObject>();
            editorState = EditorState.Draw;
            
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "png Files (.png)|*.png|All Files (*.*)|*.*";

            edit = new Edit();

            addToolStripMenuItem.Click += new EventHandler(addToolStripMenuItem_Click);

            addTypeToolStripMenuItem1.Click += (sender, e) => 
            { (new TypeForm()).ShowDialog(); };

            
            //populate the Color & textures dropdown boxs
            TextureBox.DataSource = TexturesByName.Keys.ToList<string>();
            TextureBox.SelectedIndex = 3;

            ColorBox.DataSource = ColourHelper.GetColorNames();
            ColorBox.SelectedIndex = 1;
            
            //TypeBox.SelectedIndex = 0;
            objecttypes.Add("platform");
            objecttypes.Add("player");
            objecttypes.Add("ladder");
            typeBox.DataSource = objecttypes;

            List<PictureBox> pictures = new List<PictureBox>()
            {
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5,
                pictureBox6,
                pictureBox7,
                pictureBox8,
                pictureBox9

            };

            pictures.ForEach((p) =>
            {
                p.DoubleClick += new EventHandler(PicBoxEvents.pictureBox_DoubleClick);
                p.BorderStyle = BorderStyle.FixedSingle;
                p.AllowDrop = true;

                p.MouseDown += new MouseEventHandler(PicBoxEvents.pictureBox_MouseDown);
            });
        }

        void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileStream fs =(FileStream) openFileDialog1.OpenFile();
                LevelEditor.AddToTextures(openFileDialog1.SafeFileName, Texture2D.FromStream(graphics, fs));
                TextureBox.DataSource = TexturesByName.Keys.ToList();
                fs.Close();
            }
        }

        public static void Initialize(Microsoft.Xna.Framework.Rectangle bounds, GraphicsDevice graphics)
        {
            if (le == null)
            {
                le = new LevelEditor(bounds, graphics);
                
                le.Activate();
                le.Show();
            }
            else
                throw new Exception("Level Editor Already initialized");
        }

        public static void AddToEditor(GameObject go)
        {
            if(go != null)
                GO.Add(go);
        }

        public static List<GameObject> GetEditorObjects()
        {
                return GO;
        }



        public static void AddToTextures(string key, Texture2D t)
        {
            TexturesByName.Add(key, t);
        }

        public static Texture2D GetTextureByName(string st)
        {
            if (TexturesByName.ContainsKey(st))
                return TexturesByName[st];
            else
            {
                throw new Exception("Texture not found");
           
            }
        }

        private void DrawRectangle_Click(object sender, EventArgs e)
        {
            editorState = EditorState.Draw;
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            editorState = EditorState.Edit;
        }
    
        private void LevelEditor_Load(object sender, EventArgs e) { }

        private void pNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
               SaveFileDialog saveFileDialog1 = new SaveFileDialog();
               saveFileDialog1.Filter = "png Image|*.png";
               saveFileDialog1.Title = "Save an Image File";
               saveFileDialog1.ShowDialog();

               if(saveFileDialog1.FileName != "")
               {
                  System.IO.FileStream fs = 
                     (System.IO.FileStream)saveFileDialog1.OpenFile();
                  LevelEditor.Scene.SaveAsPng(fs, bounds.Width, bounds.Height);
                  fs.Close();
              }

   
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundForm bf = new BackgroundForm();
            bf.Show();
            bf.Activate();
        }

        
        private void exportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Level level = new Level("temp", GO, bounds);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xml Level|*.xml";
            saveFileDialog1.Title = "Save an Level File";
            saveFileDialog1.ShowDialog();
            
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                level.LevelStream(fs);
                fs.Close();
            }
            
        }

        public static void MouseEventListener()
        {
            MouseState mms = Mouse.GetState();

            if (mms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (!isDown)
                {
                    isDown = true;
                    EditState.Click(mms);
                }
            }

            if (mms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                if (isDown)
                {
                    isDown = false;
                    EditState.Click(mms);
                }
            }

            if (isDown)
                EditState.Drag(mms);
        }

   
      

  
        







    }
}
