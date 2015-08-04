using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace _4_how_to_use_help
{
    [System.Runtime.InteropServices.Guid("97a31f01-c518-4427-8944-cd6032a0cb80")]
    public class howtousehelpCommand : Command
    {
        public howtousehelpCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static howtousehelpCommand Instance
        {
            get;
            private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "howtousehelpCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Loop loop = new Loop();

            loop.Run(doc);

            return Result.Success;
        }
    }
}
