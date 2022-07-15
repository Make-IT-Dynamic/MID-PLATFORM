namespace MID_PLATFORM_CLIENT
{
    partial class GetListWindow
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
            this.GetListDGV = new System.Windows.Forms.DataGridView();
            this.listView1 = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.GetListDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // GetListDGV
            // 
            this.GetListDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GetListDGV.Location = new System.Drawing.Point(12, 12);
            this.GetListDGV.Name = "GetListDGV";
            this.GetListDGV.RowHeadersWidth = 51;
            this.GetListDGV.RowTemplate.Height = 29;
            this.GetListDGV.Size = new System.Drawing.Size(353, 350);
            this.GetListDGV.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(371, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(334, 350);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // GetListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 374);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.GetListDGV);
            this.Name = "GetListWindow";
            this.Text = "GetListWindow";
            this.Load += new System.EventHandler(this.GetListWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GetListDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView GetListDGV;
        private ListView listView1;
    }
}