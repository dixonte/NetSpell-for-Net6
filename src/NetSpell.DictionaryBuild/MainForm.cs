// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Affix;
using NetSpell.SpellChecker.Dictionary.Phonetic;


namespace NetSpell.DictionaryBuild
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{

		private ArrayList _fileList = new ArrayList();
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolStripButton copyBarButton;
		private System.Windows.Forms.ToolStripButton cutBarButton;
		private System.Windows.Forms.ToolStrip editToolBar;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuEdit;
		private System.Windows.Forms.ToolStripMenuItem menuEditCopy;
		private System.Windows.Forms.ToolStripMenuItem menuEditCut;
		private System.Windows.Forms.ToolStripMenuItem menuEditPaste;
		private System.Windows.Forms.ToolStripMenuItem menuEditSelect;
		private System.Windows.Forms.ToolStripMenuItem menuEditUndo;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuFileClose;
		private System.Windows.Forms.ToolStripMenuItem menuFileCloseAll;
		private System.Windows.Forms.ToolStripMenuItem menuFileExit;
		private System.Windows.Forms.ToolStripMenuItem menuFileNew;
		private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem menuFileSave;
		private System.Windows.Forms.ToolStripMenuItem menuFileSaveAll;
		private System.Windows.Forms.ToolStripMenuItem menuHelp;
		private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
		private System.Windows.Forms.ToolStripMenuItem menuItem3;
		private System.Windows.Forms.ToolStripMenuItem menuItem5;
		private System.Windows.Forms.ToolStripMenuItem menuItem8;
		private System.Windows.Forms.ToolStripMenuItem menuItem9;
		private System.Windows.Forms.ToolStripMenuItem menuWindow;
		private System.Windows.Forms.ToolStripMenuItem menuWindowCascade;
		private System.Windows.Forms.ToolStripMenuItem menuWindowHorizontal;
		private System.Windows.Forms.ToolStripMenuItem menuWindowVertical;
		private System.Windows.Forms.ToolStripButton newBarButton;
		private System.Windows.Forms.ToolStripButton openBarButton;
		private System.Windows.Forms.ToolStripButton pasteBarButton;
		private System.Windows.Forms.ToolStripButton saveBarButton;
		private System.Windows.Forms.ImageList toolBarImages;
		internal System.Windows.Forms.StatusStrip statusBar;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton undoBarButton;


		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}


		private TextBox GetActiveTextBox()
		{
			if (this.ActiveMdiChild != null)
			{
				if (this.ActiveMdiChild.ActiveControl != null)
				{
					if (this.ActiveMdiChild.ActiveControl.GetType() == typeof(TextBox))
					{
						return (TextBox)this.ActiveMdiChild.ActiveControl;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//this.menuFileCloseAll_Click(sender, new EventArgs());
		}

		private void menuEditCopy_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Copy();
			}
		}

		private void menuEditCut_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Cut();
			}

		}

		private void menuEditPaste_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Paste();
			}

		}

		private void menuEditSelect_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.SelectAll();
			}

		}

		private void menuEditUndo_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Undo();
			}

		}

		private void menuFileClose_Click(object sender, System.EventArgs e)
		{
			if (this.ActiveMdiChild != null)
			{
				this.ActiveMdiChild.Close();
			}
		}

		private void menuFileCloseAll_Click(object sender, System.EventArgs e)
		{
			foreach (Form child in this.MdiChildren)
			{
				child.Close();
			}
		}

		private void menuFileExit_Click(object sender, System.EventArgs e)
		{
			this.menuFileCloseAll_Click(sender, e);
			Application.Exit();
		}

		private void menuFileNew_Click(object sender, System.EventArgs e)
		{
			DictionaryForm newForm = new DictionaryForm();
			newForm.MdiParent = this;
			newForm.Show();
		}

		private void menuFileOpen_Click(object sender, System.EventArgs e)
		{
			DictionaryForm newForm = new DictionaryForm();

			if (newForm.OpenDictionary())
			{
				newForm.MdiParent = this;
				newForm.Show();
			}
		}

		private void menuFileSave_Click(object sender, System.EventArgs e)
		{
			if (this.ActiveMdiChild != null)
			{
				DictionaryForm child = (DictionaryForm)this.ActiveMdiChild;
				child.SaveDictionary();
			}
		}

		private void menuFileSaveAll_Click(object sender, System.EventArgs e)
		{
			foreach (DictionaryForm child in this.MdiChildren)
			{
				child.SaveDictionary();
			}
		}

		private void menuHelpAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);
		}

	

		private void menuWindowCascade_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}

		private void menuWindowHorizontal_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuWindowVertical_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindowHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindowVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWindowCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
            this.editToolBar = new System.Windows.Forms.ToolStrip();
            this.newBarButton = new System.Windows.Forms.ToolStripButton();
            this.openBarButton = new System.Windows.Forms.ToolStripButton();
            this.saveBarButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutBarButton = new System.Windows.Forms.ToolStripButton();
            this.copyBarButton = new System.Windows.Forms.ToolStripButton();
            this.pasteBarButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.undoBarButton = new System.Windows.Forms.ToolStripButton();
            this.mainMenu.SuspendLayout();
            this.editToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 395);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(648, 22);
            this.statusBar.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuWindow,
            this.menuHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(200, 24);
            this.mainMenu.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileClose,
            this.menuFileCloseAll,
            this.menuItem5,
            this.menuFileSave,
            this.menuFileSaveAll,
            this.menuItem9,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.Size = new System.Drawing.Size(120, 22);
            this.menuFileNew.Text = "New";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Size = new System.Drawing.Size(120, 22);
            this.menuFileOpen.Text = "Open...";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileClose
            // 
            this.menuFileClose.Name = "menuFileClose";
            this.menuFileClose.Size = new System.Drawing.Size(120, 22);
            this.menuFileClose.Text = "Close";
            this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
            // 
            // menuFileCloseAll
            // 
            this.menuFileCloseAll.Name = "menuFileCloseAll";
            this.menuFileCloseAll.Size = new System.Drawing.Size(120, 22);
            this.menuFileCloseAll.Text = "Close All";
            this.menuFileCloseAll.Click += new System.EventHandler(this.menuFileCloseAll_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Name = "menuItem5";
            this.menuItem5.Size = new System.Drawing.Size(120, 22);
            this.menuItem5.Text = "-";
            // 
            // menuFileSave
            // 
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.Size = new System.Drawing.Size(120, 22);
            this.menuFileSave.Text = "Save";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileSaveAll
            // 
            this.menuFileSaveAll.Name = "menuFileSaveAll";
            this.menuFileSaveAll.Size = new System.Drawing.Size(120, 22);
            this.menuFileSaveAll.Text = "Save All";
            this.menuFileSaveAll.Click += new System.EventHandler(this.menuFileSaveAll_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Name = "menuItem9";
            this.menuItem9.Size = new System.Drawing.Size(120, 22);
            this.menuItem9.Text = "-";
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(120, 22);
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditUndo,
            this.menuItem3,
            this.menuEditCut,
            this.menuEditCopy,
            this.menuEditPaste,
            this.menuItem8,
            this.menuEditSelect});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "Edit";
            // 
            // menuEditUndo
            // 
            this.menuEditUndo.Name = "menuEditUndo";
            this.menuEditUndo.Size = new System.Drawing.Size(122, 22);
            this.menuEditUndo.Text = "Undo";
            this.menuEditUndo.Click += new System.EventHandler(this.menuEditUndo_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Name = "menuItem3";
            this.menuItem3.Size = new System.Drawing.Size(122, 22);
            this.menuItem3.Text = "-";
            // 
            // menuEditCut
            // 
            this.menuEditCut.Name = "menuEditCut";
            this.menuEditCut.Size = new System.Drawing.Size(122, 22);
            this.menuEditCut.Text = "Cut";
            this.menuEditCut.Click += new System.EventHandler(this.menuEditCut_Click);
            // 
            // menuEditCopy
            // 
            this.menuEditCopy.Name = "menuEditCopy";
            this.menuEditCopy.Size = new System.Drawing.Size(122, 22);
            this.menuEditCopy.Text = "Copy";
            this.menuEditCopy.Click += new System.EventHandler(this.menuEditCopy_Click);
            // 
            // menuEditPaste
            // 
            this.menuEditPaste.Name = "menuEditPaste";
            this.menuEditPaste.Size = new System.Drawing.Size(122, 22);
            this.menuEditPaste.Text = "Paste";
            this.menuEditPaste.Click += new System.EventHandler(this.menuEditPaste_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Name = "menuItem8";
            this.menuItem8.Size = new System.Drawing.Size(122, 22);
            this.menuItem8.Text = "-";
            // 
            // menuEditSelect
            // 
            this.menuEditSelect.Name = "menuEditSelect";
            this.menuEditSelect.Size = new System.Drawing.Size(122, 22);
            this.menuEditSelect.Text = "Select All";
            this.menuEditSelect.Click += new System.EventHandler(this.menuEditSelect_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWindowHorizontal,
            this.menuWindowVertical,
            this.menuWindowCascade});
            this.menuWindow.Name = "menuWindow";
            this.menuWindow.Size = new System.Drawing.Size(63, 20);
            this.menuWindow.Text = "Window";
            // 
            // menuWindowHorizontal
            // 
            this.menuWindowHorizontal.Name = "menuWindowHorizontal";
            this.menuWindowHorizontal.Size = new System.Drawing.Size(150, 22);
            this.menuWindowHorizontal.Text = "Tile Horizontal";
            this.menuWindowHorizontal.Click += new System.EventHandler(this.menuWindowHorizontal_Click);
            // 
            // menuWindowVertical
            // 
            this.menuWindowVertical.Name = "menuWindowVertical";
            this.menuWindowVertical.Size = new System.Drawing.Size(150, 22);
            this.menuWindowVertical.Text = "Tile Vertical";
            this.menuWindowVertical.Click += new System.EventHandler(this.menuWindowVertical_Click);
            // 
            // menuWindowCascade
            // 
            this.menuWindowCascade.Name = "menuWindowCascade";
            this.menuWindowCascade.Size = new System.Drawing.Size(150, 22);
            this.menuWindowCascade.Text = "Cascade";
            this.menuWindowCascade.Click += new System.EventHandler(this.menuWindowCascade_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "Help";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.menuHelpAbout.Text = "About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // toolBarImages
            // 
            this.toolBarImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.toolBarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImages.ImageStream")));
            this.toolBarImages.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarImages.Images.SetKeyName(0, "");
            this.toolBarImages.Images.SetKeyName(1, "");
            this.toolBarImages.Images.SetKeyName(2, "");
            this.toolBarImages.Images.SetKeyName(3, "");
            this.toolBarImages.Images.SetKeyName(4, "");
            this.toolBarImages.Images.SetKeyName(5, "");
            this.toolBarImages.Images.SetKeyName(6, "");
            this.toolBarImages.Images.SetKeyName(7, "");
            this.toolBarImages.Images.SetKeyName(8, "");
            this.toolBarImages.Images.SetKeyName(9, "");
            this.toolBarImages.Images.SetKeyName(10, "");
            // 
            // editToolBar
            // 
            this.editToolBar.AutoSize = false;
            this.editToolBar.Dock = System.Windows.Forms.DockStyle.None;
            this.editToolBar.ImageList = this.toolBarImages;
            this.editToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBarButton,
            this.openBarButton,
            this.saveBarButton,
            this.toolStripSeparator1,
            this.cutBarButton,
            this.copyBarButton,
            this.pasteBarButton,
            this.toolStripSeparator2,
            this.undoBarButton});
            this.editToolBar.Location = new System.Drawing.Point(0, 0);
            this.editToolBar.Name = "editToolBar";
            this.editToolBar.Size = new System.Drawing.Size(648, 39);
            this.editToolBar.TabIndex = 1;
            // 
            // newBarButton
            // 
            this.newBarButton.ImageIndex = 4;
            this.newBarButton.Name = "newBarButton";
            this.newBarButton.Size = new System.Drawing.Size(23, 36);
            this.newBarButton.ToolTipText = "New";
            this.newBarButton.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // openBarButton
            // 
            this.openBarButton.ImageIndex = 5;
            this.openBarButton.Name = "openBarButton";
            this.openBarButton.Size = new System.Drawing.Size(23, 36);
            this.openBarButton.ToolTipText = "Open";
            this.openBarButton.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // saveBarButton
            // 
            this.saveBarButton.ImageIndex = 8;
            this.saveBarButton.Name = "saveBarButton";
            this.saveBarButton.Size = new System.Drawing.Size(23, 36);
            this.saveBarButton.ToolTipText = "Save";
            this.saveBarButton.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // cutBarButton
            // 
            this.cutBarButton.ImageIndex = 1;
            this.cutBarButton.Name = "cutBarButton";
            this.cutBarButton.Size = new System.Drawing.Size(23, 36);
            this.cutBarButton.ToolTipText = "Cut";
            this.cutBarButton.Click += new System.EventHandler(this.menuEditCut_Click);
            // 
            // copyBarButton
            // 
            this.copyBarButton.ImageIndex = 0;
            this.copyBarButton.Name = "copyBarButton";
            this.copyBarButton.Size = new System.Drawing.Size(23, 36);
            this.copyBarButton.ToolTipText = "Copy";
            this.copyBarButton.Click += new System.EventHandler(this.menuEditCopy_Click);
            // 
            // pasteBarButton
            // 
            this.pasteBarButton.ImageIndex = 6;
            this.pasteBarButton.Name = "pasteBarButton";
            this.pasteBarButton.Size = new System.Drawing.Size(23, 36);
            this.pasteBarButton.ToolTipText = "Paste";
            this.pasteBarButton.Click += new System.EventHandler(this.menuEditPaste_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // undoBarButton
            // 
            this.undoBarButton.ImageIndex = 10;
            this.undoBarButton.Name = "undoBarButton";
            this.undoBarButton.Size = new System.Drawing.Size(23, 36);
            this.undoBarButton.ToolTipText = "Undo";
            this.undoBarButton.Click += new System.EventHandler(this.menuEditUndo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(648, 417);
            this.Controls.Add(this.editToolBar);
            this.Controls.Add(this.statusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Dictionary Build";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.editToolBar.ResumeLayout(false);
            this.editToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
#endregion


	}
}
