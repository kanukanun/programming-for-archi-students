using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace HelloWorld
{
    [System.Runtime.InteropServices.Guid("5f4140c8-2d71-4c7b-a361-27d44f189ce8")]
    public class HelloWorldCommand : Command
    {
        public HelloWorldCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HelloWorldCommand Instance
        {
            get;
            private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "HelloWorldCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {

            Loop loop = new Loop();

            loop.Run(doc); // execute

            return Result.Success;
        }
    }
}
