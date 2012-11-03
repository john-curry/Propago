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

namespace WindowsGame1
{
    public partial class LevelEditor : Form
    {
        #region editorvariables
        EditorState editorState;
        public EditorState CurrentState { get { return editorState; } }
        
        GraphicsDevice graphics;
        Microsoft.Xna.Framework.Rectangle bounds;

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
        public static string Type
        {
            get
            {
                return le.TypeBox.Text;
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
        #endregion

        #region private property varables
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
            editorState = new DrawRectangle();
            
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "png Files (.png)|*.png|All Files (*.*)|*.*";
            
            sc = new state_Click(editorState.Click);
            sr = new state_Release(editorState.Release);
            sd = new state_Dragging(editorState.Drag);

            addToolStripMenuItem.Click += new EventHandler(addToolStripMenuItem_Click);
            
            isDown = false;

            //populate the Color & textures dropdown boxs
            TextureBox.DataSource = TexturesByName.Keys.ToList<string>();
            TextureBox.SelectedIndex = 3;

            ColorBox.DataSource = ColourHelper.GetColorNames();
            ColorBox.SelectedIndex = 1;

            TypeBox.SelectedIndex = 0;
        }

        void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                FileStream fs =(FileStream) openFileDialog1.OpenFile();
                LevelEditor.AddToTextures(openFileDialog1.SafeFileName, Texture2D.FromStream(graphics, fs));
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


        public static void MouseEventListener()
        {
            MouseState mms = Mouse.GetState();

            if (mms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (!isDown)
                {
                    isDown = true;
                    sc.Invoke(mms);
                }
            }

            if (mms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                if (isDown)
                {
                    isDown = false;
                    sr.Invoke(mms);
                }
            }

            if (isDown)
                sd.Invoke(mms);
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
            var next = new DrawRectangle();
            sc -= editorState.Click;
            sd -= editorState.Drag;
            sr -= editorState.Release;

            sc += next.Click;
            sr += next.Release;
            sd += next.Drag;

            editorState = next;
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            var next = new Edit(Mouse.GetState());
            sc -= editorState.Click;
            sd -= editorState.Drag;
            sr -= editorState.Release;

            sc += next.Click;
            sr += next.Release;
            sd += next.Drag;

            editorState = next;

        }

        private void ColorBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void TypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }








    }
}
