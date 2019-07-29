namespace Stocks.Windows
{
    partial class Form1
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
            this.searchText = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.Button();
            this.stockData = new System.Windows.Forms.DataGridView();
            this.progressMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Notes = new System.Windows.Forms.RichTextBox();
            this.addStock = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.stockData)).BeginInit();
            this.SuspendLayout();
            // 
            // searchText
            // 
            this.searchText.Location = new System.Drawing.Point(13, 22);
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(664, 20);
            this.searchText.TabIndex = 0;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(699, 18);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.Search_Click);
            // 
            // stockData
            // 
            this.stockData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stockData.Location = new System.Drawing.Point(13, 63);
            this.stockData.Name = "stockData";
            this.stockData.Size = new System.Drawing.Size(664, 320);
            this.stockData.TabIndex = 2;
            // 
            // progressMessage
            // 
            this.progressMessage.AutoSize = true;
            this.progressMessage.Location = new System.Drawing.Point(12, 396);
            this.progressMessage.Name = "progressMessage";
            this.progressMessage.Size = new System.Drawing.Size(0, 13);
            this.progressMessage.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(495, 396);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 5;
            // 
            // Notes
            // 
            this.Notes.Location = new System.Drawing.Point(683, 63);
            this.Notes.Name = "Notes";
            this.Notes.Size = new System.Drawing.Size(105, 320);
            this.Notes.TabIndex = 6;
            this.Notes.Text = "";
            // 
            // addStock
            // 
            this.addStock.Location = new System.Drawing.Point(683, 395);
            this.addStock.Name = "addStock";
            this.addStock.Size = new System.Drawing.Size(105, 43);
            this.addStock.TabIndex = 7;
            this.addStock.Text = "Add Stock";
            this.addStock.UseVisualStyleBackColor = true;
            this.addStock.Click += new System.EventHandler(this.AddStock_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addStock);
            this.Controls.Add(this.Notes);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.progressMessage);
            this.Controls.Add(this.stockData);
            this.Controls.Add(this.search);
            this.Controls.Add(this.searchText);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.stockData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.DataGridView stockData;
        private System.Windows.Forms.Label progressMessage;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox Notes;
        private System.Windows.Forms.Button addStock;
    }
}

