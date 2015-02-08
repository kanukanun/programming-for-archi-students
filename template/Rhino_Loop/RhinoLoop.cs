using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace Rhino_Loop
{
    [System.Runtime.InteropServices.Guid("8b95fc2e-b907-47d6-9aba-e4b0c95aabdf")]
    public class RhinoLoop : Command
    {
        public RhinoLoop()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RhinoLoop Instance
        {
            get;
            private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "RhinoLoop"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Loop loop = new Loop(); // あたらしいループを作ります。

            loop.Run(doc); // 実行!

            return Result.Success;
        }
    }
}
