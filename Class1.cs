using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace ManipulateObjects
{
    public class Class1
    {

        [CommandMethod("MultipleCopy")]
        public static void MultipleCopy()
        {
            //Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start a transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the Block table for reading
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the BlockTable Record Modelspace for write
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(0, 0, 0);
                        c1.Radius = 5.0;

                        // Add c1 to the Blocktable record
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        using (Circle c2 = new Circle())
                        {
                            c2.Center = new Point3d(0, 0, 0);
                            c2.Radius = 7.0;

                            // Add c2 to the Blocktable record
                            btr.AppendEntity(c2);
                            trans.AddNewlyCreatedDBObject(c2, true);


                        }
                    }

                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();
                }
            }

        }

        [CommandMethod("SingleCopy")]
        public static void SingleCopy()
        {
            // Get a handle for the drawing object
            // Get the current document and database
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            // Start transaction
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                try
                {
                    // Open the block table for reading
                    BlockTable bt;
                    bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block Table record Modelspace for writing
                    BlockTableRecord btr;
                    btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create a circle that is 2,3 with a radius of 4.25
                    using (Circle c1 = new Circle())
                    {
                        c1.Center = new Point3d(2, 3, 0);
                        c1.Radius = 4.25;

                        // Add the new object to the BlockTable Record
                        btr.AppendEntity(c1);
                        trans.AddNewlyCreatedDBObject(c1, true);

                        // Create a copy of the circle and change its radius 
                        Circle c1Clone = c1.Clone() as Circle;
                        c1Clone.Radius = 1.0;

                        // Add the clone circle to the BlockTableRecord
                        btr.AppendEntity(c1Clone);
                        trans.AddNewlyCreatedDBObject(c1Clone, true);

                    }
                    trans.Commit();
                }
                catch (System.Exception ex)
                {
                    doc.Editor.WriteMessage("Error encountered: " + ex.Message);
                    trans.Abort();                    
                }
            }
        }
    }
}
