namespace KSPdeltaVCalc
{
    partial class Result
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Result));
            this.deltav_result = new System.Windows.Forms.Label();
            this.count_of_stages = new System.Windows.Forms.Label();
            this.deltav_list = new System.Windows.Forms.ListBox();
            this.but_quit = new System.Windows.Forms.Button();
            this.but_new = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deltav_result
            // 
            resources.ApplyResources(this.deltav_result, "deltav_result");
            this.deltav_result.BackColor = System.Drawing.Color.Transparent;
            this.deltav_result.ForeColor = System.Drawing.Color.Goldenrod;
            this.deltav_result.Name = "deltav_result";
            // 
            // count_of_stages
            // 
            resources.ApplyResources(this.count_of_stages, "count_of_stages");
            this.count_of_stages.BackColor = System.Drawing.Color.Transparent;
            this.count_of_stages.ForeColor = System.Drawing.Color.Goldenrod;
            this.count_of_stages.Name = "count_of_stages";
            // 
            // deltav_list
            // 
            resources.ApplyResources(this.deltav_list, "deltav_list");
            this.deltav_list.BackColor = System.Drawing.Color.Black;
            this.deltav_list.ForeColor = System.Drawing.Color.Goldenrod;
            this.deltav_list.FormattingEnabled = true;
            this.deltav_list.Name = "deltav_list";
            // 
            // but_quit
            // 
            resources.ApplyResources(this.but_quit, "but_quit");
            this.but_quit.BackColor = System.Drawing.Color.Black;
            this.but_quit.ForeColor = System.Drawing.Color.Goldenrod;
            this.but_quit.Name = "but_quit";
            this.but_quit.UseVisualStyleBackColor = false;
            this.but_quit.Click += new System.EventHandler(this.exit);
            // 
            // but_new
            // 
            resources.ApplyResources(this.but_new, "but_new");
            this.but_new.BackColor = System.Drawing.Color.Black;
            this.but_new.ForeColor = System.Drawing.Color.Goldenrod;
            this.but_new.Name = "but_new";
            this.but_new.UseVisualStyleBackColor = false;
            this.but_new.Click += new System.EventHandler(this.new_calc);
            // 
            // Result
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.but_quit);
            this.Controls.Add(this.but_new);
            this.Controls.Add(this.deltav_list);
            this.Controls.Add(this.count_of_stages);
            this.Controls.Add(this.deltav_result);
            this.Name = "Result";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label deltav_result;
        private System.Windows.Forms.Label count_of_stages;
        private System.Windows.Forms.ListBox deltav_list;
        private System.Windows.Forms.Button but_quit;
        private System.Windows.Forms.Button but_new;
    }
}