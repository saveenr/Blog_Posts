using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoPS
{
    [System.Management.Automation.Cmdlet(System.Management.Automation.VerbsCommon.Get,"DemoNames")]
    public class Get_DemoNames: System.Management.Automation.PSCmdlet
    {

        [System.Management.Automation.Parameter(Position=0,Mandatory=false)]
        public string Prefix;

        protected override void ProcessRecord()
        {
            var names = new string[] {"Chris", "Charlie", "Isaac", "Simon"};
            if (string.IsNullOrEmpty(this.Prefix))
            {
                this.WriteObject(names, true);                
            }
            else
            {
                var prefixed_names = names.Select(n => this.Prefix + n);
                this.WriteObject(prefixed_names,true);
            }
        }
    }
}
