namespace Stocks.Windows
{
    partial class addStockForm
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
            this.setID = new System.Windows.Forms.TextBox();
            this.setStockName = new System.Windows.Forms.TextBox();
            this.setStockVolume = new System.Windows.Forms.TextBox();
            this.setStockPrice = new System.Windows.Forms.TextBox();
            this.stockID = new System.Windows.Forms.Label();
            this.stockName = new System.Windows.Forms.Label();
            this.stockVolume = new System.Windows.Forms.Label();
            this.stockPrice = new System.Windows.Forms.Label();
            this.saveStock = new System.Windows.Forms.Button();
            this.backToForm1 = new System.Windows.Forms.Button();
            this.errorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // setID
            // 
            this.setID.Location = new System.Drawing.Point(48, 65);
            this.setID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setID.Name = "setID";
            this.setID.Size = new System.Drawing.Size(132, 22);
            this.setID.TabIndex = 0;
            // 
            // setStockName
            // 
            this.setStockName.Location = new System.Drawing.Point(189, 65);
            this.setStockName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setStockName.Name = "setStockName";
            this.setStockName.Size = new System.Drawing.Size(132, 22);
            this.setStockName.TabIndex = 1;
            // 
            // setStockVolume
            // 
            this.setStockVolume.Location = new System.Drawing.Point(331, 64);
            this.setStockVolume.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setStockVolume.Name = "setStockVolume";
            this.setStockVolume.Size = new System.Drawing.Size(132, 22);
            this.setStockVolume.TabIndex = 2;
            // 
            // setStockPrice
            // 
            this.setStockPrice.Location = new System.Drawing.Point(472, 65);
            this.setStockPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.setStockPrice.Name = "setStockPrice";
            this.setStockPrice.Size = new System.Drawing.Size(132, 22);
            this.setStockPrice.TabIndex = 3;
            // 
            // stockID
            // 
            this.stockID.AutoSize = true;
            this.stockID.Location = new System.Drawing.Point(48, 42);
            this.stockID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stockID.Name = "stockID";
            this.stockID.Size = new System.Drawing.Size(21, 17);
            this.stockID.TabIndex = 4;
            this.stockID.Text = "ID";
            // 
            // stockName
            // 
            this.stockName.AutoSize = true;
            this.stockName.Location = new System.Drawing.Point(189, 42);
            this.stockName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stockName.Name = "stockName";
            this.stockName.Size = new System.Drawing.Size(45, 17);
            this.stockName.TabIndex = 5;
            this.stockName.Text = "Name";
            // 
            // stockVolume
            // 
            this.stockVolume.AutoSize = true;
            this.stockVolume.Location = new System.Drawing.Point(331, 42);
            this.stockVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stockVolume.Name = "stockVolume";
            this.stockVolume.Size = new System.Drawing.Size(55, 17);
            this.stockVolume.TabIndex = 6;
            this.stockVolume.Text = "Volume";
            // 
            // stockPrice
            // 
            this.stockPrice.AutoSize = true;
            this.stockPrice.Location = new System.Drawing.Point(472, 42);
            this.stockPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stockPrice.Name = "stockPrice";
            this.stockPrice.Size = new System.Drawing.Size(40, 17);
            this.stockPrice.TabIndex = 7;
            this.stockPrice.Text = "Price";
            // 
            // saveStock
            // 
            this.saveStock.Location = new System.Drawing.Point(193, 116);
            this.saveStock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveStock.Name = "saveStock";
            this.saveStock.Size = new System.Drawing.Size(271, 47);
            this.saveStock.TabIndex = 8;
            this.saveStock.Text = "Save";
            this.saveStock.UseVisualStyleBackColor = true;
            this.saveStock.Click += new System.EventHandler(this.SaveStock_Click);
            // 
            // backToForm1
            // 
            this.backToForm1.Location = new System.Drawing.Point(767, 16);
            this.backToForm1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backToForm1.Name = "backToForm1";
            this.backToForm1.Size = new System.Drawing.Size(124, 42);
            this.backToForm1.TabIndex = 9;
            this.backToForm1.Text = "Back";
            this.backToForm1.UseVisualStyleBackColor = true;
            this.backToForm1.Click += new System.EventHandler(this.BackToForm1_Click);
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(52, 203);
            this.errorMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(0, 17);
            this.errorMessage.TabIndex = 10;
            // 
            // addStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.backToForm1);
            this.Controls.Add(this.saveStock);
            this.Controls.Add(this.stockPrice);
            this.Controls.Add(this.stockVolume);
            this.Controls.Add(this.stockName);
            this.Controls.Add(this.stockID);
            this.Controls.Add(this.setStockPrice);
            this.Controls.Add(this.setStockVolume);
            this.Controls.Add(this.setStockName);
            this.Controls.Add(this.setID);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "addStockForm";
            this.Text = "AddStock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox setID;
        private System.Windows.Forms.TextBox setStockName;
        private System.Windows.Forms.TextBox setStockVolume;
        private System.Windows.Forms.TextBox setStockPrice;
        private System.Windows.Forms.Label stockID;
        private System.Windows.Forms.Label stockName;
        private System.Windows.Forms.Label stockVolume;
        private System.Windows.Forms.Label stockPrice;
        private System.Windows.Forms.Button saveStock;
        private System.Windows.Forms.Button backToForm1;
        private System.Windows.Forms.Label errorMessage;
    }
}