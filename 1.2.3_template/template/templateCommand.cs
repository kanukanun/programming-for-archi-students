using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace template
{
    [System.Runtime.InteropServices.Guid("b1e94423-4ce9-4cc9-8738-2f38835b1303")]
    public class templateCommand : Command
    {
        public templateCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static templateCommand Instance
        {
            get;
            private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "templateCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Loop loop = new Loop();
            loop.Run(doc);

            return Result.Success;
        }
    }
}
