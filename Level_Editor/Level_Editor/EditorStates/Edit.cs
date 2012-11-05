using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WindowsGame1;
namespace WindowsGame1
{
    enum Corner
    {
        TopLeft = 0, TopRight = 1, BottomLeft = 2, BottomRight = 3, None
    }

    enum EditState
    {
        Resize, Move, Default
    }

    delegate void Click(MouseState ms, List<GameObject> gameobjects);
    delegate void Release(MouseState ms, List<GameObject> gameobjects);
    delegate void Drag(MouseState ms, List<GameObject> gameobjects);
    delegate bool IsState(MouseState ms, List<GameObject> gameobjects);
    class Edit : EditorState
    {
        Dictionary<EditState, Click> Clicks = new Dictionary<EditState, Click>();
        Dictionary<EditState, Drag> Drags = new Dictionary<EditState, Drag>();
        Dictionary<EditState, Release> Releases = new Dictionary<EditState, Release>();
        Dictionary<EditState, IsState> Checks = new Dictionary<EditState, IsState>();
        EditState cState = EditState.Default;
        
        public Edit(MouseState ms)
        {
            Clicks.Add(EditState.Move, Move.Click);
            Drags.Add(EditState.Move, Move.Drag);
            Releases.Add(EditState.Move, Move.Release);

            Clicks.Add(EditState.Resize, Resize.ResizeClick);
            Drags.Add(EditState.Resize, Resize.ResizeDrag);
            Releases.Add(EditState.Resize, Resize.ResizeRelease);



            //Checks.Add(EditState.Default, Default.IsState);
            Clicks.Add(EditState.Default, Default.DefaultClick);
            Drags.Add(EditState.Default, Default.DefaultDrag);
            Releases.Add(EditState.Default, Default.DefaultRelease);


            Checks.Add(EditState.Move, Move.IsState);
            Checks.Add(EditState.Resize, Resize.isResize);
            
        }

        public void Click(MouseState ms) 
        {
            foreach (EditState es in Checks.Keys)
            {
                if (Checks[es].Invoke(ms, LevelEditor.GetEditorObjects()))
                    cState = es;
            }

            if (!Checks.Keys.Any((y) => (Checks[y].Invoke(ms,LevelEditor.GetEditorObjects()))))
                cState = EditState.Default;

            Console.WriteLine(cState.ToString());
            Clicks[cState].Invoke(ms, LevelEditor.GetEditorObjects());
        }

        public void Release(MouseState ms) 
        {
            Releases[cState].Invoke(ms, LevelEditor.GetEditorObjects());
        }
        public void Drag(MouseState ms)
        {
            Drags[cState].Invoke(ms, LevelEditor.GetEditorObjects());
        }

        public void Draw(SpriteBatch sp)
        {


            LevelEditor.GetEditorObjects().ForEach(delegate(GameObject go)
            {
                foreach (Rectangle r in CalcCorners(go))
                {
                    sp.Draw(
                        LevelEditor.GetTextureByName("white"),
                        r,
                        Color.Black);
                }
            });

        }

        public void Save() { }
        public static Rectangle[] CalcCorners(GameObject gameobject)
        {
            Rectangle[] recs = new Rectangle[4];
            var trec = gameobject as TRectangularObject;
            recs[0] = new Rectangle(trec.X - 5, trec.Y - 5, 10, 10);
            recs[1] = new Rectangle(trec.Width + trec.X - 5, trec.Y - 5, 10, 10);
            recs[2] = new Rectangle(trec.X - 5, trec.Y + trec.Height - 5, 10, 10);
            recs[3] = new Rectangle(trec.Width + trec.X - 5, trec.Y + trec.Height - 5, 10, 10);
            return recs;
        }
    }
}
