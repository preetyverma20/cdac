/***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DBManager.forms;
/***************************************************************************/
namespace DBManager.classes
{
	/// <summary>
	/// Summary description for Editor.
	/// </summary>
/***************************************************************************/
	public class MyEditorPanel : System.Windows.Forms.Panel
	{
/***************************************************************************/
		private System.Windows.Forms.ImageList imgListEditor;
		private System.ComponentModel.IContainer components;
/***************************************************************************/
		private bool runTextChange = true;
		private Font editorFont;
		private String [] keywords = { "create", "replace", "alter", "select", "update", 
										 "delete", "procedure", "function", "trigger", "view",
										 "declare", "begin", "end"
									 };
		private String [] errors = { "err", "error", "bad", "wrong", "incorrect"
								   };
		private System.Windows.Forms.ToolBar toolBarEditor;
		private System.Windows.Forms.ToolBarButton tbbSave;
		private System.Windows.Forms.ToolBarButton tbbCompile;
		private System.Windows.Forms.ToolBarButton tbbExecute;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton tbbFont;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.RichTextBox rtbEditor;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListBox lbErrors;
		private System.Windows.Forms.Panel panErrors;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCloseErrorPan; 
/***************************************************************************/
		private string objType;			// objType of object being edited. ie. procedure/trigger
		public Database database;
		public string schema_name;		
		public string created_by;		//user who has created this object
		public string object_name;		//name of the object being edited
		public TabPage myTabPage;
		public TreeNode objNode;		//Tree node corresponding to this object
/***************************************************************************/
		public MyEditorPanel(Database database, string schema, string objName, string username, 
			string objType, ImageList imgList, TreeNode objNode, string sql)
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			/*--------------------------------------------------------------------*/
			this.objType = objType;
			this.database = database;
			this.schema_name = schema;
			this.object_name = objName;
			this.created_by = username;
			this.objNode = objNode;
			this.Dock = DockStyle.Fill;
			/*--------------------------------------------------------------------*/
			this.toolBarEditor.ImageList = imgList;
			this.tbbCompile.ImageIndex = MyImageList.getImgList().getImageIndex("COMPILE");
			this.tbbExecute.ImageIndex = MyImageList.getImgList().getImageIndex("EXECUTE");
			this.tbbFont.ImageIndex = MyImageList.getImgList().getImageIndex("FONT");
			this.tbbSave.ImageIndex = MyImageList.getImgList().getImageIndex("SAVE");

			this.btnCloseErrorPan.ImageList = imgList;
			this.btnCloseErrorPan.ImageIndex = MyImageList.getImgList().getImageIndex("CLOSE");
			/*--------------------------------------------------------------------*/
			if (objType.ToUpper().Equals("TRIGGER") || objType.ToUpper().Equals("VIEW"))
			{
				//do not show the execute button on the toolbar
				this.tbbExecute.Visible = false;
			}
			else
			{
				this.tbbExecute.Visible = true;
			}
			/*--------------------------------------------------------------------*/
			editorFont = this.rtbEditor.Font;
			/*--------------------------------------------------------------------*/
			//first try to get the SQL from database
			string old_sql = getDBObjectSQL(this.objType, this.object_name);
			this.rtbEditor.Text = (old_sql.Length != 0) ? old_sql : sql;
			/*--------------------------------------------------------------------*/
			this.highlightSyntax(0, this.rtbEditor.Text);
			this.rtbEditor.Select(this.rtbEditor.Text.Length, 0);
			/*--------------------------------------------------------------------*/
			runTextChange = true;
			/*--------------------------------------------------------------------*/
			this.panErrors.Hide();
			/*--------------------------------------------------------------------*/
		}
/***************************************************************************/
		private string getDBObjectSQL(string objType, string object_name)
		{
			switch(objType.ToUpper())
			{
				case "FUNCTION" :	Function f = database.getFunction(schema_name, object_name);
									return (f == null) ? "" : f.function_sql;
				case "PROCEDURE" :	Procedure p = database.getProcedure(schema_name, object_name);
									return (p == null) ? "" : p.procedure_sql;
				case "TRIGGER" :	Trigger t = database.getTrigger(schema_name, object_name);
									return (t == null) ? "" : t.trigger_sql;
				case "VIEW" :		View v = database.getView(schema_name, object_name);
									return (v == null) ? "" : v.view_sql;
			};
			return "";
		}
/***************************************************************************/
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
/***************************************************************************/
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MyEditorPanel));
			this.rtbEditor = new System.Windows.Forms.RichTextBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panErrors = new System.Windows.Forms.Panel();
			this.lbErrors = new System.Windows.Forms.ListBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnCloseErrorPan = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.toolBarEditor = new System.Windows.Forms.ToolBar();
			this.tbbSave = new System.Windows.Forms.ToolBarButton();
			this.tbbCompile = new System.Windows.Forms.ToolBarButton();
			this.tbbExecute = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tbbFont = new System.Windows.Forms.ToolBarButton();
			this.imgListEditor = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			this.panErrors.SuspendLayout();
			this.panel2.SuspendLayout();
			// 
			// EditorPanel
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.rtbEditor);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panErrors);
			this.Controls.Add(this.toolBarEditor);
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "EditorPanel";
			this.Size = new System.Drawing.Size(608, 485);
			this.TabIndex = 0;
			// 
			// rtbEditor
			// 
			this.rtbEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbEditor.Location = new System.Drawing.Point(0, 28);
			this.rtbEditor.Name = "rtbEditor";
			this.rtbEditor.Size = new System.Drawing.Size(608, 337);
			this.rtbEditor.TabIndex = 13;
			this.rtbEditor.Text = "rtbEditor";
			this.rtbEditor.TextChanged += new System.EventHandler(this.rtbEditor_TextChanged);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 365);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(608, 8);
			this.splitter1.TabIndex = 12;
			this.splitter1.TabStop = false;
			// 
			// panErrors
			// 
			this.panErrors.BackColor = System.Drawing.SystemColors.Control;
			this.panErrors.Controls.Add(this.lbErrors);
			this.panErrors.Controls.Add(this.panel2);
			this.panErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panErrors.Location = new System.Drawing.Point(0, 373);
			this.panErrors.Name = "panErrors";
			this.panErrors.Size = new System.Drawing.Size(608, 112);
			this.panErrors.TabIndex = 11;
			// 
			// lbErrors
			// 
			this.lbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbErrors.Location = new System.Drawing.Point(0, 15);
			this.lbErrors.Name = "lbErrors";
			this.lbErrors.Size = new System.Drawing.Size(608, 95);
			this.lbErrors.TabIndex = 1;
			this.lbErrors.SelectedIndexChanged += new System.EventHandler(this.lbErrors_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel2.Controls.Add(this.btnCloseErrorPan);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(608, 15);
			this.panel2.TabIndex = 0;
			// 
			// btnCloseErrorPan
			// 
			this.btnCloseErrorPan.BackColor = System.Drawing.SystemColors.Control;
			this.btnCloseErrorPan.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCloseErrorPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			//this.btnCloseErrorPan.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseErrorPan.Image")));
			this.btnCloseErrorPan.Location = new System.Drawing.Point(592, 0);
			this.btnCloseErrorPan.Name = "btnCloseErrorPan";
			this.btnCloseErrorPan.Size = new System.Drawing.Size(16, 15);
			this.btnCloseErrorPan.TabIndex = 1;
			this.btnCloseErrorPan.Click += new System.EventHandler(this.btnCloseErrorPan_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(608, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Errors";
			// 
			// toolBarEditor
			// 
			this.toolBarEditor.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							 this.tbbSave,
																							 this.tbbCompile,
																							 this.tbbExecute,
																							 this.toolBarButton1,
																							 this.tbbFont});
			this.toolBarEditor.DropDownArrows = true;
			this.toolBarEditor.ImageList = this.imgListEditor;
			this.toolBarEditor.Location = new System.Drawing.Point(0, 0);
			this.toolBarEditor.Name = "toolBarEditor";
			this.toolBarEditor.ShowToolTips = true;
			this.toolBarEditor.Size = new System.Drawing.Size(608, 28);
			this.toolBarEditor.TabIndex = 8;
			this.toolBarEditor.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarEditor_ButtonClick);
			// 
			// tbbSave
			// 
			this.tbbSave.ImageIndex = 0;
			this.tbbSave.Tag = "Save";
			// 
			// tbbCompile
			// 
			this.tbbCompile.ImageIndex = 1;
			this.tbbCompile.Tag = "Compile";
			// 
			// tbbExecute
			// 
			this.tbbExecute.ImageIndex = 2;
			this.tbbExecute.Tag = "Execute";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbFont
			// 
			this.tbbFont.ImageIndex = 3;
			this.tbbFont.Tag = "Font";
			// 
			// imgListEditor
			// 
			//			this.imgListEditor.ImageSize = new System.Drawing.Size(16, 16);
			//			this.imgListEditor.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListEditor.ImageStream")));
			//			this.imgListEditor.TransparentColor = System.Drawing.Color.Transparent;
			
			//			// 
			//			// EditorForm
			//			// 
			//			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			//			this.ClientSize = new System.Drawing.Size(608, 485);
			//			this.Controls.Add(this.EditorPanel);
			//			this.Name = "EditorForm";
			//			this.Text = "Editor";
			this.panErrors.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion
/***************************************************************************/
		private void rtbEditor_TextChanged(object sender, System.EventArgs e)
		{
			if (this.runTextChange && this.rtbEditor.Text.Length > 0)
			{
				// Calculate the starting position of the current line.
				int start = 0, end = 0;
				if (rtbEditor.SelectionStart == 0)
				{
					start = rtbEditor.SelectionStart;
				}
				else
				{
					for (start = rtbEditor.SelectionStart - 1; start > 0; start--) 
					{
						if (rtbEditor.Text[start] == '\n') { start++; break; }
					}
				}

				// Calculate the end position of the current line.
				for (end = rtbEditor.SelectionStart; end < rtbEditor.Text.Length; end++) 
				{
					if (rtbEditor.Text[end] == '\n') break;
				} 

				// Extract the current line that is being edited.
				String line = rtbEditor.Text.Substring(start, end - start);

				// Backup the users current selection point.
				int selectionStart = rtbEditor.SelectionStart;
				int selectionLength = rtbEditor.SelectionLength;
				
				highlightSyntax(start, line);

				// Restore the users current selection point. 
				rtbEditor.SelectionStart = selectionStart;
				rtbEditor.SelectionLength = selectionLength; 
			}
		}
/***************************************************************************/
//		static void Main() 
//		{
//			Application.Run(new EditorForm("PROCEDURE"));
//		}
/***************************************************************************/
		public void highlightSyntax(int start, string line)
		{
			runTextChange = false;

			// Split the line into tokens.
			Regex r = new Regex("([ \\t{}();])");
			string [] tokens = r.Split(line);
			int index = start;
			foreach (string token in tokens) 
			{
				if (token.Trim().Length == 0)
				{
					index += token.Length;
					continue;
				}
				// Set the token's default color and font.
				rtbEditor.SelectionStart = index;
				rtbEditor.SelectionLength = token.Length;
				rtbEditor.SelectionColor = Color.Black;
				//rtbEditor.SelectionFont = new Font("Courier New", 10, FontStyle.Regular);
				rtbEditor.SelectionFont = this.editorFont;

				// Check whether the token is a keyword. 
				for (int i = 0; i < keywords.Length; i++) 
				{
					if (keywords[i].ToUpper().Equals(token.Trim().ToUpper()))
					{
						// Apply alternative color and font to highlight keyword. 
						rtbEditor.SelectionColor = Color.Blue;
						//rtbEditor.SelectionFont = new Font("Courier New", 10, FontStyle.Bold);
						rtbEditor.SelectionFont = this.editorFont;
						break;
					}
				}
				index += token.Length;
			}
			runTextChange = true;

		}
/***************************************************************************/
		private void toolBarEditor_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			ToolBarButton tbbClicked = (ToolBarButton)e.Button;
			if (tbbClicked.Tag.ToString().ToUpper().Equals("SAVE"))
			{
				//save the contents of the rich text box
				save(true);
			}
			else if (tbbClicked.Tag.ToString().ToUpper().Equals("COMPILE"))
			{
				//compile and give error message
				//search for all lines that contain word "error" or "err"
				//and populate these errors in list box
				compile();
			}
			else if (tbbClicked.Tag.ToString().ToUpper().Equals("EXECUTE"))
			{
				//execute the procedure
			}
			else if (tbbClicked.Tag.ToString().ToUpper().Equals("FONT"))
			{
				//show font dialog box 
				FontDialog fd = new FontDialog();
				fd.ShowColor = false;
				fd.ShowEffects = false;
				fd.ShowApply = false;
				if (fd.ShowDialog() == DialogResult.OK)
				{
					//change the rich text box font
					this.editorFont = fd.Font;
					this.rtbEditor.Font = fd.Font;
					highlightSyntax(0, this.rtbEditor.Text);
				}
				
			}
		}
/***************************************************************************/
		private void lbErrors_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			//go to the particular line and select that errorneous line
//			//extract the erroneous word and select it
//			string errorDesc = (string)this.lbErrors.Items[this.lbErrors.SelectedIndex];
//			string errWord = errorDesc.Substring(errorDesc.LastIndexOf(":") + 1).Trim();
//
//			// Determine the starting location of the word "errWord".
//			int index = this.rtbEditor.Text.IndexOf(errWord);
//			// Determine if the word has been found and select it if it was.
//			if (index != -1)
//			{
//				// Select the string using the index and the length of the string.
//				this.rtbEditor.Select(index, errWord.Length);
//			}
		}
/***************************************************************************/
		private void compile()
		{
			//save the object first
			save(false);

			this.lbErrors.Items.Clear();
			Regex r1 = new Regex("\\n");
			string [] lines = r1.Split(this.rtbEditor.Text);
			int lineCount = 0;
			foreach (string line in lines)
			{
				lineCount++;
				Regex r2 = new Regex("([ \\t{}();])");
				string [] words = r2.Split(line);
				foreach (string word in words) 
				{
					// Check whether the word is an errorneous word. 
					for (int i = 0; i < errors.Length; i++) 
					{
						if (errors[i].ToUpper().Equals(word.ToUpper()))
						{
							//add this line number in list box
							this.lbErrors.Items.Add(lineCount + " : " + " Invalid word : " + word);
							break;
						}
					}
				}
			}
			if (this.lbErrors.Items.Count > 0)
			{
				this.splitter1.Show();
				this.panErrors.Show();
				if (this.objType.ToUpper().Equals("FUNCTION") || 
					this.objType.ToUpper().Equals("PROCEDURE") ||
					this.objType.ToUpper().Equals("TRIGGER"))
					change_compiled_status("WITH_ERRORS");
				MessageBox.Show(this.objType + " compiled with errors.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				if (this.objType.ToUpper().Equals("FUNCTION") || 
					this.objType.ToUpper().Equals("PROCEDURE") ||
					this.objType.ToUpper().Equals("TRIGGER"))
					change_compiled_status("COMPILED");
				MessageBox.Show(this.objType + " compiled successfully.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.splitter1.Hide();
				this.panErrors.Hide();
			}
		}
/***************************************************************************/
		private void change_compiled_status(string status)
		{
			//change the image
			change_image(status);

			TabControl tc = (TabControl)this.myTabPage.Parent;
			DBManagerMainForm main = (DBManagerMainForm)tc.Parent;

			switch(this.objType.ToUpper())
			{
				case "FUNCTION" :	Function f = this.database.getFunction(schema_name, object_name);
									f.isCompiled = status.Equals("COMPILED") ? true : false;
									main.getActiveConnection().saveDatabase();
									break;
				case "PROCEDURE" :	Procedure p = this.database.getProcedure(schema_name, object_name);
									p.isCompiled = status.Equals("COMPILED") ? true : false;
									main.getActiveConnection().saveDatabase();
									break;
				case "TRIGGER" :	Trigger t = this.database.getTrigger(schema_name, object_name);
									t.isCompiled = status.Equals("COMPILED") ? true : false;
									main.getActiveConnection().saveDatabase();break;
				case "VIEW" :		View v = this.database.getView(schema_name, object_name);
									v.isCompiled = status.Equals("COMPILED") ? true : false;
									main.getActiveConnection().saveDatabase();
									break;
			};
		}
/***************************************************************************/
		private void change_image(string imgType)
		{
			string imgName = "";
			if (imgType.Equals("WITH_ERRORS"))
				imgName = this.objType.ToUpper() + "_" + imgType;
			else
				imgName = imgType + "_" + this.objType.ToUpper();

			//change image of Object Node in DB Objects Tree View
			this.objNode.ImageIndex = MyImageList.getImgList().getImageIndex(imgName);
			this.objNode.SelectedImageIndex = MyImageList.getImgList().getImageIndex(imgName);

			//change image of Tab page
			this.myTabPage.ImageIndex = MyImageList.getImgList().getImageIndex(imgName);

			//change the image of window node in Connections and Windows Tree View
			string winNodeName = this.myTabPage.Text.Substring(this.myTabPage.Text.IndexOf("-") + 1).Trim();
			TabControl tc = (TabControl)this.myTabPage.Parent;
			DBManagerMainForm main = (DBManagerMainForm)tc.Parent;
			TreeNode winNode = main.getWindowNodeByName(winNodeName);
			if (winNode != null)
			{
				winNode.ImageIndex = MyImageList.getImgList().getImageIndex(imgName);
				winNode.SelectedImageIndex = MyImageList.getImgList().getImageIndex(imgName);
			}
			//change icon in Window Status Panel
			main.sbpanActiveWindow.Icon = MyImageList.getImgList().getIcon(imgName);
		}
/***************************************************************************/
		private void btnCloseErrorPan_Click(object sender, System.EventArgs e)
		{
			this.splitter1.Hide();
			this.panErrors.Hide();
		}
/***************************************************************************/
		private void save(bool giveMessage)
		{
			TabControl tc = (TabControl)this.myTabPage.Parent;
			DBManagerMainForm main = (DBManagerMainForm)tc.Parent;
			switch(this.objType.ToUpper())
			{
				case "FUNCTION" : if (this.database.updateFunction(schema_name,object_name, this.rtbEditor.Text) &&
									  main.getActiveConnection().saveDatabase())
								  {
									  if (giveMessage)
									  {
										  MessageBox.Show(this.objType + " '" + this.object_name + "' saved successfully.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
										  return;
									  }
								  }
								  else
								  {
									  if (giveMessage)
									  {
										  MessageBox.Show("Could not save " + this.object_name + " '" + this.object_name + "'.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
										  return;
									  }
								  }
									break;
				case "PROCEDURE" : if (this.database.updateProcedure(schema_name,object_name, this.rtbEditor.Text) &&
									   main.getActiveConnection().saveDatabase())
								   {
									   if (giveMessage)
									   {
										   MessageBox.Show(this.objType + " '" + this.object_name + "' saved successfully.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
										   return;
									   }
								   }
								   else
								   {
									   if (giveMessage)
									   {
										   MessageBox.Show("Could not save " + this.object_name + " '" + this.object_name + "'.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
										   return;
									   }
								   }
									break;
				case "TRIGGER" : if (this.database.updateTrigger(schema_name,object_name, this.rtbEditor.Text) &&
									 main.getActiveConnection().saveDatabase())
								 {
									 if (giveMessage)
									 {
										 MessageBox.Show(this.objType + " '" + this.object_name + "' saved successfully.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
										 return;
									 }
								 }
								 else
								 {
									 if (giveMessage)
									 {
										 MessageBox.Show("Could not save " + this.object_name + " '" + this.object_name + "'.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
										 return;
									 }
								 }
									break;
				case "VIEW" : if (this.database.updateView(schema_name,object_name, this.rtbEditor.Text) &&
								  main.getActiveConnection().saveDatabase())
							  {
								  if (giveMessage)
								  {
									  MessageBox.Show(this.objType + " '" + this.object_name + "' saved successfully.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
									  return;
								  }
							  }
							  else
							  {
								  if (giveMessage)
								  {
									  MessageBox.Show("Could not save " + this.objType + " '" + this.object_name + "'.", "DB Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
									  return;
								  }
							  }
								break;
			};
		}
/***************************************************************************/
	}
/***************************************************************************/
}
/***************************************************************************/
