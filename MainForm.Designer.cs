namespace myGraf
{
    partial class mForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mForm1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuMain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.новыйГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestComp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestTree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestPlanar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTestEiler = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestGamilt = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindRadius = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindHrom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindOstov = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindEx = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindWay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindComp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindMatrixLink = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindMatrixContrLink = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindBridgeRib = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindTochkaSoch = new System.Windows.Forms.ToolStripMenuItem();
            this.рисоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaintCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaintCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPaintDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mCursorPosition = new System.Windows.Forms.Label();
            this.mGrafRegion = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelResult = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGrafRegion)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain,
            this.mnuTest,
            this.mnuFind,
            this.рисоватьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(460, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "mMenuProgramm";
            // 
            // mnuMain
            // 
            this.mnuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileSave,
            this.mnuFileLoad,
            this.toolStripSeparator1,
            this.новыйГрафToolStripMenuItem,
            this.toolStripSeparator2,
            this.mnuExit});
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(45, 20);
            this.mnuMain.Text = "Файл";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(206, 22);
            this.mnuFileSave.Text = "Сохранить граф";
            this.mnuFileSave.Click += new System.EventHandler(this.сохранитьГрафToolStripMenuItem_Click);
            // 
            // mnuFileLoad
            // 
            this.mnuFileLoad.Name = "mnuFileLoad";
            this.mnuFileLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuFileLoad.Size = new System.Drawing.Size(206, 22);
            this.mnuFileLoad.Text = "Загрузить граф";
            this.mnuFileLoad.Click += new System.EventHandler(this.mnuFileLoad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // новыйГрафToolStripMenuItem
            // 
            this.новыйГрафToolStripMenuItem.Name = "новыйГрафToolStripMenuItem";
            this.новыйГрафToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.новыйГрафToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.новыйГрафToolStripMenuItem.Text = "Новый граф";
            this.новыйГрафToolStripMenuItem.Click += new System.EventHandler(this.новыйГрафToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuExit.Size = new System.Drawing.Size(206, 22);
            this.mnuExit.Text = "Выход";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuTest
            // 
            this.mnuTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTestComp,
            this.mnuTestTree,
            this.mnuTestPlanar,
            this.toolStripSeparator3,
            this.mnuTestEiler,
            this.mnuTestGamilt});
            this.mnuTest.Name = "mnuTest";
            this.mnuTest.Size = new System.Drawing.Size(114, 20);
            this.mnuTest.Text = "Является ли граф:";
            // 
            // mnuTestComp
            // 
            this.mnuTestComp.Name = "mnuTestComp";
            this.mnuTestComp.Size = new System.Drawing.Size(165, 22);
            this.mnuTestComp.Text = "Связным";
            this.mnuTestComp.Click += new System.EventHandler(this.butCompCoh_Click);
            // 
            // mnuTestTree
            // 
            this.mnuTestTree.Name = "mnuTestTree";
            this.mnuTestTree.Size = new System.Drawing.Size(165, 22);
            this.mnuTestTree.Text = "Деревом";
            this.mnuTestTree.Click += new System.EventHandler(this.mnuTestTree_Click);
            // 
            // mnuTestPlanar
            // 
            this.mnuTestPlanar.Name = "mnuTestPlanar";
            this.mnuTestPlanar.Size = new System.Drawing.Size(165, 22);
            this.mnuTestPlanar.Text = "Планарным";
            this.mnuTestPlanar.Click += new System.EventHandler(this.butPlanar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(162, 6);
            // 
            // mnuTestEiler
            // 
            this.mnuTestEiler.Name = "mnuTestEiler";
            this.mnuTestEiler.Size = new System.Drawing.Size(165, 22);
            this.mnuTestEiler.Text = "Эйлеровым";
            this.mnuTestEiler.Click += new System.EventHandler(this.mnuTestEiler_Click);
            // 
            // mnuTestGamilt
            // 
            this.mnuTestGamilt.Name = "mnuTestGamilt";
            this.mnuTestGamilt.Size = new System.Drawing.Size(165, 22);
            this.mnuTestGamilt.Text = "Гамильтоновым";
            this.mnuTestGamilt.Click += new System.EventHandler(this.mnuTestGamilt_Click);
            // 
            // mnuFind
            // 
            this.mnuFind.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFindRadius,
            this.mnuFindHrom,
            this.mnuFindOstov,
            this.mnuFindEx,
            this.mnuFindWay,
            this.mnuFindLevel,
            this.mnuFindReverse,
            this.mnuFindMatrix,
            this.mnuFindComp,
            this.mnuFindMatrixLink,
            this.mnuFindMatrixContrLink,
            this.mnuFindBridgeRib,
            this.mnuFindTochkaSoch});
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.Size = new System.Drawing.Size(50, 20);
            this.mnuFind.Text = "Найти";
            // 
            // mnuFindRadius
            // 
            this.mnuFindRadius.Name = "mnuFindRadius";
            this.mnuFindRadius.Size = new System.Drawing.Size(247, 22);
            this.mnuFindRadius.Text = "Радиус и диаметр";
            this.mnuFindRadius.Click += new System.EventHandler(this.mnuFindRadius_Click);
            // 
            // mnuFindHrom
            // 
            this.mnuFindHrom.Name = "mnuFindHrom";
            this.mnuFindHrom.Size = new System.Drawing.Size(247, 22);
            this.mnuFindHrom.Text = "Хроматическое число";
            this.mnuFindHrom.Click += new System.EventHandler(this.butFill_Click);
            // 
            // mnuFindOstov
            // 
            this.mnuFindOstov.Name = "mnuFindOstov";
            this.mnuFindOstov.Size = new System.Drawing.Size(247, 22);
            this.mnuFindOstov.Text = "Остовное дерево";
            this.mnuFindOstov.Click += new System.EventHandler(this.mnuFindOstov_Click);
            // 
            // mnuFindEx
            // 
            this.mnuFindEx.Name = "mnuFindEx";
            this.mnuFindEx.Size = new System.Drawing.Size(247, 22);
            this.mnuFindEx.Text = "Эксцентритет вершины N";
            this.mnuFindEx.Click += new System.EventHandler(this.mnuFindEx_Click);
            // 
            // mnuFindWay
            // 
            this.mnuFindWay.Name = "mnuFindWay";
            this.mnuFindWay.Size = new System.Drawing.Size(247, 22);
            this.mnuFindWay.Text = "Путь от вершины N до M";
            this.mnuFindWay.Click += new System.EventHandler(this.mnuFindWay_Click);
            // 
            // mnuFindLevel
            // 
            this.mnuFindLevel.Name = "mnuFindLevel";
            this.mnuFindLevel.Size = new System.Drawing.Size(247, 22);
            this.mnuFindLevel.Text = "Уровни вершин относительно N";
            this.mnuFindLevel.Click += new System.EventHandler(this.butLevelNode_Click);
            // 
            // mnuFindReverse
            // 
            this.mnuFindReverse.Name = "mnuFindReverse";
            this.mnuFindReverse.Size = new System.Drawing.Size(247, 22);
            this.mnuFindReverse.Text = "Обратный граф";
            this.mnuFindReverse.Click += new System.EventHandler(this.mnuFindReverse_Click);
            // 
            // mnuFindMatrix
            // 
            this.mnuFindMatrix.Name = "mnuFindMatrix";
            this.mnuFindMatrix.Size = new System.Drawing.Size(247, 22);
            this.mnuFindMatrix.Text = "Матрицу графа";
            this.mnuFindMatrix.Click += new System.EventHandler(this.butMatrix);
            // 
            // mnuFindComp
            // 
            this.mnuFindComp.Name = "mnuFindComp";
            this.mnuFindComp.Size = new System.Drawing.Size(247, 22);
            this.mnuFindComp.Text = "Число компонент связности";
            this.mnuFindComp.Click += new System.EventHandler(this.mnuFindComp_Click);
            // 
            // mnuFindMatrixLink
            // 
            this.mnuFindMatrixLink.Name = "mnuFindMatrixLink";
            this.mnuFindMatrixLink.Size = new System.Drawing.Size(247, 22);
            this.mnuFindMatrixLink.Text = "Матрицу достижимости";
            this.mnuFindMatrixLink.Click += new System.EventHandler(this.mnuFindMatrixLink_Click);
            // 
            // mnuFindMatrixContrLink
            // 
            this.mnuFindMatrixContrLink.Name = "mnuFindMatrixContrLink";
            this.mnuFindMatrixContrLink.Size = new System.Drawing.Size(247, 22);
            this.mnuFindMatrixContrLink.Text = "Матрицу контрдостижимости";
            this.mnuFindMatrixContrLink.Click += new System.EventHandler(this.mnuFindMatrixContrLink_Click);
            // 
            // mnuFindBridgeRib
            // 
            this.mnuFindBridgeRib.Name = "mnuFindBridgeRib";
            this.mnuFindBridgeRib.Size = new System.Drawing.Size(247, 22);
            this.mnuFindBridgeRib.Text = "Мост ли ребро между N и M";
            this.mnuFindBridgeRib.Click += new System.EventHandler(this.mnuFindBridgeRib_Click);
            // 
            // mnuFindTochkaSoch
            // 
            this.mnuFindTochkaSoch.Name = "mnuFindTochkaSoch";
            this.mnuFindTochkaSoch.Size = new System.Drawing.Size(247, 22);
            this.mnuFindTochkaSoch.Text = "Точка сочления N или нет";
            this.mnuFindTochkaSoch.Click += new System.EventHandler(this.mnuFindTochkaSoch_Click);
            // 
            // рисоватьToolStripMenuItem
            // 
            this.рисоватьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPaintCreate,
            this.mnuPaintCancel,
            this.mnuPaintDelete});
            this.рисоватьToolStripMenuItem.Name = "рисоватьToolStripMenuItem";
            this.рисоватьToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.рисоватьToolStripMenuItem.Text = "Рисование";
            // 
            // mnuPaintCreate
            // 
            this.mnuPaintCreate.Name = "mnuPaintCreate";
            this.mnuPaintCreate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuPaintCreate.Size = new System.Drawing.Size(224, 22);
            this.mnuPaintCreate.Text = "Творить граф";
            this.mnuPaintCreate.Click += new System.EventHandler(this.butDraw_Click);
            // 
            // mnuPaintCancel
            // 
            this.mnuPaintCancel.Name = "mnuPaintCancel";
            this.mnuPaintCancel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuPaintCancel.Size = new System.Drawing.Size(224, 22);
            this.mnuPaintCancel.Text = "Отменить действие";
            this.mnuPaintCancel.Click += new System.EventHandler(this.mnuPaintCancel_Click);
            // 
            // mnuPaintDelete
            // 
            this.mnuPaintDelete.Name = "mnuPaintDelete";
            this.mnuPaintDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuPaintDelete.Size = new System.Drawing.Size(224, 22);
            this.mnuPaintDelete.Text = "Удалить вершину";
            this.mnuPaintDelete.Click += new System.EventHandler(this.mnuPaintDelete_Click);
            // 
            // mCursorPosition
            // 
            this.mCursorPosition.AutoSize = true;
            this.mCursorPosition.Location = new System.Drawing.Point(273, 101);
            this.mCursorPosition.Name = "mCursorPosition";
            this.mCursorPosition.Size = new System.Drawing.Size(0, 13);
            this.mCursorPosition.TabIndex = 3;
            // 
            // mGrafRegion
            // 
            this.mGrafRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mGrafRegion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mGrafRegion.Location = new System.Drawing.Point(12, 126);
            this.mGrafRegion.Name = "mGrafRegion";
            this.mGrafRegion.Size = new System.Drawing.Size(436, 361);
            this.mGrafRegion.TabIndex = 5;
            this.mGrafRegion.TabStop = false;
            this.mGrafRegion.Paint += new System.Windows.Forms.PaintEventHandler(this.mGrafRegion_Paint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Граф (*.graf)|*.graf|All files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Граф (*.graf)|*.graf|All files (*.*)|*.*\"";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelResult.Location = new System.Drawing.Point(14, 41);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(384, 15);
            this.labelResult.TabIndex = 7;
            this.labelResult.Text = "Добро пожаловать в программу \"Визуализация графов\"";
            // 
            // mForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(460, 499);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.mGrafRegion);
            this.Controls.Add(this.mCursorPosition);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(420, 392);
            this.Name = "mForm1";
            this.Text = "Визуализация графов";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGrafRegion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuMain;
        private System.Windows.Forms.Label mCursorPosition;
        private System.Windows.Forms.PictureBox mGrafRegion;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTest;
        private System.Windows.Forms.ToolStripMenuItem mnuTestComp;
        private System.Windows.Forms.ToolStripMenuItem mnuTestTree;
        private System.Windows.Forms.ToolStripMenuItem mnuTestEiler;
        private System.Windows.Forms.ToolStripMenuItem mnuTestGamilt;
        private System.Windows.Forms.ToolStripMenuItem mnuTestPlanar;
        private System.Windows.Forms.ToolStripMenuItem mnuFind;
        private System.Windows.Forms.ToolStripMenuItem mnuFindRadius;
        private System.Windows.Forms.ToolStripMenuItem mnuFindHrom;
        private System.Windows.Forms.ToolStripMenuItem mnuFindOstov;
        private System.Windows.Forms.ToolStripMenuItem mnuFindEx;
        private System.Windows.Forms.ToolStripMenuItem mnuFindWay;
        private System.Windows.Forms.ToolStripMenuItem новыйГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рисоватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPaintCreate;
        private System.Windows.Forms.ToolStripMenuItem mnuPaintCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuFindLevel;
        private System.Windows.Forms.ToolStripMenuItem mnuFindReverse;
        private System.Windows.Forms.ToolStripMenuItem mnuFindMatrix;
        private System.Windows.Forms.ToolStripMenuItem mnuPaintDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuFindComp;
        private System.Windows.Forms.ToolStripMenuItem mnuFindMatrixLink;
        private System.Windows.Forms.ToolStripMenuItem mnuFindMatrixContrLink;
        private System.Windows.Forms.ToolStripMenuItem mnuFindBridgeRib;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuFileLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuFindTochkaSoch;
        private System.Windows.Forms.Label labelResult;
    }
}

