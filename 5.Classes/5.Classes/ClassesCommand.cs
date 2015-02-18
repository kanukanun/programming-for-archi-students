using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace _5.Classes
{
    [System.Runtime.InteropServices.Guid("c2e383a3-9299-4b20-9abf-84f498c74fd3")]
    public class ClassesCommand : Command
    {
        public ClassesCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static ClassesCommand Instance
        {
            get;
            private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "ClassesCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Loop loop = new Loop();
            loop.Run(doc);

            return Result.Success;
        }
    }
}
