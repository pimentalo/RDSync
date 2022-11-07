using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    /// <summary>
    /// Syncer is a Device and a list of sync rules + some sync elements
    /// </summary>
    [Serializable]
    public class Syncer
    {
        public string FriendlyName { get; set; } = "";

        public TimeSpan? Periodicity { get; set; }
        public DateTime? LastExecution { get; set; }
        public bool Automatic { get; set; }

        public string? EndPointIdentifier { get; set; } = null;

        private EndPoint? _Device;
        internal EndPoint? Device {  get { return _Device ?? (_Device = Helper.CreateEndPointFromIdentifier(EndPointIdentifier!));} }
        public List<Rule> Rules { get; set; } = new List<Rule>();

        internal void RaiseFileDetected(Rule rule, File file)
        {
            OnFileDetected(new RuleEventArgs(rule, file));
        }
        protected virtual void OnFileDetected(RuleEventArgs e)
        {
            EventHandler<RuleEventArgs> handler = FileDetected;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal void RaiseFileCopied(Rule rule, File file)
        {
            OnFileCopied(new RuleEventArgs(rule, file));
        }
        protected virtual void OnFileCopied(RuleEventArgs e)
        {
            EventHandler<RuleEventArgs> handler = FileCopied;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Thrown if a file is detected and different or missing locally
        /// </summary>
        public event EventHandler<RuleEventArgs> FileDetected;

        /// <summary>
        /// Thrown if a file has been copied
        /// </summary>
        public event EventHandler<RuleEventArgs> FileCopied;
        
        public class RuleEventArgs
        {
            internal RuleEventArgs(Rule rule, File file)
            {
                Rule = rule;  File = file;
            }

            public Rule Rule { get; set; }
            
            /// <summary>
            /// File path on device
            /// </summary>
            public File File { get; set; }
        } 

        public Syncer()
        {
        }

        public Syncer(EndPoint endpoint)
        {
            this.EndPointIdentifier = endpoint.DeviceIdentifier;
            this._Device = endpoint;
        }


        /// <summary>
        /// endPointIdentifier syntax: 
        /// [type]:[id]
        /// 
        /// Type is either Directory or Device.
        /// 
        /// If Type is "Directory" then id is a directory
        /// If type is "Device" then id is a pnp device id
        /// 
        /// Exemple:
        /// Directory:E:\
        /// 
        /// </summary>
        /// <param name="endPointIdentifier"></param>
        public Syncer(string endPointIdentifier):this()
        {
            EndPointIdentifier = endPointIdentifier;
        }

        
        public void Execute()
        {
            foreach(var r in Rules)
            {
                r.Execute(this);
            }
        }
      
    }
}
