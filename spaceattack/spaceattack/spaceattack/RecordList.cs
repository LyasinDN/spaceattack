using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spaceattack.Helpers;
using System.Xml.Linq;

namespace spaceattack.GameObjects
{
    class RecordList
    {
        string recordListFile;
        static int nMaxResultCount = 20;
        public int playerScore;
        private int offsetRecordList;
        bool bKeyDownPressed = false, bKeyUpPressed=false;
        public struct recordNote
        {
            public int result;
            public string Name;
            public string date;
            public recordNote(int r, string n, string d)
            {
                result = r;
                Name = n;
                date = d;
            }
 
        }
        public List<recordNote> rList = new List<recordNote>();
        public RecordList(string filename)
        {
            if (File.Exists(filename))
                recordListFile = filename;
            else
                recordListFile = "records.xml";
            readFromFile();
            offsetRecordList = 0;
        //    createTestList();
        //    saveToFile();
           
        }

        public void saveToFile()
        {
            XDocument doc = new XDocument(
                new XElement("SpaceAttack")
                );
            foreach(recordNote res in rList)
            {
                XElement elem = new XElement("gameResult",
                    new XElement("Name", res.Name),
                    new XElement("Result", res.result.ToString()),
                    new XElement("Date", res.date)
                    );
                doc.Root.Add(elem);

            }

            doc.Save(recordListFile);

        }

        public void readFromFile()
        {
            rList.Clear();
            XDocument doc = XDocument.Load(recordListFile);
            foreach (XElement result in doc.Root.Elements())
            {

                rList.Add(new recordNote(Convert.ToInt32(result.Element("Result").Value), result.Element("Name").Value, result.Element("Date").Value));
                
            }
            sortByResult();  
        }

        private void sortByResult()
        {
            rList.Sort(
                delegate(recordNote x,  recordNote y) 
                {
                return y.result-x.result;
            
            });

        }
        public bool checkInTop(int result)
        {
            if (rList.Count <= nMaxResultCount) return true;
            if (rList[nMaxResultCount].result < result) return true;
            return false;

        }
        public void addResult(int r, string n, string d)
        {
            rList.Add(new recordNote(r, n, d));
            sortByResult();
            saveToFile();
        }

        private void createTestList()
        {
            DateTime dt = DateTime.Now;
            rList.Add(new recordNote(100, "Piter", dt.ToShortDateString()));
            dt = dt.AddYears(-2);
            rList.Add(new recordNote(10, "Jhon", dt.ToShortDateString()));
            dt = dt.AddYears(-4);
            rList.Add(new recordNote(200, "Tom", dt.ToShortDateString()));
            dt = dt.AddMonths(-2);
            rList.Add(new recordNote(150, "Alex", dt.ToShortDateString()));

        }
       

        public void Update()
        {
           
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                SpaceAttackGame.State = GameState.MainMenu;
                MainMenu.bEscPressed = true;
            }
            if (keyboard.IsKeyDown(Keys.Down) && bKeyDownPressed==false)
            {
                bKeyDownPressed = true;
                if(offsetRecordList<20 && offsetRecordList+5<=rList.Count)
                    offsetRecordList++;
            }
            if (keyboard.IsKeyDown(Keys.Up) && bKeyUpPressed == false)
            {
                bKeyUpPressed = true;
                if (offsetRecordList >0)
                    offsetRecordList--;
            }
             if (keyboard.IsKeyUp(Keys.Down))
                 bKeyDownPressed=false;
            if (keyboard.IsKeyUp(Keys.Up))
                 bKeyUpPressed=false;
            
         }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
              "Record list",
              new Vector2(215, 300), Color.DarkSeaGreen);

           int yPos = 460;
           for (int i = offsetRecordList; i < rList.Count && i < 5+offsetRecordList; i++)
           {
               spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
               rList[i].Name, new Vector2(50, yPos), Color.Gold);
               spritebatch.DrawString(LoadHelper.Fonts[FontEnum.RecordList],
               rList[i].date, new Vector2(250, yPos+20), Color.DarkSeaGreen);
               spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
               rList[i].result.ToString(), new Vector2(500, yPos), Color.Gold);
               yPos += 60;
           }
           spritebatch.End();
        }
    }
}
