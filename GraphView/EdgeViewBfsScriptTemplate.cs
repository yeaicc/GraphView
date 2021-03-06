﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace GraphView
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class EdgeViewBfsScriptTemplateStrategyTemplate : EdgeViewBfsScriptTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n\r\n");
            
            #line 9 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
  
	var typeDictionary = new Dictionary<string, Tuple<string, string, int>>
	{
		{"int", new Tuple<string, string, int>("int", "SqlInt32", 4)},
		{"long", new Tuple<string, string, int>("bigint", "SqlInt64", 8)},
		{"double", new Tuple<string, string, int>("float", "SqlDouble", 8)},
		{"string", new Tuple<string, string, int>("nvarchar(4000)", "SqlString", 4)},
		{"bool", new Tuple<string, string, int>("bit", "SqlBoolean", 1)}
	};
	var Name = Schema + "_" + NodeName + "_" + EdgeName;
	var columnNull = Tuple.Create("", "");

            
            #line default
            #line hidden
            this.Write("\r\ncreate function ");
            
            #line 22 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_bfsPath(\r\n\t\t@source bigint, \r\n\t\t@minlength bigint,\r\n\t\t@maxlength bigint");
            
            #line 25 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"

	for (int i = 0; i < EdgeColumn.Count; i++)
	{
		WriteLine(", ");
		Write("		");
		Write("@edge" + i.ToString() + " varbinary(max)");
		WriteLine(", ");
		Write("		");
		Write("@del" + i.ToString() + " varbinary(max)");
	}
	foreach (var it in Attribute) {
		WriteLine(", ");
		Write("		");
		Write("@" + it.Item1);
		Write(" " + typeDictionary[it.Item2].Item1);
	}
            
            #line default
            #line hidden
            this.Write(")\r\nreturns table\r\nas \r\nreturn \r\nwith  allPath(sink, varPath) as (\r\n\t\tselect newpa" +
                    "th.sink,  \r\n\t\tdbo.ConvertNumberIntoBinaryForPath(@source, EdgeColumnId, newpath." +
                    "EdgeId)\r\n\t\tfrom ");
            
            #line 48 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_Decoder(\r\n\t\t");
            
            #line 49 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
for (int i = 0; i < EdgeColumn.Count; i++)
		{
			if (i != 0) 
			{
				WriteLine(", ");
				Write("		");
			}
			Write("@edge" + i.ToString());
			WriteLine(", ");
			Write("		");
			Write("@del" + i.ToString());
		}
            
            #line default
            #line hidden
            this.Write(") as newpath\r\n\t\tWhere (@maxlength != 0)\r\n");
            
            #line 62 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
foreach (var it in Attribute) {
		Write("		");
		Write("and (");
		Write("@" + it.Item1 + " is null or ");
		WriteLine("@" + it.Item1 + " = newPath." + it.Item1 + ")");
}
            
            #line default
            #line hidden
            this.Write("\r\n\t\tunion all\r\n\r\n\t\tselect newpath.Sink, allpath.varPath + \r\n\t\tdbo.ConvertNumberIn" +
                    "toBinaryForPath(allpath.sink, EdgeColumnId, newpath.EdgeId) as Path\r\n\t\tfrom (all" +
                    "Path join ");
            
            #line 73 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView on  allPath.sink = ");
            
            #line 73 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView.GlobalNodeId)\r\n\t\tcross apply ");
            
            #line 74 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_ExclusiveEdgeGenerator(allPath.varPath, ");
            
            #line 74 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView.GlobalNodeId");
            
            #line 74 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"

			foreach (var it in EdgeColumn){
			if (it.Equals(columnNull)) 
			{
				WriteLine(",");
				Write("		");
				Write("null, null");
			}
			else
			{
				WriteLine(",");
				Write("		");
				Write(Name + "_SubView." + it.Item1 + "_" + it.Item2 + ",");
				Write(Name + "_SubView." + it.Item1 + "_" + it.Item2 + "DeleteCol");
			}
		  }
            
            #line default
            #line hidden
            this.Write(") as newPath\r\n\t\tWhere (@maxlength = -1 or DATALENGTH(allPath.varPath) <= (@maxlen" +
                    "gth - 1) * 20)\r\n");
            
            #line 91 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
foreach (var it in Attribute) {
		Write("		");
		Write("and (");
		Write("@" + it.Item1 + " is null or ");
		WriteLine("@" + it.Item1 + " = newPath." + it.Item1 + ")");
}
            
            #line default
            #line hidden
            this.Write(")\r\nselect @source as sink, CAST(0x as varbinary(max)) as varPath\r\nwhere @minlengt" +
                    "h = 0\r\nunion\r\nselect *\r\nfrom allPath\r\nwhere DATALENGTH(allPath.varPath) >= @minl" +
                    "ength * 20\r\n\r\nGO\r\n\r\ncreate function ");
            
            #line 107 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_bfsPathWithMessage(@source bigint, \r\n\t\t@minlength bigint, @maxlength bigint,\r\n\t\t" +
                    "@nodeType nvarchar(max), @id nvarchar(max)");
            
            #line 109 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"

	for (int i = 0; i < EdgeColumn.Count; i++)
	{
		WriteLine(", ");
		Write("		");
		Write("@edge" + i.ToString() + " varbinary(max)");
		WriteLine(", ");
		Write("		");
		Write("@del" + i.ToString() + " varbinary(max)");
	}
	foreach (var it in Attribute) {
		WriteLine(", ");
		Write("		");
		Write("@" + it.Item1);
		Write(" " + typeDictionary[it.Item2].Item1);
	}
            
            #line default
            #line hidden
            this.Write(")\r\nreturns table\r\nas \r\nreturn \r\nwith  allPath(sink, varPath, PathMessage) as (\r\n\t" +
                    "\tselect newpath.sink,  \r\n\t\tdbo.ConvertNumberIntoBinaryForPath(@source,  EdgeColu" +
                    "mnId, newpath.EdgeId) as varPath,\r\n\t\tdbo.");
            
            #line 132 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_PathMessageEncoder(@nodeType, @id,\r\n\t\t\tnewpath._EdgeType\r\n\t\t\t");
            
            #line 134 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
foreach (var it in Attribute) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t,newpath.");
            
            #line 135 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(it.Item1));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t");
            
            #line 136 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write(") as PathMessage\r\n\t\tfrom ");
            
            #line 137 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_Decoder(\r\n\t\t");
            
            #line 138 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
for (int i = 0; i < EdgeColumn.Count; i++)
		{
			if (i != 0) 
			{
				WriteLine(", ");
				Write("		");
			}
			Write("@edge" + i.ToString());
			WriteLine(", ");
			Write("		");
			Write("@del" + i.ToString());
		}
            
            #line default
            #line hidden
            this.Write(") as newpath\r\n\t\tWhere (@maxlength != 0)\r\n");
            
            #line 151 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
foreach (var it in Attribute) {
		Write("		");
		Write("and (");
		Write("@" + it.Item1 + " is null or ");
		WriteLine("@" + it.Item1 + " = newPath." + it.Item1 + ")");
}
            
            #line default
            #line hidden
            this.Write("\r\n\t\tunion all\r\n\r\n\t\tselect newpath.Sink, allpath.varPath + \r\n\t\tdbo.ConvertNumberIn" +
                    "toBinaryForPath(allpath.sink, EdgeColumnId, newpath.EdgeId) as varPath,\r\n\t\t(allp" +
                    "ath.PathMessage + dbo.");
            
            #line 162 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_PathMessageEncoder(");
            
            #line 162 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView._NodeType,\r\n\t\t\t");
            
            #line 163 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView._NodeId,\r\n\t\t\tnewpath._EdgeType\r\n\t\t\t");
            
            #line 165 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
foreach (var it in Attribute) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t, newpath.");
            
            #line 166 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(it.Item1));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t");
            
            #line 167 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write(")) as PathMessage\r\n\t\tfrom (allPath join ");
            
            #line 168 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView on  allPath.sink = ");
            
            #line 168 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView.GlobalNodeId)\r\n\t\tcross apply ");
            
            #line 169 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_ExclusiveEdgeGenerator(allPath.varPath, ");
            
            #line 169 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Name));
            
            #line default
            #line hidden
            this.Write("_SubView.GlobalNodeId");
            
            #line 169 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"

			foreach (var it in EdgeColumn){
			if (it.Equals(columnNull)) 
			{
				WriteLine(",");
				Write("		");
				Write("null, null");
			}
			else
			{
				WriteLine(",");
				Write("		");
				Write(Name + "_SubView." + it.Item1 + "_" + it.Item2 + ",");
				Write(Name + "_SubView." + it.Item1 + "_" + it.Item2 + "DeleteCol");
			}
		  }
            
            #line default
            #line hidden
            this.Write(") as newPath\r\n\t\tWhere (@maxlength = -1 or DATALENGTH(allPath.varPath) <= (@maxlen" +
                    "gth - 1) * 20)\r\n");
            
            #line 186 "D:\Source\graphview\GraphView\EdgeViewBfsScriptTemplate.tt"
foreach (var it in Attribute) {
		Write("		");
		Write("and (");
		Write("@" + it.Item1 + " is null or ");
		WriteLine("@" + it.Item1 + " = newPath." + it.Item1 + ")");
}
            
            #line default
            #line hidden
            this.Write(")\r\nselect @source as sink, CAST(0x as varbinary(max)) as varPath, CAST(0x as varb" +
                    "inary(max)) as PathMessage\r\nwhere @minlength = 0\r\nunion\r\nselect *\r\nfrom allPath\r" +
                    "\nwhere DATALENGTH(allPath.varPath) >= @minlength * 20\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class EdgeViewBfsScriptTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
